using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Flarial.Launcher.App;
using Flarial.Launcher.SDK;

namespace Flarial.Launcher.UI;

sealed class Startup : Grid
{
    internal Startup (Content @this)
    {
        RowDefinitions.Add(new());

        Image image = new()
        {
            Source = Embedded.Icon,
            Width = Embedded.Icon.Width / 2,
            Height = Embedded.Icon.Height / 2,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new(0, 0, 0, 75),
        };

        ProgressBar progressBar = new()
        {
            Width = Embedded.Icon.Width * 2,
            Foreground = new SolidColorBrush(Colors.White),
            VerticalAlignment = VerticalAlignment.Center,
            IsIndeterminate = true,
            Margin = new(0, 125, 0, 0),
        };

        TextBlock textBlock = new()
        {
            Text = "Preparing...",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new(0, 175, 0, 0),
        };

        SetRow(image, default); Children.Add(image);
        SetRow(progressBar, default); Children.Add(progressBar);
        SetRow(textBlock, default); Children.Add(textBlock);

        Application.Current.MainWindow.ContentRendered += async (_, _) =>
        {
            await Task.Run(() => _ = Configuration.Current);
            _ = await Catalog.GetAsync();
          
            @this.Settings = new();
            @this.Content = @this.Home = new();
            @this.IsEnabled = true;
        };
    }
}