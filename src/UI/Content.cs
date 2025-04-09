using Flarial.Launcher.SDK;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI;

sealed class Content : NavigationView
{
    internal readonly Pages.Home Home;

    internal Pages.Versions Versions;

    internal readonly Pages.Settings Settings = new();

    internal Content()
    {
        IsSettingsVisible = IsEnabled = default;
        IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
        Content = Home = new(this);

        MenuItems.Add(new NavigationViewItem
        {
            Icon = new SymbolIcon(Symbol.Home),
            Content = "Home",
            Tag = Symbol.Home,
            IsSelected = true
        });

        MenuItems.Add(new NavigationViewItem
        {
            Icon = new SymbolIcon(Symbol.Library),
            Content = "Versions",
            Tag = Symbol.Library,
        });

        FooterMenuItems.Add(new NavigationViewItem
        {
            Icon = new SymbolIcon(Symbol.Setting),
            Content = "Settings",
            Tag = Symbol.Setting,
        });

        ItemInvoked += (_, args) =>
        {
            switch ((Symbol)args.InvokedItemContainer.Tag)
            {
                case Symbol.Home:
                    Content = Home;
                    break;

                case Symbol.Library:
                    Content = Versions;
                    break;

                case Symbol.Setting:
                    Content = Settings;
                    break;
            }
        };
    }
}