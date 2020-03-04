using Newtonsoft.Json;

namespace Models.Functions
{
    public class ImageMusicalizationFunctionModel
    {
        [JsonProperty("imageBase64")]
        public string ImageBase64 { get; set; }
    }
}