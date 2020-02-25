using NUnit.Framework;
using System;
using System.Linq;

namespace Service.Tests.Extraction
{
    public class ExtractionServiceTests
    {
        [Test]
        public void GivenIHaveSevenSeeds_And_ImageBase64_WhenICallKMeansExtraction_ThenShouldReturnSevenCenterInKMeansResult()
        {
            // Given
            var seed = 7;
            var imageBase64 = Constants.ImageBase64;
            var extractionService = ServiceFactory.CreateExtractionService();
            
            // When 
            var kMeansExtractionResult = extractionService.KMeansExtraction(imageBase64, seed);

            // Then
            Assert.IsNotNull(kMeansExtractionResult);
            CollectionAssert.IsNotEmpty(kMeansExtractionResult);
            Assert.AreEqual(expected: 7, actual: kMeansExtractionResult.Count());
        }
    }
}