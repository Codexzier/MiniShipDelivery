namespace CodexzierGameEngine.DataModels.World
{
    public class WorldMapChunk
    {
        public WorldMapLayer[] WorldMapLayers { get; set; }
        public int Id { get; set; }
        
        public int WorldMapChunkPositionID { get; set; }
        public WorldMapChunkPosition Position { get; set; }
    }
}
