using System.Windows;
using System.Windows.Controls;
using Flarial.Launcher.App;
using Microsoft.Win32;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI.Controls;

sealed class Custom : SimpleStackPanel
{
    readonly TextBox TextBox = new()
    {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        IsHitTestVisible = default,
        IsInactiveSelectionHighlightEnabled = default,
        IsReadOnly = true
    };

    readonly Button Button = new()
    {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Content = new SymbolIcon(Symbol.OpenFile),
        Margin = new(0, 0, 12, 0)
    };

    readonly OpenFileDialog Dialog = new()
    {
        CheckFileExists = true,
        CheckPathExists = true,
        DereferenceLinks = true,
        Filter = "Dynamic-Link Libraries (*.dll)|*.dll"
    };

    internal Custom()
    {
        IsEnabled = default;
        Spacing = 12;

        Children.Add(new TextBlock { Text = "Select a custom DLL:" });

        Grid grid = new();
        Children.Add(grid);

        grid.ColumnDefinitions.Add(new() { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new());

        TextBox.Text = Configuration.Current.Custom;

        Grid.SetRow(Button, 0);
        Grid.SetColumn(Button, 0);
        grid.Children.Add(Button);

        Grid.SetRow(TextBox, 0);
        Grid.SetColumn(TextBox, 1);
        grid.Children.Add(TextBox);

        Button.Click += (_, _) =>
        {
            if (!(bool)Dialog.ShowDialog()) return;
            TextBox.Text = Configuration.Current.Custom = Dialog.FileName;
        };
    }
}