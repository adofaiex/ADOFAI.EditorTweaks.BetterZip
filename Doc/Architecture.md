# BetterZip 架构

入口 `ADOFAI.EditorTweaks.BetterZip.Main.Load` 加载独立设置和本地化。启用后，唯一的 ArchiveIo 补丁组接管游戏的 `ZipUtils.Unzip` 与 `ZipUtils.Zip`。

`ArchiveService` 负责格式识别、旧文件名编码、安全解压、重复路径检查、覆盖保护和 ADOZIP 导出。服务从本 Mod 目录加载 `ThirdParty/7-Zip/x64/7z.dll`，不读取其他 Mod 的目录。
