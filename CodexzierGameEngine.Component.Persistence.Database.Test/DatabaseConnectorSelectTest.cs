using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database.Test;

[TestClass]
public class DatabaseConnectorSelectTest
{
    [TestMethod]
    public void GetById()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));
        var item = DataModelHelper.CreateWorldMapChunk();

        instance.Insert(item);

        // act
        var result = instance.GetById<WorldMapChunk>(1);

        // assert
        Assert.AreEqual(item, result);
    }
        
    [TestMethod]
    public void GetAll()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));
        var item = DataModelHelper.CreateWorldMapChunk();

        instance.Insert(item);

        // act
        var result = instance.GetAll<WorldMapChunk>();

        // assert
        Assert.AreEqual(item, result.First());
    }
}