using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using ModernWpf;
using ModernWpf.Controls;

namespace Flarial.Launcher;

static class Program
{
    [STAThread]
    static void Main()
    {
        Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        Application application = new();
       
        application.Resources.MergedDictionaries.Add(new ThemeResources());
        application.Resources.MergedDictionaries.Add(new XamlControlsResources());
        application.Resources.MergedDictionaries.Add(new ColorPaletteResources { TargetTheme = ApplicationTheme.Dark, Accent = Colors.IndianRed, AltHigh = Colors.Red });
        
        application.Run(new UI.Window());
    }
}