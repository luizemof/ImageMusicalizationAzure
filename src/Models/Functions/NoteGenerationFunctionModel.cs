using Newtonsoft.Json;

namespace Models.Functions
{
    public class NoteGenerationFunctionModel
    {
        [JsonProperty("imageBase64")]
        public string ImageBase64 { get; set; }
    }
}