using Service.Extraction;

namespace Service
{
    public static class ServiceFactory
    {
        public static IExtractionService CreateExtractionService()
        {
            return new ExtractionService(); 
        }
    }
}