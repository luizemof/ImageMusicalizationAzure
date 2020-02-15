using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Models.Extraction.KMeans;

namespace Extraction.KMeans
{
    public class KMeansExtraction
    {
        private readonly Bitmap ImageBitmap;
        private readonly int Seed;

        public KMeansExtraction(string imageFile, int seed)
        {
            ImageBitmap = !string.IsNullOrWhiteSpace(imageFile) ? new Bitmap(Image.FromFile(imageFile)) : throw new NullReferenceException(nameof(imageFile));
            Seed = seed;
        }

        public KMeansExtractionResult Run()
        {
            var centers = FindCenters();

            return new KMeansExtractionResult(centers);
        }

        private IEnumerable<Point> FindCenters()
        {
            var centers = FindInitialCenters();

            return centers;
        }

        private IEnumerable<Point> FindInitialCenters()
        { 
            var randomPoint = new Random();
            var points = new HashSet<Point>();
            while(points.Count < Seed)
            {
                int xPoint = randomPoint.Next(ImageBitmap.Width);
                int yPoint = randomPoint.Next(ImageBitmap.Height);
                var point = new Point(xPoint, yPoint);
                points.Add(point);
            }
            return points;
        }
    }
}