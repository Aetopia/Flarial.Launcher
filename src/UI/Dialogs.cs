using ModernWpf.Controls;

namespace Flarial.Launcher.UI;

static class Dialogs
{
    internal static readonly ContentDialog Installed = new()
    {
        Title = "Minecraft isn't installed.",
        Content = "Looks like Minecraft isn't installed.\nPlease install it from your preferred source.",
        CloseButtonText = "Close"
    };

    internal static readonly ContentDialog Unpackaged = new()
    {
        Title = "Minecraft isn't installed normally.",
        Content = @"Looks like Minecraft isn't installed normally.
• Reinstall it from the Microsoft Store and try again.
• Use the program that you used to switch versions.",
        CloseButtonText = "Close"
    };

    internal static readonly ContentDialog Loader = new()
    {
        Title = "We couldn't inject the custom DLL.",
        Content = "Looks like the custom DLL doesn't exist or is invalid.",
        CloseButtonText = "Close"
    };
}