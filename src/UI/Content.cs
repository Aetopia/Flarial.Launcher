using Flarial.Launcher.SDK;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI;

sealed class Content : NavigationView
{
    internal Pages.Home Home;

    internal Pages.Settings Settings;

    internal Content()
    {
        IsSettingsVisible = IsEnabled = default;
        IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
        Content = new Startup(this);

        MenuItems.Add(new NavigationViewItem
        {
            Icon = new SymbolIcon(Symbol.Home),
            Content = "Home",
            Tag = Symbol.Home,
            IsSelected = true
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

                case Symbol.Setting:
                    Content = Settings;
                    break;
            }
        };
    }
}