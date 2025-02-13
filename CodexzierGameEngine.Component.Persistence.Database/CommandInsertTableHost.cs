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
            case nameof(MapCoordinate):
                return InsertTableWorldMapChunkPosition((MapCoordinate)dataModel);
            case nameof(WorldMapLayer):
                return InsertTableWorldMapLevel((WorldMapLayer)dataModel);
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
    
    private static string InsertTableWorldMapChunkPosition(MapCoordinate dataModel)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO WorldMapChunkPosition (X, Y) VALUES (");
        sb.AppendLine($"{dataModel.X}, {dataModel.Y}");
        sb.AppendLine(")");

        return sb.ToString();
    }
    
    private static string InsertTableWorldMapLevel(WorldMapLayer dataModel)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO WorldMapLevel (WorldMapChunkId, LevelPart) VALUES (");
        sb.AppendLine($"{dataModel.WorldMapChunkID}, {(int)dataModel.MapLayer}");
        sb.AppendLine(")");

        return sb.ToString();
    }
    
    private static string InsertTableMapTile(MapTile dataModel)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO MapTile (WorldMapLevelId, MapTileX, MapTileY, TilemapPart) VALUES (");
        sb.AppendLine($"{dataModel.WorldMapLayerID}, {dataModel.MapTileX}, {dataModel.MapTileY}" +
                      $", {dataModel.AssetNumber}");
        sb.AppendLine(")");

        return sb.ToString();
    }
}