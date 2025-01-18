using System;
using System.IO;
using System.Linq;
using System.Text;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.World;
using MiniShipDelivery.Components.World.Textures;
using Newtonsoft.Json;

namespace MiniShipDelivery.Components.Persistence;

public class PersistenceManager : GameComponent
{
    private readonly string _mapFileJson = $"{Environment.CurrentDirectory}/map00.json";
    private readonly WorldManager _world;
    
    public PersistenceManager(Game game) : base(game)
    {
        this._world = game.GetComponent<WorldManager>();
        
        NewMapEvent += this.NewMapReset;
        LoadMapFromFileEvent += this.LoadMapFromFile;
        SaveMapToFileEvent += this.SaveMapToFile;
    }

    public static void NewMap()
    {
        NewMapEvent?.Invoke();
    }
    public static void SaveMap()
    {
        SaveMapToFileEvent?.Invoke();
    }
    public static void LoadMap()
    {
        LoadMapFromFileEvent?.Invoke();
    }
    
    private void NewMapReset()
    {
        foreach (var worldMapLevel in this._world.Map.WorldMapChunk.WorldMapLevels)
        {
            for (var y = 0; y < worldMapLevel.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                {
                    worldMapLevel.Map[y][x].AssetNumber = (int)StreetPart.Street01;
                }
            }
        }
    }
    
    private void SaveMapToFile()
    {
       var saveContent = JsonConvert.SerializeObject(this._world.Map.WorldMapChunk);
       File.WriteAllText(this._mapFileJson, saveContent);
    }

    private void LoadMapFromFile()
    {
        if(!File.Exists(this._mapFileJson)) return;

        try
        {
            var content = File.ReadAllText(this._mapFileJson);
            var worldMapChunk = JsonConvert.DeserializeObject<WorldMapChunk>(content);
            this._world.Map.WorldMapChunk = worldMapChunk;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }  
    }
        
    #region delegates
    
    private delegate void NewMapDelegateEventHandler();
    private static event NewMapDelegateEventHandler NewMapEvent;
    private delegate void SaveMapToFileDelegateEventHandler();
    private static event SaveMapToFileDelegateEventHandler SaveMapToFileEvent;
    private delegate void LoadMapFromFileDelegateEventHandler();
    private static event LoadMapFromFileDelegateEventHandler LoadMapFromFileEvent;
    
    #endregion


}