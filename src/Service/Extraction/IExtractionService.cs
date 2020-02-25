using System.Collections.Generic;
using Models.Extraction.KMeans;

namespace Service.Extraction
{
    public interface IExtractionService
    {
        IEnumerable<KMeansExtractionResult> KMeansExtraction(string imageBase64, int seeds);
    }
}