using System.Threading;
using ADOFAI.EditorTweaks.BetterZip.Patching;
using UnityModManagerNet;

namespace ADOFAI.EditorTweaks.BetterZip
{
    public static class Main
    {
        public static UnityModManager.ModEntry? Mod { get; private set; }
        public static Settings Settings { get; private set; } = null!;
        internal static int UnityThreadId { get; private set; }

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            UnityThreadId = Thread.CurrentThread.ManagedThreadId;
            Mod = modEntry;
            Localization.Load(modEntry);
            Settings = Settings.Load(modEntry);
            Settings.EnsureDefaults();
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = Settings.OnGUI;
            modEntry.OnSaveGUI = Settings.OnSaveGUI;
            modEntry.Logger.Log("ADOFAI.EditorTweaks.BetterZip loaded.");
            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                modEntry.Logger.Log("ADOFAI.EditorTweaks.BetterZip enabled.");
                PatchManager.ApplyAll(modEntry.Info.Id);
            }
            else
            {
                modEntry.Logger.Log("ADOFAI.EditorTweaks.BetterZip disabled.");
                PatchManager.UnpatchAll();
            }
            return true;
        }

        public static void Log(string message) { Mod?.Logger.Log(message); }
    }
}
