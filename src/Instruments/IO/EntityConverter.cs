using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG.src.Instruments.IO
{
    public class EntityConverter : JsonConverter<Entity>
    {
        public override void WriteJson(JsonWriter writer, Entity value, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.FromObject(value);

            // Remove the "inventory" property from entities that are not GroupMember or are GroupMember but not isPlayer
            if (!(value is GroupMember groupMember && groupMember.isPlayer))
            {
                jsonObject.Remove("inventory");
            }

            jsonObject.WriteTo(writer);
        }

        public override Entity ReadJson(JsonReader reader, Type objectType, Entity existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => false;

        public override bool CanWrite => true;
    }

}
