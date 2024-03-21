using System.Runtime.Serialization;

namespace MyOssHours.Backend.Specs.Tests.Api;

internal class Settings
{
    internal static Settings Load()
    {
        if (!File.Exists("specflow.json")) throw new Exception("specflow.json not found");

        var json = File.ReadAllText("specflow.json");
        var settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(json);
        return settings ?? throw new SerializationException("Cannot deserialize specflow.json");
    }

    public required string Endpoint { get; set; }
}