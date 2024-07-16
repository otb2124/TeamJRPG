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
            Console.WriteLine($"Writing maps to {filePath}");

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

                // Create the wrapper object containing both maps and group
                var gameData = new GameData
                {
                    Maps = Globals.maps,
                    Group = Globals.group
                };

                // Serialize the wrapper object
                string json = JsonConvert.SerializeObject(gameData, settings);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Maps and group data successfully written to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing maps and group data to {filePath}: {ex.Message}");
            }
        }


        public void ReadMaps(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Reading maps and group data from {fileName}");

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

                    // Deserialize the wrapper object
                    var gameData = JsonConvert.DeserializeObject<GameData>(json, settings);

                    var maps = gameData.Maps;

                    foreach (var map in maps)
                    {
                        foreach (var entity in map.entities)
                        {
                            if (entity.entityType == Entity.EntityType.groupMember)
                            {
                                if (((GroupMember)entity).isPlayer)
                                {
                                    Globals.player = (GroupMember)entity;
                                    Globals.currentMap = map; // Consider whether this assignment is needed here
                                }

                                Globals.group.members.Add((GroupMember)entity);
                            }
                        }
                    }

                    Globals.maps = maps;


                    Globals.group.inventory = gameData.Group.inventory;
                    Globals.group.actualQuests = gameData.Group.actualQuests;

                    Console.WriteLine($"Maps and group data successfully read from {filePath}");

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
                Console.WriteLine($"Error reading maps and group data from {filePath}: {ex.Message}");
            }
        }







        public void WriteConfig()
        {
            string fileName = "config";
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Writing config to {jsonDirectory}");

            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    TypeNameHandling = TypeNameHandling.Auto
                };

                // Serialize the Globals.config object
                string json = JsonConvert.SerializeObject(Globals.config, settings);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Config successfully written to {jsonDirectory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing config to {filePath}: {ex.Message}");
            }
        }


        public void ReadConfig()
        {
            string fileName = "config";
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Reading config from {fileName}");

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    // Deserialize the Globals.config object
                    Globals.config = JsonConvert.DeserializeObject<Configuration>(json, settings);

                    Console.WriteLine($"Config successfully read from {filePath}");
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config from {filePath}: {ex.Message}");
            }

        }
    }
}
