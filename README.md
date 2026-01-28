# Cat Timer (Enhanced)

Cat Timer 是一个极其简单、可爱的计时器软件。

![主界面](ReadMeImage/MainWindow.png)

## 开源致谢

本项目基于开源作者 **xujiangjiang** 的 [Easy-Cat-Timer](https://github.com/xujiangjiang/Easy-Cat-Timer) 项目进行二次开发与功能增强。

---

## 新增功能 (2026-01-28)

在原版基础上，我进行了以下核心功能升级与优化：

### 1. 正向计时功能
- **双模式切换**：不仅支持原有的倒计时功能，新增了**正向计时**模式。
- **UI 集成**：在设置界面添加了模式切换开关，支持一键切换计时方向。
- **独立音效**：为正向计时模式配置了专属的开始、暂停、重置及完成音效。

### 2. 技术架构现代化
- **.NET 6 迁移**：将项目从过时的 .NET Framework 4.6.1 迁移到了现代化的 **.NET 6.0 (Windows)** 平台，提升了运行效率和系统兼容性。
- **代码重构**：优化了计时逻辑系统，使用 `TimerMode` 枚举管理状态，并实现了更完善的 `INotifyPropertyChanged` 数据绑定。

---

## 技术细节

| 功能模块 | 说明 |
| :--- | :--- |
| **计时系统** | 基于 `DispatcherTimer` 实现，支持 `Countdown` 和 `Forward` 两种模式。 |
| **UI 框架** | 纯 WPF 实现，包含大量自定义控件与异形窗口设计。 |
| **目标平台** | .NET 6.0 (Windows)，采用 **Framework-dependent** 发布以优化体积。 |
| **数据持久化** | 使用 `Settings.settings` 自动保存用户的置顶设置、音量及计时模式。 |
| **打包技术** | 使用 WiX Toolset v5 编写安装脚本，实现自动化资源收集。 |

## 安装与运行

1. **下载安装包**：在项目根目录找到 `CatTimerSetup.msi` 并运行。
2. **环境要求**：运行环境需安装 [.NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)。
3. **手动编译**：
   ```powershell
   # 在 CatTimer WpfProject 目录下执行
   dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -o Publish\Release
   dotnet wix build Package.wxs -ext WixToolset.UI.wixext -o CatTimerSetup.msi
   ```

---

## 声明

本项目仅供学习交流使用，尊重原作者 **xujiangjiang** 的劳动成果。
