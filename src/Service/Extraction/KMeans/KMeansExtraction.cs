using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Extraction.KMeans;
using SkiaSharp;
using System.Drawing;

namespace Service.Extraction.KMeans
{
    internal class KMeansExtraction : IDisposable
    {
        private readonly SKBitmap ImageBitmap;
        private readonly int Seed;
        private const int Threshold = 50;

        public KMeansExtraction(byte[] imageByte, int seed)
        {
            ImageBitmap = imageByte != null && imageByte.Any() ? GetImageBitmap(imageByte) : throw new NullReferenceException(nameof(imageByte));
            Seed = seed;
        }

        private SKBitmap GetImageBitmap(byte[] imageByte)
        {
            SKBitmap sKBitmap;
            using(var inputStream = new SKMemoryStream(imageByte))
            {
                sKBitmap = SKBitmap.Decode(inputStream);
            }

            return sKBitmap;
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
                var distance = 0d;
                var pixelPossibleCenter = ImageBitmap.GetPixel(possibleCenter.X, possibleCenter.Y);
                foreach (var item in points)
                {
                    var pixelItem = ImageBitmap.GetPixel(item.X, item.Y);
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

        private Point GetClosestCenterPoint(IEnumerable<Point> centers, SKColor pixel)
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

        private double CalculateDistance(SKColor pixelA, SKColor pixelB)
        {
            double distanceR = Convert.ToDouble(pixelA.Red) - Convert.ToDouble(pixelB.Red);
            double distanceG = Convert.ToDouble(pixelA.Green) - Convert.ToDouble(pixelB.Green);
            double distanceB = Convert.ToDouble(pixelA.Blue) - Convert.ToDouble(pixelB.Blue);

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

        public void Dispose()
        {
            ImageBitmap.Dispose();
        }
    }
}