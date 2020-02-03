using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Models.Extraction.KMeans;

namespace Extraction.KMeans
{
    public class KMeansExtraction
    {
        private readonly string ImageFile;
        private readonly int Seed;

        public KMeansExtraction(string imageFile, int seed)
        {
            ImageFile = imageFile;
            Seed = seed;
        }

        public KMeansExtractionResult Run()
        {
            if (string.IsNullOrWhiteSpace(ImageFile))
                throw new NullReferenceException(nameof(ImageFile));

            var image = Image.FromFile(ImageFile);
            var bitMap = new Bitmap(Image.FromFile(ImageFile));
            Console.Write(bitMap.GetPixel(12, 12));

            var kMeansExtractionResult = new KMeansExtractionResult(null);

            return kMeansExtractionResult;
        }
    }
}