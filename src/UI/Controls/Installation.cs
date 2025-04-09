using ModernWpf.Controls;
using Modern = ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Flarial.Launcher.UI.Controls;

sealed class Installation : Grid
{
    internal readonly Button Install = new()
    {
        Content = new SymbolIcon(Symbol.Download),
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch,
    };

    internal readonly Button Cancel = new()
    {
        Content = new SymbolIcon(Symbol.Cancel),
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Margin = new(12, 0, 0, 0),
        Visibility = Visibility.Collapsed,
        IsEnabled = default
    };

    internal readonly Modern.ProgressBar Progress = new()
    {
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        IsIndeterminate = true,
        Foreground = new SolidColorBrush(Colors.White),
        Visibility = Visibility.Collapsed
    };

    internal Installation()
    {
        ColumnDefinitions.Add(new());
        ColumnDefinitions.Add(new() { Width = GridLength.Auto });

        SetRow(Install, 0);
        SetColumn(Install, 0);
        Children.Add(Install);

        SetRow(Progress, 0);
        SetColumn(Progress, 0);
        Children.Add(Progress);

        SetRow(Cancel, 0);
        SetColumn(Cancel, 1);
        Children.Add(Cancel);
    }
}