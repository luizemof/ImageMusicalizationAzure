using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Models.Extraction.KMeans;

namespace Extraction.KMeans
{
    public class KMeansExtraction
    {
        private readonly Bitmap ImageBitmap;
        private readonly int Seed;

        private const int Threshold = 50;

        public KMeansExtraction(string imageFile, int seed)
        {
            ImageBitmap = !string.IsNullOrWhiteSpace(imageFile) ? new Bitmap(Image.FromFile(imageFile)) : throw new NullReferenceException(nameof(imageFile));
            Seed = seed;
        }

        public KMeansExtractionResult Run()
        {
            var centers = GetCenters();
            return new KMeansExtractionResult(centers);
        }

        private IEnumerable<Point> GetCenters()
        {
            var centers = GetInitialCenters();
            for (int i = 0; i < Threshold; i++)
            {
                var groups = GetGroups(centers);
                var newCenters = GetCenters(groups).Result;
                if(centers.All(center => newCenters.Contains(center)))
                {
                    break;
                }

                centers = newCenters;
            }

            return centers;
        }

        private async Task<IEnumerable<Point>> GetCenters(Dictionary<Point, List<Point>> groups)
        {
            var getCenterTasks = new List<Task<Point>>();

            foreach (var item in groups.Values)
            {
                getCenterTasks.Add(Task.Run(() => GetNewCenter(item)));
            }

            var centers = await Task.WhenAll(getCenterTasks);
            return centers;
        }

        private Point GetNewCenter(IEnumerable<Point> points)
        {
            var center = Point.Empty;
            var closestAvgDistance = Double.MaxValue;
            var totalPoints = points.Count();
            foreach (var possibleCenter in points)
            {
                double distance = 0;
                Color pixelPossibleCenter = ImageBitmap.GetPixel(possibleCenter.X, possibleCenter.Y);
                foreach (var item in points)
                {
                    Color pixelItem = ImageBitmap.GetPixel(item.X, item.Y);
                    distance += CalculateDistance(pixelPossibleCenter, pixelItem);
                }

                var avgDistance = distance / totalPoints;
                if(avgDistance < closestAvgDistance)
                {
                    closestAvgDistance = avgDistance;
                    center = possibleCenter;
                }

            }
            return center;
        }

        private Dictionary<Point, List<Point>> GetGroups(IEnumerable<Point> centers)
        {
            var groups = centers.ToDictionary(center => center, (center) => new List<Point>());
            for (int x = 0; x < ImageBitmap.Width; x++)
            {
                for (int y = 0; y < ImageBitmap.Height; y++)
                {
                    var point = new Point(x, y);
                    var pixel = ImageBitmap.GetPixel(point.X, point.Y);
                    var clostestCenter = GetClosestCenterPoint(centers, pixel);
                    groups[clostestCenter].Add(point);
                }
            }

            return groups;
        }

        private Point GetClosestCenterPoint(IEnumerable<Point> centers, Color pixel)
        {
            Point closestPoint = Point.Empty;
            double closestDistance = double.MaxValue;
            foreach (var center in centers)
            {
                var pixelCenter = ImageBitmap.GetPixel(center.X, center.Y);
                var distance = CalculateDistance(pixelCenter, pixel);
                if (distance < closestDistance)
                {
                    closestPoint = center;
                    closestDistance = distance;
                }
            }

            return closestPoint;
        }

        private double CalculateDistance(Color pixelA, Color pixelB)
        {
            double distanceR = Convert.ToDouble(pixelA.R) - Convert.ToDouble(pixelB.R);
            double distanceG = Convert.ToDouble(pixelA.G) - Convert.ToDouble(pixelB.G);
            double distanceB = Convert.ToDouble(pixelA.B) - Convert.ToDouble(pixelB.B);

            return Math.Sqrt(Math.Pow(distanceR, 2) + Math.Pow(distanceG, 2) + Math.Pow(distanceB, 2));
        }

        private IEnumerable<Point> GetInitialCenters()
        {
            var randomPoint = new Random();
            var points = new HashSet<Point>();
            while (points.Count < Seed)
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