using Models.NoteGeneration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models.ImageMusicalization
{
    public class ImageMusicalizationModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ENote Note { get; set; }
    }
}