using System.Windows;
using System.Windows.Media;
using Flarial.Launcher.App;
using ModernWpf.Controls;
using Modern = ModernWpf.Controls;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Flarial.Launcher.UI.Pages;

sealed class Home : Grid
{
    internal Home()
    {
        Image image = new()
        {
            Source = Embedded.Icon,
            Width = Embedded.Icon.Width / 2,
            Height = Embedded.Icon.Height / 2,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new(0, 0, 0, 75),
        };

        Modern.ProgressBar progressBar = new()
        {
            Width = Embedded.Icon.Width * 2,
            Foreground = new SolidColorBrush(Colors.White),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsIndeterminate = true,
            Margin = new(0, 125, 0, 0),
            Visibility = Visibility.Collapsed
        };

        TextBlock textBlock = new()
        {
            Text = "Preparing...",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new(0, 175, 0, 0),
            Visibility = Visibility.Collapsed
        };

        Button button = new()
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Content = new SymbolIcon(Symbol.Play),
            Width = Embedded.Icon.Width * 2,
            Margin = new(0, 125, 0, 0),
        };

        SetRow(image, default); Children.Add(image);
        SetRow(progressBar, default); Children.Add(progressBar);
        SetRow(textBlock, default); Children.Add(textBlock);
        SetRow(button, default); Children.Add(button);

        button.Click += async (_, _) =>
        {
            button.Visibility = Visibility.Collapsed;
            progressBar.Visibility = textBlock.Visibility = Visibility.Visible;

            var build = Configuration.Current.Build;

            if (build is Builds.Release or Builds.Beta)
            {
                await SDK.Client.DownloadAsync(build is Builds.Beta, (_) => Dispatcher.Invoke(() =>
                {
                    if (progressBar.Value == _) return;
                    if (progressBar.IsIndeterminate) progressBar.IsIndeterminate = false;
                    textBlock.Text = $"Downloading.. {progressBar.Value = _}%";
                }));
            }

            button.Visibility = Visibility.Visible;
            progressBar.Visibility = textBlock.Visibility = Visibility.Collapsed;

            textBlock.Text = "Preparing";
            progressBar.Value = default;
            progressBar.IsIndeterminate = true;
        };
    }
}