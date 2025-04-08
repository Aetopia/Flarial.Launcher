using System;
using System.IO;
using System.Runtime.Serialization;
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
    }

    [DataMember]
    internal string Custom
    {
        get { lock (_) return field; }
        set { lock (_) field = value; }
    }

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
                        using var stream = File.OpenRead("Configuration.xml");
                        Object = (Configuration)Serializer.ReadObject(stream);
                        if (!Enum.IsDefined(typeof(Build), Object.Build)) Object.Build = default;
                    }
                    catch (Exception _)
                    {
                        Log.Current.Write($"{_}");
                        Object = new();
                    }

                    Log.Current.Write($"Configuration is \"Build: {Object.Build}, Debug: {Object.Debug}, Custom: '{Object.Custom}'\".");
                }
            }
            return Object;
        }
    }

    internal static void Save()
    {
        Log.Current.Write($"Configuration is \"Build: {Object.Build}, Debug: {Object.Debug}, Custom: '{Object.Custom}'\".");
        using var writer = XmlWriter.Create("Configuration.xml", Settings);
        Serializer.WriteObject(writer, Current);
    }

    static Configuration Object;

    static readonly object _ = new();

    static readonly DataContractSerializer Serializer = new(typeof(Configuration));

    static readonly XmlWriterSettings Settings = new() { Indent = true };
}