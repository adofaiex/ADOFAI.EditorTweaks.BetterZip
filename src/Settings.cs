using ADOFAI.EditorTweaks.BetterZip.Features.ArchiveIo;
using UnityModManagerNet;
using UnityEngine;

namespace ADOFAI.EditorTweaks.BetterZip
{
    public class Settings : UnityModManager.ModSettings
    {
        public string LegacyZipEncoding = LegacyZipEncodingModes.Auto;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("压缩包与旧编码处理");
            GUILayout.Label("旧 ZIP 文件名编码模式：" + LegacyZipEncoding);
            GUILayout.BeginHorizontal();
            foreach (string mode in LegacyZipEncodingModes.Values)
            {
                if (GUILayout.Button(mode, GUILayout.Width(90f))) LegacyZipEncoding = mode;
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("7z、RAR、ADOZIP 以及路径安全检查始终由此 Mod 负责。");
            GUILayout.EndVertical();
        }

        public void OnSaveGUI(UnityModManager.ModEntry modEntry) { Save(modEntry); }

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Normalize();
            Save(this, modEntry);
        }

        public void EnsureDefaults() { Normalize(); }

        public void Normalize() { LegacyZipEncoding = LegacyZipEncodingModes.Normalize(LegacyZipEncoding); }

        public void ResetAllDefaults(UnityModManager.ModEntry modEntry)
        {
            LegacyZipEncoding = LegacyZipEncodingModes.Auto;
            Normalize();
        }

        public static string Text(string key) { return Localization.Text(key); }

        public static Settings Load(UnityModManager.ModEntry modEntry) { return Load<Settings>(modEntry); }
    }
}
