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
    private readonly string _mapFileJson = $"{Environment.CurrentDirectory}/MAP00.json";
    private readonly WorldManager _world;
    public static readonly List<string> MapFilenames = new();
    
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
    public static void SaveMap(string filename)
    {
        SaveMapToFileEvent?.Invoke(filename);
    }
    public static void LoadMap(string selectedFilename)
    {
        LoadMapFromFileEvent?.Invoke(selectedFilename);
    }
    
    public static void SetFileList()
    {
        MapFilenames.Clear();
        var files = Directory.GetFiles(Environment.CurrentDirectory, "*.json");
        foreach (var filename in files)
        {
            var fi = new FileInfo(filename);
            
            if(!fi.Name.ToLower().StartsWith("map")) continue;
            
            MapFilenames.Add(fi.Name);
        }
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
    
    private void SaveMapToFile(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            filename = "00";
        }
        
        var fullFilename = $"{Environment.CurrentDirectory}/MAP{filename}.json";
        
       var saveContent = JsonConvert.SerializeObject(this._world.Map.WorldMapChunk);
       File.WriteAllText(fullFilename, saveContent);
    }

    private void LoadMapFromFile(string selectedFilename)
    {
        if(!File.Exists(this._mapFileJson)) return;

        string fullFilename = $"{Environment.CurrentDirectory}/{selectedFilename}";
        if (string.IsNullOrEmpty(selectedFilename))
        {
            fullFilename = this._mapFileJson;
        }

        try
        {
            var content = File.ReadAllText(fullFilename);
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
    private delegate void SaveMapToFileDelegateEventHandler(string filename);
    private static event SaveMapToFileDelegateEventHandler SaveMapToFileEvent;
    private delegate void LoadMapFromFileDelegateEventHandler(string selectedFilename);
    private static event LoadMapFromFileDelegateEventHandler LoadMapFromFileEvent;
    
    #endregion

}