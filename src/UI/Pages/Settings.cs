using System.Windows;
using Flarial.Launcher.App;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI.Pages;

sealed class Settings : SimpleStackPanel
{
    readonly RadioButtons Build = new()
    {
        Header = "Select what DLL should be used:",
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    readonly ToggleSwitch Debug = new()
    {
        Header = "Prevent the game from being suspended when minimized.",
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    readonly Controls.Custom Custom = new()
    {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    internal Settings()
    {
        var _ = Configuration.Current;

        Spacing = (Margin = new(12)).Left;

        Build.Items.Add("Release");
        Build.Items.Add("Beta");
        Build.Items.Add("Custom");

        Children.Add(Build);
        Children.Add(Custom);
        Children.Add(Debug);

        Build.SelectionChanged += (_, _) =>
        {
            var index = Build.SelectedIndex; if (index is -1) return;
            Custom.IsEnabled = (_.Build = (Build)index) is App.Build.Custom;
        };

        Debug.Toggled += (_, _) => { _.Debug = Debug.IsOn; };

        Build.SelectedIndex = (int)_.Build;
        Debug.IsOn = _.Debug;
    }
}