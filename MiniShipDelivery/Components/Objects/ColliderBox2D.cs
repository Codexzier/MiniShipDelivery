using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderBox2D(int width, int height)
    {
        public int Width { get; } = width;
        public int Height { get; } = height;
        public Vector2 Position { get; internal set; }

        internal bool Intersects(ColliderBox2D collider)
        {
            return this.Position.X < collider.Position.X + collider.Width &&
                   this.Position.X + this.Width > collider.Position.X &&
                   this.Position.Y < collider.Position.Y + collider.Height &&
                   this.Position.Y + this.Height > collider.Position.Y;
        }
    }
}