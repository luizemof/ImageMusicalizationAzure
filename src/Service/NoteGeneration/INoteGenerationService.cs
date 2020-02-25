using System.Collections.Generic;
using Models.NoteGeneration;

namespace Service.NoteGeneration
{
    public interface INoteGenerationService
    {
        IEnumerable<NoteGenerationResult> Generate(string imageBase64);
    }
}