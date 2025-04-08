using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Flarial.Launcher.App;

static class Configuration
{
    static readonly DataContractJsonSerializer Serializer = new(typeof(Dictionary<string, object>), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true });

    internal static void Serialize(Dictionary<string, object> value)
    {
        using var stream = File.Create("Flarial.Launcher.json");
        Serializer.WriteObject(stream, value);
    }

    internal static Dictionary<string, object> Deserialize()
    {
        using var stream = File.OpenRead("Flarial.Launcher.json");
        return (Dictionary<string, object>)Serializer.ReadObject(stream);
    }
}