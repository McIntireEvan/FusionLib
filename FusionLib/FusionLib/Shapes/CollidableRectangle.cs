using FusionLib.Collision;
using Microsoft.Xna.Framework;

namespace FusionLib.Shapes
{
    public class CollidableRectangle : ICollidable
    {
        private Rectangle rectangle;

        public CollidableRectangle(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        public Rectangle GetHitbox()
        {
            return rectangle;
        }

        public void OnCollision(ICollidable o)
        {
        }
    }
}