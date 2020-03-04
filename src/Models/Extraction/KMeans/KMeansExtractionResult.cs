using System.Drawing;
using SkiaSharp;

namespace Models.Extraction.KMeans
{
    public class KMeansExtractionResult
    {
        public Point Coordinator { get; }
        public SKColor Pixel { get; }
        public int NumberOfElements {get; }

        public KMeansExtractionResult(Point coordinator, SKColor pixel, int numberOfElements)
        {
            Coordinator = coordinator;
            Pixel = pixel;
            NumberOfElements = numberOfElements;
        }
    }
}