using FusionLib.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FusionLib.Entities
{
    /// <summary>
    /// Serves as a base for other entities
    /// </summary>
    public class Entity : ICollidable
    {
        protected Texture2D texture;
        protected Vector2 pos;
        protected Rectangle hitbox;

        public Entity(Texture2D texture, Vector2 pos)
            : this(texture, pos, new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height)) { }

        public Entity(Texture2D texture, Vector2 pos, Rectangle hitbox)
        {
            this.texture = texture;
            this.pos = pos;
            this.hitbox = hitbox;
        }

        public virtual void OnCollision(ICollidable other)
        {
        }

        public virtual Rectangle GetHitbox()
        {
            return this.hitbox;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }
    }
}