using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fotos_Etiquetas
{
    public partial class Foto
    {
        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }

        [JsonProperty("odata.count")]
        public long OdataCount { get; set; }

        [JsonProperty("value")]
        public List<Value> Value { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("foto")]
        public string Foto { get; set; }

        [JsonProperty("cfoto")]
        public long Cfoto { get; set; }

        [JsonProperty("desc_espec")]
        public string DescEspec { get; set; }
    }

    public partial class Foto
    {
        public static Foto FromJson(string json) => JsonConvert.DeserializeObject<Foto>(json, Fotos_Etiquetas.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Foto self) => JsonConvert.SerializeObject(self, Fotos_Etiquetas.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
