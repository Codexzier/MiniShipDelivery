namespace CodexzierGameEngine.DataModels.World
{
    public struct TilePosition
    {
        public float X;
        public float Y;

        public TilePosition(int positionX, int positionY)
        {
            this.X = positionX; 
            this.Y = positionY;
        }
    }
}
