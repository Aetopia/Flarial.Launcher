using System;
using System.Globalization;
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
    [DllImport("Shell32", CharSet = CharSet.Auto, SetLastError = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    static extern int ShellMessageBox(nint hAppInst = default, nint hWnd = default, string lpcText = default, string lpcTitle = "Error", int fuStyle = 0x00000010);

    [STAThread]
    static void Main()
    {
        Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        Log.Current.Write($"We are running on \"{Environment.OSVersion}\".");

        using (new Mutex(default, "BC3F9461-563D-4EBD-982D-7AE54C80310C", out var value))
        {
            if (!value)
            {
                Log.Current.Write($"The launcher is already running.");
                return;
            }
            else Log.Current.Write($"The launcher is been started.");

            Application application = new();
            application.Exit += (_, _) => Configuration.Save();

            application.Resources.MergedDictionaries.Add(new ThemeResources());
            application.Resources.MergedDictionaries.Add(new XamlControlsResources());
            application.Resources.MergedDictionaries.Add(new ColorPaletteResources { TargetTheme = ApplicationTheme.Dark, Accent = Colors.IndianRed, AltHigh = Colors.Red });

            UI.Window window = new();

            AppDomain.CurrentDomain.UnhandledException += (_, @this) =>
            {
                Log.Current.Write($"{@this.ExceptionObject}");
                ShellMessageBox(hWnd: new WindowInteropHelper(window).EnsureHandle(), lpcText: $"{@this.ExceptionObject}");
                Environment.Exit(default);
            };

            application.Run(window);

            Log.Current.Write("The launcher is now exiting.");
        }
    }
}