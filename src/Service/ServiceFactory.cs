using Service.Extraction;
using Service.NoteGeneration;

namespace Service
{
    public static class ServiceFactory
    {
        public static IExtractionService CreateExtractionService()
        {
            return new ExtractionService(); 
        }

        public static INoteGenerationService CreateNoteGenerationService()
        {
            var extractionService = CreateExtractionService();
            return new NoteGenerationService(extractionService);
        }
    }
}