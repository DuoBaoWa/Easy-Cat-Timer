# Cat Timer (Enhanced) - v1.0.7

Cat Timer 是一个极其简单、可爱且功能丰富的计时器软件。

![主界面](ReadMeImage/MainWindow.png)

## 开源致谢

本项目基于开源作者 **xujiangjiang** 的 [Easy-Cat-Timer](https://github.com/xujiangjiang/Easy-Cat-Timer) 项目进行二次开发与功能增强。

---

## 核心功能与优化 (v1.0.7)

在原版基础上，本项目进行了深度的功能扩展与用户体验优化：

### 1. 计时模式升级
- **双模式切换**：支持传统的**倒计时**（Countdown）和新增的**正向计时**（Forward）模式。
- **番茄钟增强**：优化了番茄钟通知逻辑，使用 `Viewbox` 确保不同长度的提醒文字都能完美显示，不再被遮挡。

### 2. UI 与交互打磨
- **完美衔接**：修复了黑猫组件底部的空白间隙，使猫咪与桌面/任务栏的视觉衔接更加自然。
- **设置界面优化**：重新设计了设置面板的布局，完美兼容中英文显示，移除了冗余字体库以减小体积。
- **窗口管理**：支持置顶显示、音量调节，并能自动保存用户的使用习惯。

### 3. 技术架构现代化
- **.NET 6 迁移**：全面升级至 **.NET 6.0 (Windows)** 平台，运行更流畅，兼容性更强。
- **WiX 自动化打包**：采用 WiX Toolset v5 构建安装包，支持“面向所有用户”的系统级安装。
- **发布模式优化**：支持 `Self-contained`（自带运行时）压缩发布，解决用户电脑没有 .NET 环境的运行难题。

---

## 技术细节

| 模块 | 说明 |
| :--- | :--- |
| **计时核心** | 基于 `DispatcherTimer` 实现，高精度管理双模式计时逻辑。 |
| **UI 框架** | 纯 WPF 实现，包含大量自定义 Path 动画与异形窗口设计。 |
| **目标平台** | .NET 6.0 (Windows)。 |
| **持久化** | 使用 `Settings.settings` 存储模式、音量及置顶状态。 |
| **打包方案** | 使用 WiX Toolset 构建 `.msi` 安装包，支持全局安装。 |

---

## 下载与安装

### 方案 A：全能安装包（推荐）
下载 **[CatTimer_v1.0.7_AllUsers.msi](CatTimer%20WpfProject/CatTimer_v1.0.7_AllUsers.msi)**。
- **特点**：自带运行环境，双击即可为所有用户安装，无需额外配置。

### 方案 B：极简运行包
如果您已安装 [.NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)，可直接下载极简包。
- [minimal_package_v1.0.7](CatTimer%20WpfProject/bin/Release/net6.0-windows/win-x64/minimal_package_v1.0.7)

---

## 开发者指南

若需手动编译或打包：

```powershell
# 1. 发布程序
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o Publish\Compressed

# 2. 构建 MSI
wix build Package.wxs -ext WixToolset.UI.wixext -o CatTimer_v1.0.7.msi
```

---

## 声明

本项目仅供学习交流使用，尊重原作者 **xujiangjiang** 的劳动成果。
