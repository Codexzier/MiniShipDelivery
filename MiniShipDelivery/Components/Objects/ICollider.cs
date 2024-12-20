using System.Collections.Generic;

namespace MiniShipDelivery.Components.Objects
{
    public interface ICollider
    {
        ColliderBox2D Collider { get; }
        List<ICollider> Collisions { get; }
        void ClearCollisions();
        void OnCollision(ICollider otherCollider);
    }
}