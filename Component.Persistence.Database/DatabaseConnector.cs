namespace Component.Persistence.Database
{
    [DatabaseConnectorName("SQLite")]
    public class DatabaseConnector : IDatabaseConnector
    {
        #region interface methods

        public void CreateTable<TTable>()
        {
            this.Execute(db => db.CreateTable<TTable>());
        }

        public IEnumerable<ArticleItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public ArticleItem GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ArticleItem articleItem)
        {
            var statement = SqlHelper.CreateInsertSQL<ArticleItem>();
        }

        public IArticleSubItem SubItem_GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleItem articleItem)
        {
            throw new NotImplementedException();
        }

        public void Update(IArticleSubItem subItem)
        {
            throw new NotImplementedException();
        }

        #endregion

        public DatabaseConnector(string databasePath)
        {
            if (string.IsNullOrEmpty(databasePath))
            {
                throw new SqLiteDatabaseException("path is empty");
            }

            this._database = databasePath;
        }

        public DatabaseConnector()
        {
            this._database = $"{Environment.CurrentDirectory}//warehouse.db";
        }

        #region base methods

        protected readonly string _database;

        protected void Execute(Action<SQLiteConnection> runMethod)
        {
            try
            {

                var connection = new SQLiteConnection(this._database);
                runMethod.Invoke(connection);
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new SqLiteDatabaseException(
                    $"Fail to execute sql command. Database: {this._database}",
                    ex);
            }
        }

        #endregion
    }
}
