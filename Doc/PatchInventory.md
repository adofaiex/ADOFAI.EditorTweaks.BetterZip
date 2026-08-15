# BetterZip 补丁清单

| 目标 | 作用 |
| --- | --- |
| `ZipUtils.Unzip` | 接管 ZIP、ADOZIP、7z、RAR、TAR、GZip、BZip2、XZ、CAB 等读取，并执行路径、条目数、解压体积和覆盖安全检查。 |
| `ZipUtils.Zip` | 使用标准 ZIP 导出谱面资源，保持相对目录和 Unicode 文件名。 |

归档组使用独立 Harmony ID，初始化或 7z 位数检查失败时会撤销本组补丁。
