namespace CodexzierGameEngine.DataModels.World
{
    public class WorldMapLevel
    {
        public int Id { get; set; }
        public int WorldMapChunkID { get; set; }
        public LayerPart LayerPart { get; set; }
        public MapTile[][] Map { get; set; }
        public int[] ListOfValidateTileNumbers { get; set; }
    }
}
