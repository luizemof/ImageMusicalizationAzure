
using Service.Extraction.KMeans;
using System.Drawing;
using System;
using NUnit.Framework;
using SkiaSharp;
using System.Linq;

namespace Service.Tests.Extraction.KMeans
{
    public class KMeansExtractionTests
    {
        [Test]
        public void GivenIHaveCenters_AndImageByte_WhenIRunTheExtraction_ThenShouldReturnTheResult()
        {
            // Given
            var expectedCenters = new[]
            {
                new Point(x: 02, y: 23),
                new Point(x: 24, y: 28),
                new Point(x: 31, y: 20),
                new Point(x: 43, y: 22),
                new Point(x: 58, y: 05),
                new Point(x: 60, y: 39),
                new Point(x: 35, y: 16)
            };
            var image = GetImageBitmap(Constants.ImageBase64);
            var centers = new[]
            {
                new Point(x: 01, y: 24),
                new Point(x: 20, y: 32),
                new Point(x: 11, y: 44),
                new Point(x: 27, y: 50),
                new Point(x: 50, y: 52),
                new Point(x: 48, y: 27),
                new Point(x: 44, y: 02),
            };

            // When
            var kMeansExtractionResults = new KMeansExtraction(image).Run(centers);

            // Then
            Assert.IsNotNull(kMeansExtractionResults);
            CollectionAssert.IsNotEmpty(kMeansExtractionResults);
            CollectionAssert.AreEqual(expectedCenters, kMeansExtractionResults.Select(x => x.Coordinator));
        }

        private SKBitmap GetImageBitmap(string imageBase64)
        {
            var imageBytes = Convert.FromBase64String(Constants.ImageBase64);
            SKBitmap sKBitmap;
            using (var inputStream = new SKMemoryStream(imageBytes))
            {
                sKBitmap = SKBitmap.Decode(inputStream);
            }

            return sKBitmap;
        }
    }
}