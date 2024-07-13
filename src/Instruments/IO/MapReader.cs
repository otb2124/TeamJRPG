using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;

namespace TeamJRPG
{
    public class MapReader
    {
        private string GetJsonDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] directories = baseDirectory.TrimEnd('\\').Split('\\');
            int startIndex = Math.Max(0, directories.Length - 3);
            string remainingPath = string.Join("\\", directories.Take(startIndex));
            string jsonDirectory = Path.Combine(remainingPath, "Content", "res", "json");

            if (!Directory.Exists(jsonDirectory))
            {
                Directory.CreateDirectory(jsonDirectory);
            }

            return jsonDirectory;
        }

        public void WriteMap(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Writing map to {jsonDirectory}");

            try
            {
                

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                string json = JsonConvert.SerializeObject(Globals.map, settings);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Map data successfully written to {jsonDirectory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing map data to {filePath}: {ex.Message}");
            }
        }

        public void ReadMap(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Reading map from {fileName}");

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    Globals.map = JsonConvert.DeserializeObject<Map>(json, settings);

                    Console.WriteLine($"Map data successfully read from {jsonDirectory}");
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading map data from {filePath}: {ex.Message}");
            }
        }
    }
}
