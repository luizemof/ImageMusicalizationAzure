using Service.Extraction;
using Service.ImageMusicalization;
using Service.NoteGeneration;
using Service.SequenceGenerator;
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

        public static ISequenceGeneratorService CreateSequenceGeneratorService()
        {
            return new SequenceGeneratorService();
        }

        public static IImageMusicalizationService CreateImageMusicalizationService()
        {
            INoteGenerationService noteGenerationService = CreateNoteGenerationService();
            IStateMachineService stateMachineService = CreateStateMachineService();
            ISequenceGeneratorService sequenceGeneratorService = CreateSequenceGeneratorService();

            return new ImageMusicalizationService(noteGenerationService, stateMachineService, sequenceGeneratorService);
        }
    }
}