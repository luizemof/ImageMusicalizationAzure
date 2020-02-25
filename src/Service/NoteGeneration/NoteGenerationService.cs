using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models.NoteGeneration;
using Service.Extraction;
using System.Linq;

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
            var kMeansExtractionResults = ExtractionService.KMeansExtraction(imageBase64, seeds: 7);
            var noteGenerator = new NoteGenerator();
            return noteGenerator.Generate(kMeansExtractionResults.Select(x => new NoteGenerationInput(x.Pixel)));
        }
    }
}