using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Flarial.Launcher.App;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI.Pages;

sealed class Settings : SimpleStackPanel
{
    readonly RadioButtons Builds = new()
    {
        Header = "Select what dynamic link library should be used:",
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    readonly ToggleSwitch Suspension = new()
    {
        Header = "Prevent the game from being suspended when minimized.",
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };

    readonly Dictionary<string, object> Graph = new() { ["Build"] = "Release", ["Suspension"] = false };

    internal Settings()
    {
        Spacing = 12;
        Margin = new(12);

        Builds.Items.Add("Release");
        Builds.Items.Add("Beta");
        Builds.SelectedIndex = default;

        Children.Add(Builds);
        Children.Add(Suspension);

        Builds.SelectionChanged += (_, _) =>
        {
            var index = Builds.SelectedIndex; if (index is -1) return;
            Graph["Build"] = Builds.Items[index];
        };

        Suspension.Toggled += (_, _) => Graph["Suspension"] = Suspension.IsOn;

        Application.Current.Exit += (_, _) => Configuration.Serialize(Graph);
    }
}