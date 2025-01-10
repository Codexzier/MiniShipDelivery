using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database.Test
{
    [TestClass]
    public class DatabaseConnectorCreateTableTest
    {
        [TestMethod]
        public void CreateTableWorldMapChunk()
        {
            // arrange
            var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));

            // act
            instance.Create<WorldMapChunk>();

            // assert
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void CreateTableWorldMapChunkPosition()
        {
            // arrange
            var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));

            // act
            instance.Create<WorldMapChunkPosition>();

            // assert
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void CreateTableWorldMapLevel()
        {
            // arrange
            var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));

            // act
            instance.Create<WorldMapLevel>();

            // assert
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void CreateTableMapTile()
        {
            // arrange
            var instance = new DatabaseConnectorTable(SqliteDatabaseHelper.GetDatabasePath(1));

            // act
            instance.Create<MapTile>();

            // assert
            Assert.IsTrue(true);
        }
    }
}
