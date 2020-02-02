using NUnit.Framework;
using Extraction.KMeans;
using System;

namespace Extraction.Tests.KMeans
{
    public class KMeansExtractionTest
    {
        [Test]
        public void GivenIPassNullOrEmptyImageFile_WhenICalledKMeansExtraction_ThenShouldThrowsNullReferenceException()
        {
            // Given
            var imageFile = string.Empty;

            // When
            var kMeansExtraction = new KMeansExtraction(imageFile);

            // Then
            Assert.Throws(typeof(NullReferenceException), () => kMeansExtraction.Run());
        }

        [Test]
        public void GivenIHaveValidImageFile_And_OneSeed_WhenIRunKMeansExtraction_ThenSouldReturnKMeansExtractionResult()
        {
            // Given

            // When
            var kMeansExtraction = new KMeansExtraction(Constants.ImageFile);
            var kMeansExtractionResult = kMeansExtraction.Run();

            // Then
            Assert.IsNotNull(kMeansExtractionResult);
        }
    }
}