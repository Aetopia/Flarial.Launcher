using System.Threading.Tasks;
using Bedrockix.Minecraft;
using ModernWpf.Controls;

namespace Flarial.Launcher.UI;

static class Dialogs
{
    static readonly ContentDialog Installed = new()
    {
        Title = "Minecraft isn't installed.",
        Content = "Looks like Minecraft isn't installed.\nPlease install it from your preferred source.",
        CloseButtonText = "Close"
    };

    static readonly ContentDialog Unpackaged = new()
    {
        Title = "Minecraft isn't installed normally.",
        Content = @"Looks like Minecraft isn't installed normally.
• Reinstall it from the Microsoft Store and try again.
• Use the program that you used to switch versions.",
        CloseButtonText = "Close"
    };

    static readonly ContentDialog Loader = new()
    {
        Title = "We couldn't inject the custom DLL.",
        Content = "Looks like the custom DLL doesn't exist or is invalid.",
        CloseButtonText = "Close"
    };

    static readonly ContentDialog Unsupported = new()
    {
        Title = "We don't support this version of Minecraft.",
        Content = @"Looks like the currently installed version of Minecraft is unsupported.
• Wait for the list of supported versions to be updated.
• Switch to a version of Minecraft that Flarial Client supports.
• Try restarting the launcher to refresh the list of supported versions.",
        CloseButtonText = "Close"
    };

    internal static async Task InstalledAsync()
    {
        await Logger.WarningAsync("Minecraft isn't installed.");
        await Installed.ShowAsync();
    }

    internal static async Task UnpackagedAsync()
    {
        await Logger.WarningAsync("Minecraft is unpackaged.");
        await Unpackaged.ShowAsync();
    }

    internal static async Task LoaderAsync()
    {
        await Logger.WarningAsync("The specified custom DLL is invalid.");
        await Loader.ShowAsync();
    }

    internal static async Task UnsupportedAsync()
    {
        await Task.Run(() => Logger.Warning($"Minecraft {Metadata.Version} is unsupported."));
        await Unsupported.ShowAsync();
    }
}