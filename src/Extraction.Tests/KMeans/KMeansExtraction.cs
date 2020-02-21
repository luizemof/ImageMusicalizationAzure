using NUnit.Framework;
using Extraction.KMeans;
using System;
using System.Linq;

namespace Extraction.Tests.KMeans
{
    public class KMeansExtractionTest
    {
        [Test]
        public void GivenIPassNullOrEmptyImageFile_WhenICreateKMeansExtractionInstance_ThenShouldThrowsNullReferenceException()
        {
            // Given
            var seed = 1;

            // When

            // Then
            Assert.Throws(typeof(NullReferenceException), () => new KMeansExtraction(imageByte: null, seed));
        }

        [Test]
        public void GivenIHaveAValidImage_AndSevenSeed_WhenIRunKMeansExtraction_ThenSouldRetrunSevenCenters()
        {
            // Given
            var seed = 8;
            var imageByte = Convert.FromBase64String(Constants.ImageBase64);

            // When
            var kMeansExtraction = new KMeansExtraction(imageByte, seed);
            var kMeansExtractionResult = kMeansExtraction.Run();

            // Then
            Assert.IsNotNull(kMeansExtractionResult);
            Assert.IsNotNull(kMeansExtractionResult.Centers);
            Assert.AreEqual(expected: 8, actual: kMeansExtractionResult.Centers.Count());
        }
    }
}