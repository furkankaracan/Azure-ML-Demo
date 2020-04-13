namespace PricePrediction_API.Models
{
    public class CarModel
    {
        public string selectedModel { get; set; }
        public string selectedBody { get; set; }
        public int engineSize { get; set; }
        public int horsePower { get; set; }
        public int wheelBase { get; set; }
        public int peakRpm { get; set; }
        public int highwayMpg { get; set; }
        public string price { get; set; }

    }
}