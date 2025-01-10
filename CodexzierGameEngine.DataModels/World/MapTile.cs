namespace CodexzierGameEngine.DataModels.World
{
    public class MapTile
    {
        public int Id { get; set; }
        
        public int WorldMapLevelID { get; set; }
        public int MapTileX { get;set; }
        public int MapTileY { get; set; }
        
        [Obsolete("Ist Redundant, vielleicht entfernen")]
        public int TilemapPart { get; set; }
        public TilePosition Position { get; set; }
        
    }
}
