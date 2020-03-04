using System.Linq;
using Models.NoteGeneration;
using Service.NoteGeneration;
using NUnit.Framework;
using SkiaSharp;
using System;

namespace Service.Tests.NoteGeneration
{
    public class NoteGeneratorTests
    {
        private const string BlackHex = "FF000000";
        private const string IndianRedHex = "FFCD5C5C";
        private const string ForestGreenHex = "FF228B22";
        private const string DarkBlue = "FF00008B";
        private const string YellowGreen = "FF9ACD32";
        private const string MediumPurple = "FF9370DB";
        private const string DarkCyan = "FF008B8B";
        private const string LightGray = "FFD3D3D3";

        [TestCase(BlackHex, ExpectedResult = ENote.C)]
        [TestCase(IndianRedHex, ExpectedResult = ENote.D)]
        [TestCase(ForestGreenHex, ExpectedResult = ENote.E)]
        [TestCase(DarkBlue, ExpectedResult = ENote.F)]
        [TestCase(YellowGreen, ExpectedResult = ENote.G)]
        [TestCase(MediumPurple, ExpectedResult = ENote.A)]
        [TestCase(DarkCyan, ExpectedResult = ENote.B)]
        [TestCase(LightGray, ExpectedResult = ENote.C_8)]
        public ENote GivenIHaveCoordinatorAndPixel_WhenIGenerateNotes_TheShouldReturnNotes(string colorHex)
        {
            // Given
            var inputs = new NoteGenerationInput(id: Guid.NewGuid(), pixel: SKColor.Parse(colorHex));

            // When
            var noteGenerationResult = new NoteGenerator().Generate(inputs);

            // Then
            Assert.IsNotNull(noteGenerationResult);
            return noteGenerationResult.Value;
        }
    }
}