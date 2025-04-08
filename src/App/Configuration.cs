using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Flarial.Launcher.App;

enum Builds { Release, Beta }

[DataContract]
sealed class Configuration
{
    [DataMember]
    internal Builds Build = Builds.Release;

    [DataMember]
    internal bool Desktop = default;

    [DataMember]
    internal string Custom = string.Empty;

    static readonly DataContractSerializer Serializer = new(typeof(Configuration));

    static readonly XmlWriterSettings Settings = new() { Indent = true };

    static Configuration Object;

    internal static Configuration Current => Object ??= Get();

    static Configuration Get()
    {
        try
        {
            using var stream = File.OpenRead("Configuration.xml");
            var @this = (Configuration)Serializer.ReadObject(stream);

            if (!Enum.IsDefined(typeof(Builds), @this.Build)) @this.Build = default;
            return @this;
        }
        catch { }
        return new();
    }

    internal static void Save()
    {
        using var writer = XmlWriter.Create("Configuration.xml", Settings);
        Serializer.WriteObject(writer, Current);
    }
}