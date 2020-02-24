using Service.Extraction;

namespace Service
{
    public static class ServiceFactory
    {
        public static IExtractionService CreateKMeansService()
        {
            return new ExtractionService(); 
        }
    }
}