using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonSerialization
{
    [Serializable]
    public class JsonSample
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Content")]
        public JsonSampleContent Content { get; set; }
    }

    public class JsonSampleContent
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
