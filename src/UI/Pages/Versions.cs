using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Bedrockix.Minecraft;
using Flarial.Launcher.SDK;
using Flarial.Launcher.UI.Controls;

namespace Flarial.Launcher.UI.Pages;

sealed class Versions : Grid
{
    readonly ListBox ListBox = new()
    {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Margin = new(0, 0, 0, 12)
    };

    readonly Version Version = new()
    {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    internal readonly Catalog Catalog;

    internal Versions(Catalog @this)
    {
        Margin = new(12);

        RowDefinitions.Add(new());
        RowDefinitions.Add(new() { Height = GridLength.Auto });

        SetRow(ListBox, 0);
        SetColumn(ListBox, 0);
        Children.Add(ListBox);

        SetRow(Version, 1);
        SetColumn(Version, 0);
        Children.Add(Version);

        foreach (var item in (Catalog = @this).Reverse())
            ListBox.Items.Add(item);
        ListBox.SelectedIndex = default;

        Version.Install.Click += async (_, _) =>
        {
            if (!Game.Installed)
            {
                await Dialogs.Installed.ShowAsync();
                return;
            }

            if (Game.Unpackaged)
            {
                await Dialogs.Unpackaged.ShowAsync();
                return;
            }
        };
    }
}