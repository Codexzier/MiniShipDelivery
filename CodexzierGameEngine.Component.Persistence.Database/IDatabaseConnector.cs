using CodexzierGameEngine.DataModels.World;

namespace CodexzierGameEngine.Component.Persistence.Database;

public interface IDatabaseConnector
{
    void Create<TDataModel>();
    void Insert<TDataModel>(TDataModel world);
    IEnumerable<TDataModel> GetAll<TDataModel>();
    TDataModel GetById<TDataModel>(long id);
    void Update<TDataModel>(TDataModel worldMapChunk);
}