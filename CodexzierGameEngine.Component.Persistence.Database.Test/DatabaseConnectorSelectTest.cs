using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database.Test;

[TestClass]
public class DatabaseConnectorSelectGetByIdTest
{
    [TestMethod]
    public void GetById()
    {
        // arrange
        var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));

        // act
        var result = instance.GetById<WorldMapChunk>(1);

        // assert
        Assert.AreEqual(1, result.Id);
    }
}