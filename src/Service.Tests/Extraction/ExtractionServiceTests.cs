using NUnit.Framework;
using System;
using System.Linq;

namespace Service.Tests
{
    public class ExtractionServiceTests
    {
        [Test]
        public void GivenIHaveSevenSeeds_And_ImageBase64_WhenICallKMeansExtraction_ThenShouldReturnSevenCenterInKMeansResult()
        {
            // Given
            var seed = 7;
            var imageBase64 = Constants.ImageBase64;
            var kMeansService = ServiceFactory.CreateKMeansService();
            
            // When 
            var kMeansExtractionResult = kMeansService.KMeansExtraction(imageBase64, seed);

            // Then
            Assert.IsNotNull(kMeansExtractionResult);
            CollectionAssert.IsNotEmpty(kMeansExtractionResult.Centers);
            Assert.AreEqual(expected: 7, actual: kMeansExtractionResult.Centers.Count());
        }

        [Test]
        public void GivenIHaveOneSeed_And_ImageBase64_WhenICallKMeansExtraction_ThenShouldReturnOneCenterInKMeansResult()
        {
            // Given
            var seed = 8;
            var imageBase64 = Constants.ImageBase64;
            var kMeansService = ServiceFactory.CreateKMeansService();
            
            // When 
            var kMeansExtractionResult = kMeansService.KMeansExtraction(imageBase64, seed);

            // Then
            Assert.IsNotNull(kMeansExtractionResult);
            CollectionAssert.IsNotEmpty(kMeansExtractionResult.Centers);
            Assert.AreEqual(expected: 8, actual: kMeansExtractionResult.Centers.Count());
        }
    }
}