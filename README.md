# 延迟启动工具，主要解决某些加载硬件驱动程序的软件，还有everything之类的软件开机启动太慢了，这些软件没有必要配置成开机立即启动，但是每次又要手动启动也烦。


## 🎉 支持图形界面配置启动参数

**新版特性**：仿照网上某个延时启动的程序，纯AI vibe coding一模一样的照抄功能，但是解决了它的两个烦人的地方：

1、开机启动有个倒计时大黑框，烦
2、某些软件（everything）启动之后不用直接显示主界面，直接最小化到托盘，烦

双击启动项即可设置延迟时间和启动参数，无需手动修改脚本！

---

## 快速使用

1. **运行程序**：双击 `DelayedStartupTool.exe`
2. **添加程序**：点击【添加文件】
3. **设置参数**：双击列表项，在对话框中输入参数（如 `-startup`）
4. **保存启动**：点击【保存及开机启动】
5. **测试效果**：点击【立即测试】

---

## Everything 静默启动示例

### 步骤
1. 添加 `Everything.exe` 到列表
2. **双击** Everything 这一行
3. 在"启动参数"框中输入：`-startup`
4. 点击确定
5. 保存并测试

### 效果
开机后 Everything 直接进入托盘，不显示主窗口！

---

## 文件说明

### 必需文件（全部必须）
- `DelayedStartupTool.exe` - 主程序
- `DelayedStartupTool.dll` - 程序库
- `DelayedStartupTool.runtimeconfig.json` - 运行配置
- `DelayedStartupTool.deps.json` - 依赖信息
- `Newtonsoft.Json.dll` - JSON 库

**重要**：请保持所有文件在同一目录！

### 自动生成
- `config.json` - 配置文件
- `DelayedStartup.vbs` - 启动脚本

---

## 常用启动参数

| 参数 | 说明 |
|------|------|
| `-startup` | 启动到托盘（Everything） |
| `-silent` | 静默启动（Proxifier） |
| `/background` | 后台启动（QQ、网盘） |
| `-minimized` | 最小化启动 |

---

## 系统要求

- Windows 10/11
- .NET 8.0 运行时（通常已内置）

---

## 版本信息

- **版本**：2.1.0
- **核心改进**：图形界面配置启动参数
- **基础功能**：VBScript 静默启动（无黑框）

---

**详细说明请查看 [使用说明.md](使用说明.md)**
