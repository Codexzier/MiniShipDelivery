using System.Text;
using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database;

public static class CommandCreateTableHost
{
    public static IDictionary<Type, Func<string>> CommandDictionary = new Dictionary<Type, Func<string>>
    {
        {typeof(WorldMapChunk), CreateTableWorldMapChunk},
        {typeof(MapCoordinate), CreateTableWorldMapChunkPosition},
        {typeof(WorldMapLayer), CreateTableWorldMapLevel},
        {typeof(MapTile), CreateTableMapTile}
    };

    private static string CreateTableWorldMapChunk()
    {
        var sb = new StringBuilder();
        sb.AppendLine("CREATE TABLE IF NOT EXISTS WorldMapChunk (");
        sb.AppendLine("Id INTEGER PRIMARY KEY,");
        sb.AppendLine("WorldMapChunkPositionId INTEGER");
        sb.AppendLine(")");
            
        return sb.ToString();
    }

    private static string CreateTableWorldMapChunkPosition()
    {
        var sb = new StringBuilder();
        sb.AppendLine("CREATE TABLE IF NOT EXISTS WorldMapChunkPosition (");
        sb.AppendLine("Id INTEGER PRIMARY KEY,");
        sb.AppendLine("WorldMapChunkId INTEGER,");
        sb.AppendLine("X INTEGER,");
        sb.AppendLine("Y INTEGER");
        sb.AppendLine(")");
            
        return sb.ToString();
    }

    private static string CreateTableWorldMapLevel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("CREATE TABLE IF NOT EXISTS WorldMapLevel (");
        sb.AppendLine("Id INTEGER PRIMARY KEY,");
        sb.AppendLine("WorldMapChunkId INTEGER,");
        sb.AppendLine("LevelPart INTEGER");
        sb.AppendLine(")");
            
        return sb.ToString();
    }

    private static string CreateTableMapTile()
    {
        var sb = new StringBuilder();
        sb.AppendLine("CREATE TABLE IF NOT EXISTS MapTile (");
        sb.AppendLine("Id INTEGER PRIMARY KEY,");
        sb.AppendLine("WorldMapLevelId INTEGER,");
        sb.AppendLine("MapTileX INTEGER,");
        sb.AppendLine("MapTileY INTEGER,");
        sb.AppendLine("TilemapPart INTEGER");
        sb.AppendLine(")");

        return sb.ToString();
    }
}