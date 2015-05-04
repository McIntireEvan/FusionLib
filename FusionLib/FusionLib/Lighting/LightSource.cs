using Microsoft.Xna.Framework;

namespace FusionLib.Lighting
{
    public class LightSource
    {
        protected Vector2 pos;
        protected int radius;

        public LightSource(Vector2 pos, int radius)
        {
            this.pos = pos;
            this.radius = radius;
        }

        public LightSource(Point pos, int radius)
        {
            this.pos = new Vector2(pos.X, pos.Y);
            this.radius = radius;
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}