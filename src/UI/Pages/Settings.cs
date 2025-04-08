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

    readonly Configuration Configuration;

    internal Settings(Configuration configuration)
    {
        Configuration = configuration;

        Spacing = 12; Margin = new(12);

        Build.Items.Add("Release");
        Build.Items.Add("Beta");
              Build.Items.Add("Custom");
        Build.SelectedIndex = (int)Configuration.Build;

        Desktop.IsOn = Configuration.Desktop;

        Children.Add(Build);
        Children.Add(Desktop);

        Build.SelectionChanged += (_, _) =>
        {
            var index = Build.SelectedIndex; if (index is -1) return;
            Configuration.Build = (Configuration.Builds)index;
        };

        Desktop.Toggled += (_, _) => Configuration.Desktop = Desktop.IsOn;

        Application.Current.Exit += (_, _) => Configuration.Save();
    }
}