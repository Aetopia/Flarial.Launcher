using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Flarial.Launcher.App;

static class Embedded
{
    static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    internal static readonly ImageSource Icon;

    static Embedded()
    {
        using var stream = Assembly.GetManifestResourceStream("app.ico");
        Icon = BitmapFrame.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
    }
}