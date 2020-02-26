using Models.Extraction.KMeans;
using Service.Extraction.KMeans;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.Extraction
{
    internal class ExtractionService : IExtractionService
    {
        public IEnumerable<KMeansExtractionResult> KMeansExtraction(string imageBase64, int seeds)
        {
            IEnumerable<KMeansExtractionResult> kMeansExtractionResults;
            var image = GetImageBitmap(imageBase64);
            var centers = GetInitialCenters(seeds, image.Width, image.Height);
            using (var kMeansExtraction = new KMeansExtraction(image))
            {
                kMeansExtractionResults = kMeansExtraction.Run(centers);
            }

            return kMeansExtractionResults;
        }
        private SKBitmap GetImageBitmap(string imageBase64)
        {
            var imageBytes = ConvertImageBase64ToBytes(imageBase64);
            SKBitmap sKBitmap;
            using(var inputStream = new SKMemoryStream(imageBytes))
            {
                sKBitmap = SKBitmap.Decode(inputStream);
            }

            return sKBitmap;
        }
        
        private IEnumerable<Point> GetInitialCenters(int seeds, int width, int height)
        {
            var randomPoint = new Random();
            var points = new HashSet<Point>();
            while (points.Count < seeds)
            {
                int xPoint = randomPoint.Next(width);
                int yPoint = randomPoint.Next(height);
                var point = new Point(xPoint, yPoint);
                points.Add(point);
            }
            return points;
        }

        private byte[] ConvertImageBase64ToBytes(string imageBase64)
        {
            return Convert.FromBase64String(imageBase64);
        }
    }
}