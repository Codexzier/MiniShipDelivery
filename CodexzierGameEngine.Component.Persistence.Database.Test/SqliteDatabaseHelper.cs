namespace CodexzierGameEngine.Component.Persistence.Database.Test;

public static class SqliteDatabaseHelper
{
    private static readonly string _databasePath = $"{Environment.CurrentDirectory}//worldMap";

    public static string GetDatabasePath(int id)
    {
        return $"{_databasePath}_{id}.db";
    }
}