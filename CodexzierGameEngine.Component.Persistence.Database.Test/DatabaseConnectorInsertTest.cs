namespace CodexzierGameEngine.Component.Persistence.Database.Test;

[TestClass]
public class DatabaseConnectorInsertTest
{
    [TestMethod]
    public void InsertWorldMapChunk()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));
        var item = DataModelHelper.CreateWorldMapChunk();

        // act
        instance.Insert(item);

        // assert
        Assert.IsTrue(true);
    }
        
    [TestMethod]
    public void InsertWorldMapChunkPosition()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));
        var item = DataModelHelper.CreateWorldMapChunkPosition(1, 1, 1);

        // act
        instance.Insert(item);

        // assert
        Assert.IsTrue(true);
    }
    
    [TestMethod]
    public void InsertWorldMapLevel()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));
        var item = DataModelHelper.CreateWorldMapLevel();

        // act
        instance.Insert(item);

        // assert
        Assert.IsTrue(true);
    }
    
    [TestMethod]
    public void InsertMapTile()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));
        var item = DataModelHelper.CreateMapTile();

        // act
        instance.Insert(item);

        // assert
        Assert.IsTrue(true);
    }
}