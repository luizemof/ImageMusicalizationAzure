using System.Collections.Generic;
using System.Drawing;

namespace Models.Extraction.KMeans
{
    public class KMeansExtractionResult
    {
        public IEnumerable<Point> Centers { get; }

        public KMeansExtractionResult(IEnumerable<Point> centers)
        {
            Centers = centers;
        }
    }
}