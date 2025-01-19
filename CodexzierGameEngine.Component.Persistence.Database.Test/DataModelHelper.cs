using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database.Test;

public static class DataModelHelper
{
    public static WorldMapChunk CreateWorldMapChunk(int id = 1)
    {
        var worldMapChunk = new WorldMapChunk
        {
            Id = id,
            WorldMapLayers = []
        };
        return worldMapChunk;
    }

    public static WorldMapChunkPosition CreateWorldMapChunkPosition(int id, int x, int y)
    {
        var worldMapChunkPosition = new WorldMapChunkPosition
        {
            Id = id,
            X = x,
            Y = y
        };
        return worldMapChunkPosition;
    }

    public static WorldMapLayer CreateWorldMapLevel()
    {
        var worldMapLevel = new WorldMapLayer
        {
            Id = 1,
            WorldMapChunkID = 1,
            MapLayer = MapLayer.Grass
        };
        return worldMapLevel;
    }

    public static MapTile CreateMapTile()
    {
        var mapTile = new MapTile
        {
            Id = 1,
            AssetNumber = 1,
            Position = new TilePosition(1, 1)
        };
        return mapTile;
    }
}