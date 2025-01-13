namespace CodexzierGameEngine.DataModels.World
{
    public class MapTile
    {
        public int Id { get; set; }
        
        public int WorldMapLevelID { get; set; }
        public int MapTileX { get;set; }
        public int MapTileY { get; set; }
        
        /// <summary>
        /// Vielleicht durch id ersetzten?
        /// </summary>
        [Obsolete("Ist Redundant, vielleicht entfernen")]
        public int TilemapPart { get; set; }
        public TilePosition Position { get; set; }
        
    }
}
