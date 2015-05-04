using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FusionLib.Utils
{
    public static class ClockSort
    {
        // Sorts points in a roughly clockwise order
        public static List<Vector2> ClockwiseSort(List<Vector2> points, Vector2 center)
        {
            Dictionary<Vector2, double> values = new Dictionary<Vector2, double>();
            List<Vector2> sortedPoints = new List<Vector2>();

            foreach (Vector2 p in points)
            {
                Vector2 v = p - center;
                double a = Math.Atan2(v.Y, v.X);

                if (!((int)p.X == (int)center.X))
                    values.Add(p, a);
            }

            var sortedValues = values.Values.OrderBy(x => x);

            foreach (double d in sortedValues.ToList())
            {
                sortedPoints.Add((from a in values where a.Value == d select a).First().Key);
            }

            return sortedPoints;
        }
    }
}