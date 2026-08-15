# BetterZip 构建

```powershell
dotnet build ADOFAI.EditorTweaks.BetterZip.csproj -c Debug
dotnet build ADOFAI.EditorTweaks.BetterZip.csproj -c Release
```

输出在 `out/`，压缩包在 `Build/`，部署到 `Mods/ADOFAI.EditorTweaks.BetterZip/`。最终包包含本项目 DLL、SharpSevenZip 运行时依赖、许可证和 x64 `7z.dll`，不包含 Web UI、FFmpeg 或编辑器代码。
