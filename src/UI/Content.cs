using ModernWpf.Controls;

namespace Flarial.Launcher.UI;

sealed class Content : NavigationView
{
    readonly Pages.Settings Settings;

    internal Content()
    {
        IsSettingsVisible = default;
        IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        PaneDisplayMode = NavigationViewPaneDisplayMode.Top;

        Settings = new();

        MenuItems.Add(new NavigationViewItem
        {
            Icon = new SymbolIcon(Symbol.Home),
            Content = "Home",
            Tag = Symbol.Home,
            IsSelected = true
        });

        MenuItems.Add(new NavigationViewItem
        {
            Icon = new SymbolIcon(Symbol.List),
            Content = "Versions",
            Tag = Symbol.List,
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
                case Symbol.Home: break;

                case Symbol.List:
                    break;

                case Symbol.Setting:
                    Content = Settings;
                    break;
            }
        };
    }
}