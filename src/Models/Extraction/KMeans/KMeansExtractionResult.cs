using System.Drawing;
using SkiaSharp;

namespace Models.Extraction.KMeans
{
    public class KMeansExtractionResult
    {
        public Point Coordinator { get; }
        public SKColor Pixel { get; }

        public KMeansExtractionResult(Point coordinator, SKColor pixel)
        {
            Coordinator = coordinator;
            Pixel = pixel;
        }
    }
}