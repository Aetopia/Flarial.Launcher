using System.Windows;
using System.Windows.Media;
using Flarial.Launcher.App;
using ModernWpf.Controls;
using Modern = ModernWpf.Controls;
using System.Windows.Controls;
using System.Threading.Tasks;
using Flarial.Launcher.SDK;
using Bedrockix.Windows;
using Bedrockix.Minecraft;

namespace Flarial.Launcher.UI.Pages;

sealed class Home : Grid
{
    readonly Image Image = new()
    {
        Source = Manifest.Icon,
        Width = Manifest.Icon.Width / 2,
        Height = Manifest.Icon.Height / 2,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new(0, 0, 0, 75)
    };

    readonly Modern.ProgressBar ProgressBar = new()
    {
        Width = Manifest.Icon.Width * 2,
        Foreground = new SolidColorBrush(Colors.White),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        IsIndeterminate = true,
        Margin = new(0, 125, 0, 0),
        Visibility = Visibility.Visible
    };

    readonly TextBlock TextBlock = new()
    {
        Text = "Preparing...",
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new(0, 175, 0, 0),
        Visibility = Visibility.Visible
    };

    readonly Button Button = new()
    {
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        Content = new SymbolIcon(Symbol.Play),
        Width = Manifest.Icon.Width * 2,
        Margin = new(0, 125, 0, 0),
        Visibility = Visibility.Collapsed
    };

    internal Home(Content @this)
    {
        Children.Add(Image);
        Children.Add(ProgressBar);
        Children.Add(TextBlock);
        Children.Add(Button);

        Button.Click += async (_, _) =>
        {
            if (!Game.Installed)
            {
                Log.Current.Write("Minecraft isn't installed.");
                await Dialogs.Installed.ShowAsync();
                return;
            }

            var _ = Configuration.Current;
            Button.Visibility = Visibility.Collapsed;
            ProgressBar.Visibility = TextBlock.Visibility = Visibility.Visible;

            if (_.Build is Build.Release or Build.Beta)
            {
                Log.Current.Write($"Trying to launch {(_.Build is Build.Beta ? "Beta" : "Release")}.");

                await Client.DownloadAsync(_.Build is Build.Beta, (_) => Dispatcher.Invoke(() =>
                {
                    if (ProgressBar.Value == _) return;
                    if (ProgressBar.IsIndeterminate) ProgressBar.IsIndeterminate = false;
                    TextBlock.Text = $"Downloading.. {ProgressBar.Value = _}%";
                }));

                TextBlock.Text = "Launching..."; ProgressBar.IsIndeterminate = true;

                Log.Current.Write($"Launching Minecraft with Flarial Client.");
                await Client.LaunchAsync(_.Build is Build.Beta);
            }
            else
            {
                Log.Current.Write($"Trying to use '{_.Custom} as the custom DLL.'");

                if (!string.IsNullOrEmpty(_.Custom))
                {
                    Library value = new(_.Custom);
                    if (value.Valid)
                    {
                        TextBlock.Text = "Launching...";

                        Log.Current.Write($"Launching Minecraft with Custom DLL.");
                        await Task.Run(() => Loader.Launch(value));
                    }
                    else await Dialogs.Loader.ShowAsync();
                }
                else await Dialogs.Loader.ShowAsync();
            }

            Button.Visibility = Visibility.Visible;
            ProgressBar.Visibility = TextBlock.Visibility = Visibility.Collapsed;

            TextBlock.Text = "Preparing...";
            ProgressBar.Value = default;
            ProgressBar.IsIndeterminate = true;
        };

        Application.Current.MainWindow.ContentRendered += async (_, _) =>
        {
            await Task.Run(() => _ = Configuration.Current);

            Log.Current.Write("Acquire the latest version catalog.");
            var catalog = await Catalog.GetAsync();

            Button.Visibility = Visibility.Visible;
            ProgressBar.Visibility = TextBlock.Visibility = Visibility.Collapsed;

            TextBlock.Text = "Preparing...";
            ProgressBar.Value = default;
            ProgressBar.IsIndeterminate = true;

            @this.Versions = new(catalog);
            @this.Settings = new();
            @this.IsEnabled = true;

            Log.Current.Write("The launcher is now ready to use.");
        };
    }
}