using Service.Extraction;
using Service.NoteGeneration;
using Service.StateMachine;

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

        public static IStateMachineService CreateStateMachineService()
        {
            return new StateMachineService();
        }
    }
}