using System.Collections.Generic;
using System.Linq;
using Models.ImageMusicalization;
using Service.Extraction;
using Service.NoteGeneration;
using Service.SequenceGenerator;
using Service.StateMachine;

namespace Service.ImageMusicalization
{
    internal class ImageMusicalizationService : IImageMusicalizationService
    {
        private readonly INoteGenerationService NoteGenerationService;
        private readonly IStateMachineService StateMachineService;
        private readonly ISequenceGeneratorService SequenceGeneratorService;

        public ImageMusicalizationService(INoteGenerationService noteGenerationService, IStateMachineService stateMachineService, ISequenceGeneratorService sequenceGeneratorService)
        {
            NoteGenerationService = noteGenerationService;
            StateMachineService = stateMachineService;
            SequenceGeneratorService = sequenceGeneratorService;
        }

        public IEnumerable<ImageMusicalizationModel> CreateSongFromImage(string imageBase64)
        {
            var generationResults = NoteGenerationService.Generate(imageBase64);

            var stateMachineServiceArgs = generationResults.Select(generationResult => new StateMachinceArgs(generationResult.Note, generationResult.Pixel, generationResult.NumberOfElements));
            var stateMachineResults = StateMachineService.CreateStateMachine(stateMachineServiceArgs);

            var sequence = SequenceGeneratorService.GenerateSequence(stateMachineResults.FirstOrDefault());

            return sequence.Select(s => new ImageMusicalizationModel() { Note = s.Note });
        }
    }
}