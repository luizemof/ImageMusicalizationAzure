using System;

namespace SkiaSharp
{
    public static class SKColorExtensions
    {
        public static double CalculateDistance(this SKColor pixelA, SKColor pixelB)
        {
            double distanceR = Convert.ToDouble(pixelA.Red) - Convert.ToDouble(pixelB.Red);
            double distanceG = Convert.ToDouble(pixelA.Green) - Convert.ToDouble(pixelB.Green);
            double distanceB = Convert.ToDouble(pixelA.Blue) - Convert.ToDouble(pixelB.Blue);

            return Math.Sqrt(Math.Pow(distanceR, 2) + Math.Pow(distanceG, 2) + Math.Pow(distanceB, 2));
        }
    }
}