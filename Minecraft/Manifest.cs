using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OkkamiMaker
{
    public class Manifest
    {
        // Estructura del manifiesto
        public class ManifestData
        {
            [JsonPropertyName("format_version")]
            public int FormatVersion { get; set; }

            [JsonPropertyName("header")]
            public HeaderData Header { get; set; }

            [JsonPropertyName("modules")]
            public List<ModuleData> Modules { get; set; }

            [JsonPropertyName("dependencies")]
            public List<DependencyData> Dependencies { get; set; }

            // Constructor para inicializar los campos esenciales
            public ManifestData()
            {
                FormatVersion = 2;
                Header = new HeaderData
                {
                    UUID = Guid.NewGuid().ToString(),
                    Version = new List<int> { 1, 0, 0 },
                    MinEngineVersion = new List<int> { 1, 17, 0 }
                };
                Modules = new List<ModuleData>
                {
                    new ModuleData
                    {
                        Type = "resources",
                        UUID = Guid.NewGuid().ToString(),
                        Version = new List<int> { 1, 0, 0 }
                    }
                };
                Dependencies = new List<DependencyData>
                {
                    new DependencyData
                    {
                        UUID = Guid.NewGuid().ToString(),
                        Version = new List<int> { 1, 0, 0 }
                    }
                };
            }
        }

        // Estructura del "header"
        public class HeaderData
        {
            [JsonPropertyName("uuid")]
            public string UUID { get; set; }

            [JsonPropertyName("version")]
            public List<int> Version { get; set; }

            [JsonPropertyName("min_engine_version")]
            public List<int> MinEngineVersion { get; set; }
        }

        // Estructura de "modules"
        public class ModuleData
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("uuid")]
            public string UUID { get; set; }

            [JsonPropertyName("version")]
            public List<int> Version { get; set; }
        }

        // Estructura de "dependencies"
        public class DependencyData
        {
            [JsonPropertyName("uuid")]
            public string UUID { get; set; }

            [JsonPropertyName("version")]
            public List<int> Version { get; set; }
        }

        // Método para generar el manifiesto
        public static string GenerateManifest()
        {
            // Crear la estructura del manifiesto
            ManifestData manifest = new ManifestData();

            // Convertir el manifiesto a JSON
            string json = JsonSerializer.Serialize(manifest, new JsonSerializerOptions { WriteIndented = true });
            return json;
        }
    }
}
