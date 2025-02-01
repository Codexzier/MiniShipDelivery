using Microsoft.Xna.Framework;
using System;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderBox2D(int Width, int height)
    {
        public int Width { get; } = Width;
        public int Height { get; } = height;

        public Vector2 Position { get; internal set; }

        internal bool Intersects(ColliderBox2D collider)
        {
            // return Math.Abs(this.Position.X - collider.Position.X) < this.Wide &&
            //        Math.Abs(this.Position.Y - collider.Position.Y) < this.Height;
            
            return this.Position.X < collider.Position.X + collider.Width &&
                   this.Position.X + this.Width > collider.Position.X &&
                   this.Position.Y < collider.Position.Y + collider.Height &&
                   this.Position.Y + this.Height > collider.Position.Y;
        }
    }
}