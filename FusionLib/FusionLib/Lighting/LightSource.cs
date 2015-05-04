using FusionLib.Shapes;
using FusionLib.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FusionLib.Lighting
{
    public class LightSource
    {
        protected Vector2 pos;
        protected int radius;

        protected List<VertexPositionColor> verticies;
        protected List<VertexPositionColor> offsetVerticies;

        protected Vector2 screenCenter;

        protected BasicEffect basicEffect;
        protected Matrix world = Matrix.CreateTranslation(0, 0, 0);
        protected Matrix view;
        protected Matrix projection;

        protected List<Line> lightRays;

        public LightSource(Vector2 pos, int radius)
        {
            this.pos = pos;
            this.radius = radius;

            verticies = new List<VertexPositionColor>();
            offsetVerticies = new List<VertexPositionColor>();
            basicEffect = new BasicEffect(GameServices.GetService<GraphicsDevice>());

            screenCenter = new Vector2(GameServices.GetService<GraphicsDeviceManager>().PreferredBackBufferWidth * .5f,
                                       GameServices.GetService<GraphicsDeviceManager>().PreferredBackBufferHeight * .5f);
            view = Matrix.CreateLookAt(new Vector3(screenCenter, 0), new Vector3(screenCenter, 1), new Vector3(0, -1, 0));

            projection = Matrix.CreateOrthographic(screenCenter.X * 2, screenCenter.Y * 2, -.5f, 1f);
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.VertexColorEnabled = true;
        }

        public LightSource(Point pos, int radius)
        {
            this.pos = new Vector2(pos.X, pos.Y);
            this.radius = radius;
        }

        public void calculateRays(List<Vector2> vertexes, List<Line> lines)
        {
            List<Vector2> endVertexes = new List<Vector2>();
            lightRays.Clear();
            List<VertexPositionColor> newPoints = new List<VertexPositionColor>();
            foreach (Vector2 vertex in vertexes)
            {
                if (Vector2.Distance(pos, vertex) < radius)
                {
                    Line l = new Line(pos, vertex);
                    Vector2 v = l.getClosestIntersection(lines, pos);
                    if (!endVertexes.Contains(v))
                        endVertexes.Add(v);

                    l.SetEnd(v);
                    lightRays.Add(l);
                }
            }
            endVertexes = ClockSort.ClockwiseSort(endVertexes, pos);

            Vector2[] vertexArray = endVertexes.ToArray();
            Color color = Color.White;

            for (int i = 0; i < vertexArray.Length - 1; i++)
            {
                newPoints.Add(new VertexPositionColor(new Vector3(pos, 0), color));
                newPoints.Add(new VertexPositionColor(new Vector3(vertexArray[i], 0), color));
                newPoints.Add(new VertexPositionColor(new Vector3(vertexArray[i + 1], 0), color));
            }

            newPoints.Add(new VertexPositionColor(new Vector3(pos, 0), color));
            newPoints.Add(new VertexPositionColor(new Vector3(vertexArray[vertexArray.Length - 1], 0), color));
            newPoints.Add(new VertexPositionColor(new Vector3(vertexArray[0], 0), color));
        }

        public void setPositon(Vector2 pos)
        {
            this.pos = pos;
        }

        public void ClearPoints()
        {
            verticies.Clear();
        }

        public void AddPoint(Vector3 position, Color color)
        {
            verticies.Add(new VertexPositionColor(position, color));
        }

        public void SetPoints(List<VertexPositionColor> points)
        {
            this.verticies = points;
        }

        public List<VertexPositionColor> GetPoints()
        {
            return this.offsetVerticies;
        }

        public void Update(Vector2 offset)
        {
            offsetVerticies.Clear();
            foreach (VertexPositionColor c in verticies)
            {
                offsetVerticies.Add(new VertexPositionColor(new Vector3((new Vector2(c.Position.X, c.Position.Y) + offset), 0), c.Color));
            }
        }

        public void Draw()
        {
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (offsetVerticies.ToArray().Length != 0)
                    GameServices.GetService<GraphicsDevice>().DrawUserPrimitives(PrimitiveType.TriangleList, offsetVerticies.ToArray(), 0, offsetVerticies.ToArray().Length / 3);
            }
        }
    }
}