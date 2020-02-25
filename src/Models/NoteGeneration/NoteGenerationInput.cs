using System.Drawing;
using SkiaSharp;

namespace Models.NoteGeneration
{
    public class NoteGenerationInput
    {
        public SKColor Pixel { get; private set; }

        public NoteGenerationInput(SKColor pixel)
        {
            Pixel = pixel;
        }
    }
}