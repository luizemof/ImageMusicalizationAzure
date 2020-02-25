using System.Linq;
using Models.NoteGeneration;
using Service.NoteGeneration;
using NUnit.Framework;
using System.Drawing;
using SkiaSharp;

namespace Service.Tests.NoteGeneration
{
    public class NoteGeneratorTests
    {
        [Test]
        public void GivenIHaveCoordinatorAndPixel_WhenIGenerateNotes_TheShouldReturnNotes()
        {
            // Given
            var expectedResult = new []
            {
                new NoteGenerationResult() { Note = ENote.C },
                new NoteGenerationResult() { Note = ENote.D },
                new NoteGenerationResult() { Note = ENote.E },
                new NoteGenerationResult() { Note = ENote.F },
                new NoteGenerationResult() { Note = ENote.G },
                new NoteGenerationResult() { Note = ENote.A },
                new NoteGenerationResult() { Note = ENote.B },
                new NoteGenerationResult() { Note = ENote.C_8 }
            };

            var inputs = new []
            {
                new NoteGenerationInput(pixel: SKColors.Black ),
                new NoteGenerationInput(pixel: SKColors.IndianRed ),
                new NoteGenerationInput(pixel: SKColors.ForestGreen ),
                new NoteGenerationInput(pixel: SKColors.DarkBlue ),
                new NoteGenerationInput(pixel: SKColors.YellowGreen ),
                new NoteGenerationInput(pixel: SKColors.MediumPurple ),
                new NoteGenerationInput(pixel: SKColors.DarkCyan ),
                new NoteGenerationInput(pixel: SKColors.LightGray ),
            };

            // When
            var noteGenerationResult = new NoteGenerator().Generate(inputs);

            // Then
            Assert.IsNotNull(noteGenerationResult);
            CollectionAssert.AreEqual(expectedResult.ToList(), noteGenerationResult.ToList());
        }
    }
}