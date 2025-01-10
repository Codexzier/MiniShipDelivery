using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database.Test;

[TestClass]
public class DatabaseConnectorSelectGetAllTest
{
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