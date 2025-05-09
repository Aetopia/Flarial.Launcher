using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Interop;
using System.Windows.Media;
using System.Xml;
using Bedrockix.Minecraft;

namespace Flarial.Launcher.App;

enum Build { Release, Beta, Custom }

[DataContract]
sealed class Configuration
{
    [DataMember]
    internal Build Build
    {
        get { lock (_) return field; }
        set { lock (_) field = value; }
    }

    [DataMember]
    internal bool Debug
    {
        get { lock (_) return field; }
        set { lock (_) { field = value; if (Game.Installed) Game.Debug = field; } }
    } = true;

    [DataMember]
    internal string Custom
    {
        get { lock (_) return field; }
        set { lock (_) field = value; }
    }

    [DataMember]
    internal bool Hardware
    {
        get { lock (_) return field; }
        set { lock (_) RenderOptions.ProcessRenderMode = (field = value) ? RenderMode.Default : RenderMode.SoftwareOnly; }
    } = true;

    internal static Configuration Current
    {
        get
        {
            lock (_)
            {
                if (Object is null)
                {
                    try
                    {
                        using var stream = File.OpenRead("Flarial.Launcher.xml");
                        Object = (Configuration)Serializer.ReadObject(stream);
                        if (!Enum.IsDefined(typeof(Build), Object.Build)) Object.Build = default;
                    }
                    catch { Object = new(); }
                }
            }
            return Object;
        }
    }

    internal static void Save()
    {
        using var writer = XmlWriter.Create("Flarial.Launcher.xml", Settings);
        Serializer.WriteObject(writer, Current);
    }

    static Configuration Object;

    static readonly object _ = new();

    static readonly DataContractSerializer Serializer = new(typeof(Configuration));

    static readonly XmlWriterSettings Settings = new() { Indent = true };
}