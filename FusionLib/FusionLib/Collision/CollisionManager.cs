using System;
using System.Collections.Generic;

namespace FusionLib.Collision
{
    /// <summary>
    /// Handles collisions by tracking ICollidables, tracking their hitboxes, and calling OnCollision() for each when they collide
    /// </summary>
    public class CollisionManager
    {
        protected Dictionary<ICollidable, List<ICollidable>> objects;

        public CollisionManager()
        {
            objects = new Dictionary<ICollidable, List<ICollidable>>();
        }

        public virtual void Update()
        {
            foreach (KeyValuePair<ICollidable, List<ICollidable>> pair in objects)
            {
                foreach (ICollidable two in pair.Value)
                {
                    Detect(pair.Key, two);
                }
            }
        }

        public void Track(ICollidable one, ICollidable two)
        {
            if (objects.ContainsKey(one))
            {
                objects[one].Add(two);
            }
            else
            {
                objects.Add(one, new List<ICollidable>() { two });
            }
        }

        public void Remove(ICollidable c)
        {
            objects.Remove(c);
        }

        public void Detect(ICollidable one, ICollidable two)
        {
            if (one.GetHitbox().Intersects(two.GetHitbox()))
            {
                float xDepth = CalcCollisionDepth(one, two, CollisionType.HORIZONTAL);
                float yDepth = CalcCollisionDepth(one, two, CollisionType.VERTICAL);

                if ((xDepth != 0 && Math.Abs(xDepth) < Math.Abs(yDepth)) || (yDepth != 0 && Math.Abs(yDepth) < Math.Abs(xDepth)))
                {
                    one.OnCollision(two);
                    two.OnCollision(one);
                }
            }
        }

        //TODO: Error checking?
        protected float CalcCollisionDepth(ICollidable one, ICollidable two, CollisionType type)
        {
            float distOne = 0, distTwo = 0, centerOne = 0, centerTwo = 0;
            switch (type)
            {
                case CollisionType.HORIZONTAL:
                    {
                        distOne = one.GetHitbox().Width / 2;
                        distTwo = two.GetHitbox().Width / 2;
                        centerOne = one.GetHitbox().Center.X;
                        centerTwo = two.GetHitbox().Center.X;
                        break;
                    }
                case CollisionType.VERTICAL:
                    {
                        distOne = one.GetHitbox().Height / 2;
                        distTwo = two.GetHitbox().Height / 2;
                        centerOne = one.GetHitbox().Center.Y;
                        centerTwo = one.GetHitbox().Center.Y;
                        break;
                    }
            }

            float currentDistance = centerOne - centerTwo;
            float minDistance = distOne + distTwo;

            if (Math.Abs(currentDistance) >= minDistance)
                return 0;

            return currentDistance > 0 ? minDistance - currentDistance : -minDistance - currentDistance;
        }

        enum CollisionType { VERTICAL, HORIZONTAL};
    }
}