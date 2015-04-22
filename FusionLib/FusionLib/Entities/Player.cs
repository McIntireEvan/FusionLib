using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionLib.Entities
{
    public class Player : Entity
    {
        protected Vector2 velocity;

        public Player(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            velocity = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}
