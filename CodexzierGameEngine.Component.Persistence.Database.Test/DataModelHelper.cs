using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database.Test;

public static class DataModelHelper
{
    public static WorldMapChunk CreateWorldMapChunk(int id = 1)
    {
        var worldMapChunk = new WorldMapChunk
        {
            Id = id,
            WorldMapLevels = []
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

    public static WorldMapLevel CreateWorldMapLevel()
    {
        var worldMapLevel = new WorldMapLevel
        {
            Id = 1,
            WorldMapChunkID = 1,
            LevelPart = LevelPart.Grass
        };
        return worldMapLevel;
    }

    public static MapTile CreateMapTile()
    {
        var mapTile = new MapTile
        {
            Id = 1,
            NumberPart = 1,
            Position = new TilePosition(1, 1)
        };
        return mapTile;
    }
}