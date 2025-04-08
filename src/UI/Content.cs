using System.Threading.Tasks;
using System.Windows;
using Flarial.Launcher.App;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI;

sealed class Content : NavigationView
{
    internal object Home = default;

    internal object Versions = default;

    internal Pages.Settings Settings;

    internal Content()
    {
        IsSettingsVisible = default;
        IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        PaneDisplayMode = NavigationViewPaneDisplayMode.Top;

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
                case Symbol.Home:
                    Content = Home;
                    break;

                case Symbol.List:
                    Content = Versions;
                    break;

                case Symbol.Setting:
                    Content = Settings;
                    break;
            }
        };

        Application.Current.MainWindow.ContentRendered += async (_, _) =>
        {
            Settings = new(await Task.Run(() => Configuration.Get()));
            IsEnabled = true;
        };
    }
}