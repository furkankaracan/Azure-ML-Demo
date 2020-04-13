using Newtonsoft.Json;
using PricePrediction_API.Models;

namespace PricePrediction_API.Helpers
{
    public partial class PredictResultHelper
    {
        public static PredictResultModel FromJson(string json) => JsonConvert.DeserializeObject<PredictResultModel>(json, Converter.Settings);
    }

    static class Serialize {
        public static string ToJson(this PredictResultModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public static class Converter{
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None
        };
    }
}