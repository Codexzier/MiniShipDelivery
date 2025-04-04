﻿namespace CodexzierGameEngine.DataModels.World
{
    public class WorldMapLayer
    {
        public int Id { get; set; }
        public int WorldMapChunkID { get; set; }
        public MapLayer MapLayer { get; set; }
        public MapTile[][] Map { get; set; }
        public int[] ListOfValidateTileNumbers { get; set; }
    }
}
