using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyOssHours.Backend.Specs.Tests.Shared;

internal class Settings
{
    internal static Settings Load()
    {
        if (!System.IO.File.Exists("specflow.json")) throw new Exception("specflow.json not found");

        var json = System.IO.File.ReadAllText("specflow.json");
        var settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(json);
        return settings ?? throw new SerializationException("Cannot deserialize specflow.json");
    }

    public required string Endpoint { get; set; }
}