using System.Threading.Tasks;
using System.Windows;
using Flarial.Launcher.App;
using ModernWpf;
using ModernWpf.Controls.Primitives;

namespace Flarial.Launcher.UI;

sealed class Window : System.Windows.Window
{
    internal Window()
    {
        WindowHelper.SetUseModernWindowStyle(this, true);
        ThemeManager.SetRequestedTheme(this, ElementTheme.Dark);

        Icon = Manifest.Icon;
        Title = $"Flarial Launcher {Manifest.Version}";
        Width = 960 * 0.75; Height = 540 * 0.75;
        UseLayoutRounding = SnapsToDevicePixels = true;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ResizeMode = ResizeMode.NoResize;

        Content = new Content() { IsEnabled = default };
    }
}