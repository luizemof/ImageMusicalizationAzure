using NUnit.Framework;
using Service.NoteGeneration;
using Moq;
using Service.Extraction;
using Models.Extraction.KMeans;
using System.Drawing;
using SkiaSharp;

namespace Service.Tests.NoteGeneration
{
    public class NoteGenerationServiceTests
    {
        [Test]
        public void GivenIHaveNoteGenerationService_WhenICallNoteGeneration_ThenShoudCallKMeansExtraction()
        {
            // Given
            var imageBase64 = Constants.ImageBase64;
            var extractionService = new Mock<IExtractionService>();
            extractionService
                .Setup(service => service.KMeansExtraction(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new[] { new KMeansExtractionResult(coordinator: new Point(), SKColors.Black) });

            // When
            var noteGenerationService = new NoteGenerationService(extractionService.Object);
            var result = noteGenerationService.Generate(imageBase64);

            // Then
            Assert.IsNotNull(result);
            extractionService.Verify(x => x.KMeansExtraction(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}