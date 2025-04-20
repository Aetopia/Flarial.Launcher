using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Flarial.Launcher.App;
using ModernWpf;
using ModernWpf.Controls;

namespace Flarial.Launcher;

static class Program
{
    [STAThread]
    static void Main()
    {
        using (new Mutex(default, "54874D29-646C-4536-B6D1-8E05053BE00E", out var value))
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            Environment.CurrentDirectory = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Flarial\Launcher")).FullName;
            if (!value) return; _ = Logger.Current;

            Application application = new();
            application.Exit += (_, _) => Configuration.Save();

            application.Resources.MergedDictionaries.Add(new ThemeResources());
            application.Resources.MergedDictionaries.Add(new XamlControlsResources());
            application.Resources.MergedDictionaries.Add(new ColorPaletteResources { TargetTheme = ApplicationTheme.Dark, Accent = Colors.IndianRed, AltHigh = Colors.Red });

            application.Run(new UI.Window());
        }
    }
}