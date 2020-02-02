using System;
using Models.Extraction.KMeans;

namespace Extraction.KMeans
{
    public class KMeansExtraction
    {
        private readonly string ImageFile;

        public KMeansExtraction(string imageFile)
        {
            ImageFile = imageFile;
        }

        public KMeansExtractionResult Run()
        {
            if(string.IsNullOrWhiteSpace(ImageFile))
                throw new NullReferenceException(nameof(ImageFile));

            var kMeansExtractionResult = new KMeansExtractionResult();

            return kMeansExtractionResult;
        }
    }
}