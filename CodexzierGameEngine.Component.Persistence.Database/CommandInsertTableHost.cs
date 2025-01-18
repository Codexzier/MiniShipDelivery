using System.Text;
using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database;

public static class CommandInsertTableHost
{
    public static string GetCommandByDataModel(object dataModel)
    {
        switch (dataModel.GetType().Name)
        {
            case nameof(WorldMapChunk):
                return InsertTableWorldMapChunk((WorldMapChunk)dataModel);
            case nameof(WorldMapChunkPosition):
                return InsertTableWorldMapChunkPosition((WorldMapChunkPosition)dataModel);
            case nameof(WorldMapLevel):
                return InsertTableWorldMapLevel((WorldMapLevel)dataModel);
            case nameof(MapTile):
                return InsertTableMapTile((MapTile)dataModel);
        }
        
        throw new NotImplementedException();
    }

    private static string InsertTableWorldMapChunk(WorldMapChunk worldMapChunk)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO WorldMapChunk (WorldMapChunkPositionId) VALUES (");
        sb.AppendLine($"{worldMapChunk.WorldMapChunkPositionID}");
        sb.AppendLine(")");

        return sb.ToString();
    }
    
    private static string InsertTableWorldMapChunkPosition(WorldMapChunkPosition dataModel)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO WorldMapChunkPosition (X, Y) VALUES (");
        sb.AppendLine($"{dataModel.X}, {dataModel.Y}");
        sb.AppendLine(")");

        return sb.ToString();
    }
    
    private static string InsertTableWorldMapLevel(WorldMapLevel dataModel)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO WorldMapLevel (WorldMapChunkId, LevelPart) VALUES (");
        sb.AppendLine($"{dataModel.WorldMapChunkID}, {(int)dataModel.LayerPart}");
        sb.AppendLine(")");

        return sb.ToString();
    }
    
    private static string InsertTableMapTile(MapTile dataModel)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO MapTile (WorldMapLevelId, MapTileX, MapTileY, TilemapPart) VALUES (");
        sb.AppendLine($"{dataModel.WorldMapLevelID}, {dataModel.MapTileX}, {dataModel.MapTileY}" +
                      $", {dataModel.NumberPart}");
        sb.AppendLine(")");

        return sb.ToString();
    }
}