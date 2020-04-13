using Newtonsoft.Json;

namespace PricePrediction_API.Models
{
    public class PredictResultModel
    {
        [JsonProperty("Results")]
        public Results Results { get; set; }
    }

    public class Results
    {
        [JsonProperty("output1")]
        public PredictionOutput output { get; set; }
    }

    public class PredictionOutput
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("value")]
        public value value { get; set; }
    }

    public class value
    {
        [JsonProperty("ColumnNames")]
        public string[] ColumnNames { get; set; }

        [JsonProperty("ColumnTypes")]
        public string[] ColumnTypes { get; set; }

        [JsonProperty("Values")]
        public string[][] Values { get; set; }
    }
}