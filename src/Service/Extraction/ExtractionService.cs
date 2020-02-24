using Models.Extraction.KMeans;
using Service.Extraction.KMeans;
using System;

namespace Service.Extraction
{
    internal class ExtractionService : IExtractionService
    {
        public KMeansExtractionResult KMeansExtraction(string imageBase64, int seeds)
        {
            KMeansExtractionResult kMeansExtractionResult;
            var imageBytes = ConvertImageBase64ToBytes(imageBase64);
            using (var kMeansExtraction = new KMeansExtraction(imageBytes, seeds))
            {
                kMeansExtractionResult = kMeansExtraction.Run();
            }

            return kMeansExtractionResult;
        }

        private byte[] ConvertImageBase64ToBytes(string imageBase64)
        {
            return Convert.FromBase64String(imageBase64);
        }
    }
}