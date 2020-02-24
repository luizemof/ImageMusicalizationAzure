using Models.Extraction.KMeans;

namespace Service.Extraction
{
    public interface IExtractionService
    {
        KMeansExtractionResult KMeansExtraction(string imageBase64, int seeds);
    }
}