using System;
using System.Drawing;
using SkiaSharp;

namespace Models.NoteGeneration
{
    public class NoteGenerationInput
    {
        public SKColor Pixel { get; private set; }
        public Guid Id { get; private set; }

        public NoteGenerationInput( Guid id, SKColor pixel)
        {
            Id = id;
            Pixel = pixel;
        }
    }
}