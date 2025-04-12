using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Flarial.Launcher.App;
using ModernWpf.Controls;
using Modern = ModernWpf.Controls;

sealed class Play : Grid
{
    readonly Image Image = new()
    {
        Source = Manifest.Icon,
        Width = Manifest.Icon.Width / 2,
        Height = Manifest.Icon.Height / 2,
        VerticalAlignment = VerticalAlignment.Center,
    };

    readonly Modern.ProgressBar ProgressBar = new()
    {
        Width = Manifest.Icon.Width * 2,
        Foreground = new SolidColorBrush(Colors.White),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        IsIndeterminate = true,
        Visibility = Visibility.Visible
    };

    readonly TextBlock TextBlock = new()
    {
        Text = "Preparing...",
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        Visibility = Visibility.Visible
    };

    readonly Button Button = new()
    {
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        Content = new SymbolIcon(Symbol.Play),
        Width = Manifest.Icon.Width * 2,
        Visibility = Visibility.Collapsed
    };

    internal Play()
    {

    }
}