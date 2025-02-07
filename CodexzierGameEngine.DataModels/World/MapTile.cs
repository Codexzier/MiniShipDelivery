namespace CodexzierGameEngine.DataModels.World
{
    public class MapTile
    {
        public int Id { get; set; }
        public int WorldMapLayerID { get; set; }
        public int MapTileX { get;set; }
        public int MapTileY { get; set; }
        public int AssetNumber { get; set; }
        public TilePosition Position { get; set; }
        public bool DrawTop { get; set; }
        public int Width { get; set; } 
        public int Height { get; set; } 
    }
}
