using Newtonsoft.Json;

namespace Models.Functions
{
    public class ImageMusicalizationModel
    {
        [JsonProperty("imageBase64")]
        public string ImageBase64 { get; set; }

        [JsonProperty("seed")]
        public int Seed { get; set; }
    }
}