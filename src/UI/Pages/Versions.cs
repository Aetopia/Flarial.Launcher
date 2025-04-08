using System;
using System.Linq;
using System.Threading.Tasks;
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

    readonly Installation Installation = new()
    {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    internal readonly Catalog Catalog;

    Request Request = default;

    internal Versions(Catalog @this)
    {
        Margin = new(12);

        RowDefinitions.Add(new());
        RowDefinitions.Add(new() { Height = GridLength.Auto });

        SetRow(ListBox, 0);
        SetColumn(ListBox, 0);
        Children.Add(ListBox);

        SetRow(Installation, 1);
        SetColumn(Installation, 0);
        Children.Add(Installation);

        foreach (var item in (Catalog = @this).Reverse()) ListBox.Items.Add(item);
        ListBox.SelectedIndex = default;

        Installation.Install.Click += async (_, _) =>
        {
            if (!Game.Installed)
            {
                Log.Current.Write("Minecraft isn't installed.");
                await Dialogs.Installed.ShowAsync();
                return;
            }

            if (Game.Unpackaged)
            {
                Log.Current.Write("Minecraft is unpackaged.");
                await Dialogs.Unpackaged.ShowAsync();
                return;
            }

            ListBox.IsEnabled = default;
            Installation.Install.Visibility = Visibility.Collapsed;
            Installation.Progress.Visibility = Installation.Cancel.Visibility = Visibility.Visible;

            using (Request = await Catalog.InstallAsync((string)ListBox.SelectedItem, (_) => Dispatcher.Invoke(() =>
            {
                if (Installation.Progress.Value == _) return;
                if (Installation.Progress.IsIndeterminate) Installation.Progress.IsIndeterminate = false;
                Installation.Progress.Value = _;
            })))
            {
                Log.Current.Write($"An installation request has created for {ListBox.SelectedItem}.");
                Installation.Cancel.IsEnabled = true;
                await Request; Request = default;
                Log.Current.Write($"An installation request has finished for {ListBox.SelectedItem}.");
            }

            Installation.Progress.Value = default;
            Installation.Cancel.IsEnabled = default;
            Installation.Install.Visibility = Visibility.Visible;
            Installation.Progress.Visibility = Installation.Cancel.Visibility = Visibility.Collapsed;
            Installation.Progress.IsIndeterminate = ListBox.IsEnabled = true;
        };

        Installation.Cancel.Click += async (_, _) =>
        {
            Installation.Cancel.IsEnabled = default;
            Log.Current.Write($"An installation request has been canceled for {ListBox.SelectedItem}.");
            await Task.Run(Request.Cancel);
        };

        Application.Current.Exit += (_, _) => { using (Request) Request?.Cancel(); };
    }
}