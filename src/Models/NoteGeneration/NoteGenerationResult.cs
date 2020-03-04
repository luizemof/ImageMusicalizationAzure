using System;
using System.Diagnostics.CodeAnalysis;
using SkiaSharp;

namespace Models.NoteGeneration
{
    public class NoteGenerationResult : IEquatable<NoteGenerationResult>
    {
        public ENote Note { get; set; }
        public int NumberOfElements { get; set; }
        public SKColor Pixel { get; set; }

        public bool Equals([AllowNull] NoteGenerationResult other)
        {
            return
            (this == null && other == null)
            ||
            (this != null && other != null && this.Note == other.Note);
        }

        public override string ToString()
        {
            return Note.ToString();
        }
    }
}