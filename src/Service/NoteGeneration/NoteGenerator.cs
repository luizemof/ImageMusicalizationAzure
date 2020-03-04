using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Models.NoteGeneration;
using SkiaSharp;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.NoteGeneration
{
    internal class NoteGenerator
    {
        private Dictionary<ENote, SKColor> ColorNotes = new Dictionary<ENote, SKColor>()
        {
            { ENote.C, SKColor.Parse("000000") },
            { ENote.D, SKColor.Parse("ff0000") },
            { ENote.E, SKColor.Parse("00ff00") },
            { ENote.F, SKColor.Parse("0000ff") },
            { ENote.G, SKColor.Parse("ffff00") },
            { ENote.A, SKColor.Parse("ff00ff") },
            { ENote.B, SKColor.Parse("00ffff") },
            { ENote.C_8, SKColor.Parse("ffffff") }
        };

        public Task<KeyValuePair<Guid, ENote>> GenerateAsync(NoteGenerationInput input)
        {
            return Task.Run(() => Generate(input));
        }

        public KeyValuePair<Guid, ENote> Generate(NoteGenerationInput input)
        {
            var minValue = double.MaxValue;
            var closestNote = ENote.Unknow;
            foreach (var colorNote in ColorNotes)
            {
                var distance = colorNote.Value.CalculateDistance(input.Pixel);
                if (distance < minValue)
                {
                    minValue = distance;
                    closestNote = colorNote.Key;
                }
            }

            return new KeyValuePair<Guid, ENote>(input.Id, closestNote);
        }
    }
}