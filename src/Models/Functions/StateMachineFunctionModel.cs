using Newtonsoft.Json;

namespace Models.Functions
{
    public class StateMachineFunctionModel
    {
        [JsonProperty("numberOfElements")]
        public int NumberOfElements { get; set; }

        [JsonProperty("colorHex")]
        public string ColorHex { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }
}