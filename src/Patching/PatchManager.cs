using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ADOFAI.EditorTweaks.BetterZip.Features.ArchiveIo;
using HarmonyLib;

namespace ADOFAI.EditorTweaks.BetterZip.Patching
{
    internal enum PatchFeature { ArchiveIo }
    internal enum PatchGroupState { Inactive, Active, Failed }

    internal sealed class PatchGroupStatus
    {
        public PatchGroupState State { get; internal set; }
        public int PatchCount { get; internal set; }
        public string Reason { get; internal set; } = string.Empty;
    }

    internal static class PatchManager
    {
        private static readonly PatchGroupStatus status = new PatchGroupStatus();
        private static Harmony? harmony;
        private static string harmonyId = string.Empty;

        public static PatchGroupStatus Status => status;

        public static PatchGroupStatus ApplyAll(string modId)
        {
            UnpatchAll();
            harmonyId = modId + ".archive-io";
            harmony = new Harmony(harmonyId);
            List<Type> patchTypes = CollectPatchTypes(typeof(ArchiveIoPatches));
            status.PatchCount = patchTypes.Count;
            try
            {
                foreach (Type patchType in patchTypes.OrderBy(type => type.FullName, StringComparer.Ordinal))
                    harmony.CreateClassProcessor(patchType).Patch();
                ArchiveService.EnableAdditionalArchiveExtensions();
                status.State = PatchGroupState.Active;
                status.Reason = string.Empty;
                Main.Log("[PatchManager] Archive IO active (" + patchTypes.Count + " patches).");
            }
            catch (Exception exception)
            {
                try { harmony.UnpatchAll(harmonyId); } catch { }
                try { ArchiveService.DisableAdditionalArchiveExtensions(); } catch { }
                status.State = PatchGroupState.Failed;
                status.Reason = exception.GetBaseException().Message;
                Main.Mod?.Logger.Error("[PatchManager] Archive IO failed: " + exception);
            }
            return status;
        }

        public static void UnpatchAll()
        {
            if (harmony != null && !string.IsNullOrEmpty(harmonyId))
            {
                try { harmony.UnpatchAll(harmonyId); } catch { }
            }
            try { ArchiveService.DisableAdditionalArchiveExtensions(); } catch { }
            harmony = null;
            harmonyId = string.Empty;
            status.State = PatchGroupState.Inactive;
        }

        public static string GetSummary()
        {
            return status.State == PatchGroupState.Active ? "压缩包功能可用" : "压缩包功能不可用";
        }

        private static List<Type> CollectPatchTypes(Type root)
        {
            List<Type> result = new List<Type>();
            CollectPatchTypes(root, result, new HashSet<Type>());
            return result;
        }

        private static void CollectPatchTypes(Type type, ICollection<Type> result, ISet<Type> visited)
        {
            if (!visited.Add(type)) return;
            if (type.IsDefined(typeof(HarmonyPatch), true)) result.Add(type);
            foreach (Type nested in type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
                CollectPatchTypes(nested, result, visited);
        }
    }
}
