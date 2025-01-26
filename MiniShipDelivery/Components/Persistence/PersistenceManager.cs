using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.World;
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
        this._world.Map.WorldMapChunk.WorldMapLayers = WorldMapHelper.CreateWorldMapLayers();
        var ml = Enum.GetValues<MapLayer>();
        var n = this._world.Map.WorldMapChunk.WorldMapLayers.Select(s => s.MapLayer).Distinct();
        if (ml.Length != n.Count())
        {
            throw new MapSetupException("map layers are not the same count.");
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
            
            if(worldMapChunk.WorldMapLayers == null || worldMapChunk.WorldMapLayers.Length == 0)
            {
                throw new MapSetupException("map layers are not the same count.");
            }
            
            this._world.Map.WorldMapChunk.WorldMapLayers = WorldMapHelper.CheckGetWorldMap(
                this._world.Map.WorldMapChunk.WorldMapLayers, 
                worldMapChunk.WorldMapLayers);
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