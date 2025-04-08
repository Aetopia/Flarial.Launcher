using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Flarial.Launcher.App;

[DataContract]
internal sealed class Configuration
{
    internal enum Builds { Release, Beta, Custom }

    [DataMember]
    internal Builds Build = Builds.Release;

    [DataMember]
    internal bool Desktop = default;

    static readonly DataContractJsonSerializer Serializer = new(typeof(Configuration), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true });

    internal static Configuration Get()
    {
        try
        {
            using var stream = File.OpenRead("Configuration.json");
            var value = (Configuration)Serializer.ReadObject(stream);

            if (!Enum.IsDefined(typeof(Builds), value.Build)) value.Build = default;
            return value;
        }
        catch { }
        return new();
    }

    internal void Save()
    {
        using var stream = File.Create("Configuration.json");
        Serializer.WriteObject(stream, this);
    }
}