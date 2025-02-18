using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.Objects
{
    public class ColliderBox2D(
        int width, 
        int height,
        Vector2 position,
        int chunkX,
        int chunkY)
    {
        public int Width { get; } = width;
        public int Height { get; } = height;
        public Vector2 Position { get; private set; } = position;
        
        public int ChunkX { get; } = chunkX;
        public int ChunkY { get; } = chunkY;
        

        internal bool Intersects(ColliderBox2D collider)
        {
            return this.Position.X < collider.Position.X + collider.Width &&
                   this.Position.X + this.Width > collider.Position.X &&
                   this.Position.Y < collider.Position.Y + collider.Height &&
                   this.Position.Y + this.Height > collider.Position.Y;
        }

        public void SetPosition(Vector2 position) => this.Position = position;

        public void AddPosition(Vector2 position) => this.Position += position;
    }
}