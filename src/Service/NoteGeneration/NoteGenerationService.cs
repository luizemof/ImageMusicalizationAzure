using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models.NoteGeneration;
using Service.Extraction;
using System.Linq;
using System.Threading.Tasks;
using System;
using Models.Extraction.KMeans;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.NoteGeneration
{
    internal class NoteGenerationService : INoteGenerationService
    {
        private readonly IExtractionService ExtractionService;

        public NoteGenerationService(IExtractionService extractionService)
        {
            ExtractionService = extractionService;
        }

        public IEnumerable<NoteGenerationResult> Generate(string imageBase64)
        {
            return GenerateAsync(imageBase64).Result;
        }

        private async Task<IEnumerable<NoteGenerationResult>> GenerateAsync(string imageBase64)
        {
            var extractionResultDic = ExtractionService.KMeansExtraction(imageBase64, seeds: 7).ToDictionary(x => Guid.NewGuid());

            List<Task<KeyValuePair<Guid, ENote>>> generateTasks = new List<Task<KeyValuePair<Guid, ENote>>>();
            var noteGenerator = new NoteGenerator();
            foreach (var extractionResult in extractionResultDic)
            {
                var noteGenerationInput = new NoteGenerationInput(extractionResult.Key, extractionResult.Value.Pixel);
                generateTasks.Add(noteGenerator.GenerateAsync(noteGenerationInput));
            }

            var taskResult = await Task.WhenAll(generateTasks);

            return taskResult.Select(x => ConvertToNoteGenerationResult(x, extractionResultDic));
        }

        private NoteGenerationResult ConvertToNoteGenerationResult(KeyValuePair<Guid, ENote> noteByGuid, Dictionary<Guid, KMeansExtractionResult> extractionResultDic)
        {
            if(extractionResultDic.TryGetValue(noteByGuid.Key, out KMeansExtractionResult result))
            {
                return new NoteGenerationResult()
                {
                    Note = noteByGuid.Value,
                    NumberOfElements = result.NumberOfElements,
                    Pixel = result.Pixel
                };
            }
            else
                throw new Exception("Id not found");
        }
    }
}