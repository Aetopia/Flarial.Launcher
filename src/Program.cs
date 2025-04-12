using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
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
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Flarial\Launcher");
        Directory.CreateDirectory(path); Environment.CurrentDirectory = path;

        using (new Mutex(default, "BC3F9461-563D-4EBD-982D-7AE54C80310C", out var value))
        {
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