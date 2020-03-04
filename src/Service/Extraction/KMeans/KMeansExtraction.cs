using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Extraction.KMeans;
using SkiaSharp;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.Extraction.KMeans
{
    internal class KMeansExtraction : IDisposable
    {
        private class Center : IEquatable<Center>
        {
            public Point Coordinate { get; set; }
            public int NumberOfElements { get; set; }

            public bool Equals([AllowNull] Center other)
            {
                return 
                (this == null && other == null) 
                || 
                (this != null && other != null && this.Coordinate.X == other.Coordinate.X && this.Coordinate.Y == other.Coordinate.Y);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Center);
            }

            public override int GetHashCode()
            {
                return Coordinate.GetHashCode();
            }
        }

        private readonly SKBitmap ImageBitmap;
        private const int Threshold = 50;

        public KMeansExtraction(SKBitmap imageBitmap)
        {
            ImageBitmap = imageBitmap ?? throw new NullReferenceException();
        }

        public IEnumerable<KMeansExtractionResult> Run(IEnumerable<Point> input)
        {
            return GetCenters(input).ToList();
        }

        private IEnumerable<KMeansExtractionResult> GetCenters(IEnumerable<Point> initialCenter)
        {
            var centers = initialCenter.Select(c => new Center() { Coordinate = c }).ToList();
            for (int i = 0; i < Threshold; i++)
            {
                var groups = GetGroups(centers);
                var newCenters = GetCenters(groups).Result;
                if (centers.All(center => newCenters.Contains(center)))
                {
                    break;
                }

                centers = newCenters.ToList();
            }

            return centers.Select(center => ConvertToResult(center));
        }

        private KMeansExtractionResult ConvertToResult(Center center)
        {
            var coordinates = center.Coordinate;
            var pixel = ImageBitmap.GetPixel(coordinates.X, coordinates.Y);
            var numberOfElements = center.NumberOfElements;
            return new KMeansExtractionResult(coordinates, pixel, numberOfElements);
        }

        private async Task<IEnumerable<Center>> GetCenters(Dictionary<Point, List<Point>> groups)
        {
            var getCenterTasks = new List<Task<Center>>();

            foreach (var item in groups.Values)
            {
                getCenterTasks.Add(Task.Run(() => GetNewCenter(item)));
            }

            var centers = await Task.WhenAll(getCenterTasks);
            return centers;
        }

        private Center GetNewCenter(IEnumerable<Point> points)
        {
            var coordinate = Point.Empty;
            var closestAvgDistance = Double.MaxValue;
            var totalPoints = points.Count();
            foreach (var possibleCenter in points)
            {
                var distance = 0d;
                var pixelPossibleCenter = ImageBitmap.GetPixel(possibleCenter.X, possibleCenter.Y);
                foreach (var item in points)
                {
                    var pixelItem = ImageBitmap.GetPixel(item.X, item.Y);
                    distance += pixelPossibleCenter.CalculateDistance(pixelItem);
                }

                var avgDistance = distance / totalPoints;
                if (avgDistance < closestAvgDistance)
                {
                    closestAvgDistance = avgDistance;
                    coordinate = possibleCenter;
                }

            }
            return new Center() { Coordinate = coordinate, NumberOfElements = points.Count() };
        }

        private Dictionary<Point, List<Point>> GetGroups(IEnumerable<Center> centers)
        {
            var groups = centers.ToDictionary(center => center.Coordinate, (center) => new List<Point>());
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

        private Point GetClosestCenterPoint(IEnumerable<Center> centers, SKColor pixel)
        {
            Point closestPoint = Point.Empty;
            double closestDistance = double.MaxValue;
            foreach (var center in centers)
            {
                var coordinate = center.Coordinate;
                var pixelCenter = ImageBitmap.GetPixel(coordinate.X, coordinate.Y);
                var distance = pixelCenter.CalculateDistance(pixel);
                if (distance < closestDistance)
                {
                    closestPoint = coordinate;
                    closestDistance = distance;
                }
            }

            return closestPoint;
        }

        public void Dispose()
        {
            ImageBitmap.Dispose();
        }
    }
}