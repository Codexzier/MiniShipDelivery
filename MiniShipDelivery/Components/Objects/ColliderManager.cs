using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderManager
    {
        private List<ICollider> _colliders = new List<ICollider>();

        public ColliderManager()
        {
        }

        public void Add(ICollider collider)
        {
            this._colliders.Add(collider);
        }

        internal void Update(GameTime gameTime)
        {
            // reset all collisions
            foreach (var collider in this._colliders)
            {
                collider.ClearCollisions();
            }

            foreach (var collider in this._colliders)
            {
                foreach (var otherCollider in this._colliders)
                {
                    if (collider == otherCollider)
                    {
                        continue;
                    }
                    if (collider.Collider.Intersects(otherCollider.Collider))
                    {
                        collider.OnCollision(otherCollider);
                    }
                }
            }
        }
    }
}