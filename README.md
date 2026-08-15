# ADOFAI.EditorTweaks.BetterZip

独立的谱面压缩包 Mod，版本 `1.0.0`。只负责 ZIP、ADOZIP、7z、RAR、TAR、GZip、BZip2、XZ、CAB 等归档的读取/导出、旧 ZIP 文件名编码和压缩包安全检查。

UMM 面板只提供旧 ZIP 文件名编码模式：Auto、CP949、GB18030、Shift-JIS、CP437。项目不包含编辑器优化、视频渲染、Web UI 或 FFmpeg，也不依赖另外两个 Mod。

构建：

```powershell
dotnet build ADOFAI.EditorTweaks.BetterZip.csproj -c Debug
dotnet build ADOFAI.EditorTweaks.BetterZip.csproj -c Release
```

产物在 `out/` 和 `Build/`，部署目录为 `Mods/ADOFAI.EditorTweaks.BetterZip/`。发行包额外包含 `SharpSevenZip.dll`、许可证和 `ThirdParty/7-Zip/x64/7z.dll`。
