using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamJRPG
{
    public class MapReader
    {


        private string GetResDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] directories = baseDirectory.TrimEnd('\\').Split('\\');
            int startIndex = Math.Max(0, directories.Length - 3);
            string remainingPath = string.Join("\\", directories.Take(startIndex));
            string resDirectory = Path.Combine(remainingPath, "Content", "res");

            if (!Directory.Exists(resDirectory))
            {
                Directory.CreateDirectory(resDirectory);
            }

            return resDirectory;
        }


        private string GetJsonDirectory()
        {
            return Path.Combine(GetResDirectory(), "json");
        }

        private string GetOutputDirectory()
        {
            return Path.Combine(GetResDirectory(), "consoleoutput");
        }







        public void WriteMaps(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Writing maps to {jsonDirectory}");

            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    TypeNameHandling = TypeNameHandling.Auto,
                    Converters = new List<JsonConverter> { new TileArrayConverter() }
                };

                string json = JsonConvert.SerializeObject(Globals.maps, settings);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Maps data successfully written to {jsonDirectory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing maps data to {filePath}: {ex.Message}");
            }
        }

        public void ReadMaps(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Reading maps data from {fileName}");

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        TypeNameHandling = TypeNameHandling.Auto,
                        Converters = new List<JsonConverter> { new TileArrayConverter() }
                    };

                    var maps = JsonConvert.DeserializeObject<Map[]>(json, settings);

                    foreach (var map in maps)
                    {

                        foreach (var entity in map.entities) // ToList() creates a copy to avoid modification during iteration
                        {
                            if (entity.entityType == Entity.EntityType.groupMember)
                            {

                                if (((GroupMember)entity).isPlayer)
                                {
                                    Globals.player = ((GroupMember)entity);
                                    Globals.currentMap = map; // Consider whether this assignment is needed here
                                }

                                Globals.group.members.Add(((GroupMember)entity));
                            }
                        }
                    }

                    Globals.maps = maps;

                    Console.WriteLine($"Maps data successfully read from {jsonDirectory}");

                    if (Globals.player == null)
                    {
                        Console.WriteLine("Player not found in entities data.");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading maps data from {filePath}: {ex.Message}");
            }
        }



    }
}
