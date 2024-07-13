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






        public void WriteEntities(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Writing entities data to {jsonDirectory}");

            try
            {
                var entitiesToSerialize = Globals.entities.ToList();

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                string json = JsonConvert.SerializeObject(entitiesToSerialize, settings);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Entities data successfully written to {jsonDirectory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing entities data to {filePath}: {ex.Message}");
            }
        }



        public void ReadEntities(string fileName)
        {
            string jsonDirectory = GetJsonDirectory();
            string filePath = Path.Combine(jsonDirectory, fileName);
            Console.WriteLine($"Reading entities data from {fileName}");

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

                    var entities = JsonConvert.DeserializeObject<List<dynamic>>(json, settings);

                    foreach (var entity in entities)
                    {
                        if (entity.entityType == Entity.EntityType.groupMember)
                        {
                            // Deserialize as GroupMember
                            var groupMember = JsonConvert.DeserializeObject<GroupMember>(entity.ToString(), settings);
                            Globals.group.members.Add(groupMember);

                            if (groupMember.isPlayer)
                            {
                                Globals.player = groupMember;
                            }

                            Globals.entities.Add(groupMember);
                        }
                        else if (entity.entityType == Entity.EntityType.obj)
                        {
                            // Deserialize as Object
                            var obj = JsonConvert.DeserializeObject<Object>(entity.ToString(), settings);
                            // Handle objects if needed
                            Globals.entities.Add(obj);
                        }
                        else
                        {
                            // Default case: Deserialize as Entity
                            var baseEntity = JsonConvert.DeserializeObject<Entity>(entity.ToString(), settings);
                            // Handle other entities if needed
                        }
                    }

                    Console.WriteLine($"Entities data successfully read from {jsonDirectory}");

                    // Ensure Globals.player is set
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
                Console.WriteLine($"Error reading entities data from {filePath}: {ex.Message}");
            }
        }



    }
}
