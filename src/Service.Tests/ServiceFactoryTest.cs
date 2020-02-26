using NUnit.Framework;
using Service.Extraction;
using Service.NoteGeneration;

namespace Service.Tests
{
    public class ServiceFactoryTest
    {
        [Test]
        public void GivenIHaveAServiceFactory_WhenICallCreateExtraction_ThenShouldReturnAnInstanceOfExtractionService()
        {
            // Given

            // When
            var extractionService = ServiceFactory.CreateExtractionService();

            // Then
            Assert.IsInstanceOf(typeof(IExtractionService), extractionService);
        }

        [Test]
        public void GivenIHaveAServiceFactory_WhenICallCreateNoteGeneration_ThenShouldReturnAnInstanceOfNoteGenerationService()
        {
            // Given

            // When
            var noteGenerationService = ServiceFactory.CreateNoteGenerationService();

            // Then
            Assert.IsInstanceOf(typeof(INoteGenerationService), noteGenerationService);
        }
    }
}