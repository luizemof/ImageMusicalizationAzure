using System.Collections.Generic;
using Models.ImageMusicalization;

namespace Service.ImageMusicalization
{
    public interface IImageMusicalizationService
    {
        IEnumerable<ImageMusicalizationModel> CreateSongFromImage(string imageBase64);
    }
}