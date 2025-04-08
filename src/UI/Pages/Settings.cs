using System.Windows;
using Flarial.Launcher.App;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI.Pages;

sealed class Settings : SimpleStackPanel
{
    readonly RadioButtons Build = new()
    {
        Header = "Select what dynamic link library should be used:",
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    readonly ToggleSwitch Desktop = new()
    {
        Header = "Prevent the game from being suspended when minimized.",
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    internal Settings()
    {
        var configuration = Configuration.Current;

        Spacing = 12; Margin = new(12);

        Build.Items.Add("Release");
        Build.Items.Add("Beta");
        Build.SelectedIndex = (int)configuration.Build;

        Desktop.IsOn = configuration.Desktop;

        Children.Add(Build);
        Children.Add(Desktop);

        Build.SelectionChanged += (_, _) =>
        {
            var index = Build.SelectedIndex; if (index is -1) return;
            configuration.Build = (Builds)index;
        };

        Desktop.Toggled += (_, _) => configuration.Desktop = Desktop.IsOn;

        Application.Current.Exit += (_, _) => Configuration.Save();
    }
}