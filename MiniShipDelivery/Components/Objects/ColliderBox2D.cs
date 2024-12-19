using Microsoft.Xna.Framework;
using System;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderBox2D
    {
        public ColliderBox2D(int wide, int height)
        {
            this.Wide = wide;
            this.Height = height;
        }

        public int Wide { get; }
        public int Height { get; }

        public Vector2 Position { get; internal set; }

        internal bool Intersects(ColliderBox2D collider)
        {
            return Math.Abs(this.Position.X - collider.Position.X) < this.Wide &&
                   Math.Abs(this.Position.Y - collider.Position.Y) < this.Height;
        }
    }
}