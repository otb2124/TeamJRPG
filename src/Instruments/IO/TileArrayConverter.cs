using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class TileArrayConverter : JsonConverter<Tile[,]>
    {
        public override void WriteJson(JsonWriter writer, Tile[,] value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            for (int x = 0; x < value.GetLength(0); x++)
            {
                writer.WriteStartArray();
                for (int y = 0; y < value.GetLength(1); y++)
                {
                    serializer.Serialize(writer, value[x, y]);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }

        public override Tile[,] ReadJson(JsonReader reader, Type objectType, Tile[,] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var tilesList = new List<List<Tile>>();
            reader.Read();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var row = serializer.Deserialize<List<Tile>>(reader);
                tilesList.Add(row);
                reader.Read();
            }

            if (tilesList.Count == 0 || tilesList[0].Count == 0)
                return new Tile[0, 0];

            var tilesArray = new Tile[tilesList.Count, tilesList[0].Count];
            for (int x = 0; x < tilesList.Count; x++)
            {
                for (int y = 0; y < tilesList[x].Count; y++)
                {
                    tilesArray[x, y] = tilesList[x][y];
                }
            }

            return tilesArray;
        }
    }
}
