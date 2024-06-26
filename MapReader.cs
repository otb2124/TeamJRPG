using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TeamJRPG
{
    public class MapReader
    {



        public void WriteMap(string filePath)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Ignore circular references
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                string json = JsonConvert.SerializeObject(Globals.map, settings);

                //File.WriteAllText(filePath, json);

                Debug.WriteLine($"Map data successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error writing map data to {filePath}: {ex.Message}");
            }
        }
    }
}
