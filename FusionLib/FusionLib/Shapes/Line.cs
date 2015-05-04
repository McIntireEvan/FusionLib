using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

//TODO: Add a way to render lines
namespace FusionLib.Shapes
{
    public class Line
    {
        private Vector2 start;
        private Vector2 end;

        public Line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public Line(Vector2 start, List<Line> lines)
        {
            this.start = start;
            this.end = getClosestIntersection(lines, start);
        }

        public float GetDeltaX()
        {
            return this.end.X - this.start.X;
        }

        public float GetDeltaY()
        {
            return this.end.Y - this.start.Y;
        }

        public float GetSlope()
        {
            return this.GetDeltaY() / this.GetDeltaX();
        }

        public Vector2 GetStart()
        {
            return this.start;
        }

        public Vector2 GetEnd()
        {
            return this.end;
        }

        public double GetLength()
        {
            return Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }

        public double GetDirection()
        {
            return Math.Atan2(-(end.X - start.X), end.Y - start.Y);
        }

        public void SetStart(Vector2 start)
        {
            this.start = start;
        }

        public void SetEnd(Vector2 end)
        {
            this.end = end;
        }

        public Vector2? getIntersection(Line line)
        {
            float rx = this.GetDeltaX();
            float ry = this.GetDeltaY();

            float lx = line.GetDeltaX();
            float ly = line.GetDeltaY();

            if ((ry / rx) == (ly / lx))
            {
                return null;
            }

            float t2 = (rx * (line.GetStart().Y - this.GetStart().Y) + ry * (this.GetStart().X - line.GetStart().X)) / (lx * ry - ly * rx);
            float t1 = (line.GetStart().X + lx * t2 - this.GetStart().X) / rx;

            if (!(t1 > 0)) return null;
            if (!(t2 > 0 && t2 < 1)) return null;

            return new Vector2(this.GetStart().X + rx * t1, this.GetStart().Y + ry * t1);
        }

        public Vector2 getClosestIntersection(List<Line> lines, Vector2 point)
        {
            Vector2 closest = new Vector2(float.MaxValue, float.MaxValue);
            foreach (Line l in lines)
            {
                Vector2? v = this.getIntersection(l);
                if (v.HasValue)
                {
                    if (new Line(point, v.Value).GetLength() < new Line(point, closest).GetLength())
                    {
                        closest = v.Value;
                    }
                }
            }
            if (closest.X.Equals(float.MaxValue))
            {
                closest = point;
            }

            return closest;
        }
    }
}