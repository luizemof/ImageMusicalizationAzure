using NUnit.Framework;
using Extraction.KMeans;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            var imageFile = string.Empty;

            // When

            // Then
            Assert.Throws(typeof(NullReferenceException), () => new KMeansExtraction(imageFile, seed));
        }

        [Test]
        public void GivenIHaveAValidImage_AndSevenSeed_WhenIRunKMeansExtraction_ThenSouldRetrunSevenCenters()
        {
            // Given
            var seed = 7;
            var imageFile = Constants.ImageFile;

            // When
            var kMeansExtraction = new KMeansExtraction(imageFile, seed);
            var kMeansExtractionResult = kMeansExtraction.Run();

            // Then
            Assert.IsNotNull(kMeansExtractionResult);
            Assert.IsNotNull(kMeansExtractionResult.Centers);
            Assert.AreEqual(expected: 7, actual: kMeansExtractionResult.Centers.Count());
        }
    }
}