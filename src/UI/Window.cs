using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Flarial.Launcher.App;
using ModernWpf;
using ModernWpf.Controls.Primitives;

namespace Flarial.Launcher.UI;

sealed class Window : System.Windows.Window
{
    [DllImport("Shell32", CharSet = CharSet.Auto, SetLastError = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    static extern int ShellMessageBox(nint hAppInst = default, nint hWnd = default, string lpcText = default, string lpcTitle = "Error", int fuStyle = 0x00000010);

    internal Window()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, @this) =>
        {
            var value = $"{@this.ExceptionObject}"; Logger.Error(value);
            ShellMessageBox(hWnd: new WindowInteropHelper(this).EnsureHandle(), lpcText: value);
            Environment.Exit(default);
        };

        WindowHelper.SetUseModernWindowStyle(this, true);
        ThemeManager.SetRequestedTheme(this, ElementTheme.Dark);

        Icon = Manifest.Icon;
        Title = $"Flarial Launcher";
        Width = 960; Height = 540;
        UseLayoutRounding = SnapsToDevicePixels = true;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ResizeMode = ResizeMode.NoResize;

        Content = new Content() { IsEnabled = default };
    }
}