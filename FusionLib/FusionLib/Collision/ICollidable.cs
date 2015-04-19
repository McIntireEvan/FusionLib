using Microsoft.Xna.Framework;

namespace FusionLib.Collision
{
    /// <summary>
    /// Interface for collision detection, provides methods used by the collision detector
    /// </summary>
    public interface ICollidable
    {
        void OnCollision(ICollidable other);
        Rectangle GetHitbox();
    }
}