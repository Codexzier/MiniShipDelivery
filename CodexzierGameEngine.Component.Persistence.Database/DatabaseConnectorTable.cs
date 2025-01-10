using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;

namespace CodexzierGameEngine.Component.Persistence.Database
{
    public class DatabaseConnectorTable : IDatabaseConnector
    {
        #region interface methods
        
        public void Create<TDataModel>()
        {
            this.Execute(CommandCreateTableHost.CommandDictionary[typeof(TDataModel)]());
        }

        public void Insert<TDataModel>(TDataModel world)
        {
            this.Execute(CommandInsertTableHost.GetCommandByDataModel(world));
        }

        public IEnumerable<TDataModel> GetAll<TDataModel>()
        {
            var tt = this.ExecuteWithReturn<TDataModel>(CommandSelectTableHost.GetCommandByDataModel<TDataModel>());

            return tt;
        }

        public TDataModel GetById<TDataModel>(long id)
        {
            throw new NotImplementedException();
        }

        public void Update<TDataModel>(TDataModel worldMapChunk)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        private readonly string _database;

        public DatabaseConnectorTable(string databasePath)
        {
            if (string.IsNullOrEmpty(databasePath))
            {
                throw new SqLiteDatabaseException("path is empty");
            }

            this._database = databasePath;
        }

        #region base methods
        
        private void Execute(string commandSql)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={this._database}");
                connection.Open();
            
                using var command = connection.CreateCommand();
                command.CommandText = commandSql;
            
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new SqLiteDatabaseException(
                    $"Fail to execute sql command. Database: {this._database}",
                    ex);
            }
        }
        
        private IEnumerable<TDataModel> ExecuteWithReturn<TDataModel>(string commandSql)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={this._database}");
                connection.Open();
            
                using var command = connection.CreateCommand();
                command.CommandText = commandSql;
                var reader = command.ExecuteReader();

                var list = new List<TDataModel>();
                while (reader.Read())
                {
                    //reader.GetName()
                }
      
                return list;
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

    public class CommandSelectTableHost
    {
        public static string GetCommandByDataModel<TDataModel>()
        {
            throw new NotImplementedException();
        }
    }
}
