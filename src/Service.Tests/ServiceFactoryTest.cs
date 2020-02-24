using NUnit.Framework;
using Service.Extraction;

namespace Service.Tests
{
    public class ServiceFactoryTest
    {
        [Test]
        public void GivenIHaveAServiceFactory_WhenICallCreateExtraction_Then()
        {
            // Given

            // When
            var extractionService = ServiceFactory.CreateExtractionService();

            // Then
            Assert.IsInstanceOf(typeof(IExtractionService), extractionService);
        }
    }
}