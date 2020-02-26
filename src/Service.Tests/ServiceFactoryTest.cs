using NUnit.Framework;
using Service.Extraction;
using Service.NoteGeneration;
using Service.StateMachine;

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

        [Test]
        public void GivenIHaveAServiceFactory_WhenICallCreateStateMachineService_ThenShouldReturnAnInstanceOfStateMachinceService()
        {
            // Given

            // When
            var stateMachineService = ServiceFactory.CreateStateMachineService();

            // Then
            Assert.IsInstanceOf(typeof(IStateMachineService), stateMachineService);
        }
    }
}