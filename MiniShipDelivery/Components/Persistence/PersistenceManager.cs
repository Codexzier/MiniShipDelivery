using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.World;
using Newtonsoft.Json;

namespace MiniShipDelivery.Components.Persistence;

public class PersistenceManager : GameComponent
{
    private readonly string _mapDirectory = $"{Environment.CurrentDirectory}/maps";
    private readonly string _mapDefaultFilename = "/MAP00.json";
    private readonly WorldManager _world;
    public static readonly List<string> MapFilenames = new();
    
    private int _chunkIndex = 0;
    
    public PersistenceManager(Game game) : base(game)
    {
        this._world = game.GetComponent<WorldManager>();
        
        NewMapEvent += this.NewMapReset;
        LoadMapFromFileEvent += this.LoadMapFromFile;
        SaveMapToFileEvent += this.SaveMapToFile;
        ReloadMapListEvent += this.LoadMapList;

        if (!Directory.Exists(this._mapDirectory))
        {
            Directory.CreateDirectory(this._mapDirectory);
        }
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
    
    public static void ReloadMapList()
    {
        ReloadMapListEvent?.Invoke();
    }

    private void LoadMapList()
    {
        MapFilenames.Clear();
        var files = Directory.GetFiles(this._mapDirectory, "*.json");
        foreach (var filename in files)
        {
            var fi = new FileInfo(filename);
            
            MapFilenames.Add(fi.Name);
        }
    }
    
    public delegate void ReloadMapListEventHandler();
    public static event ReloadMapListEventHandler ReloadMapListEvent;
    
    private void NewMapReset()
    {
        this._world.Map.WorldMapChunks[this._chunkIndex].WorldMapLayers = WorldMapHelper.CreateWorldMapLayers();
        var ml = Enum.GetValues<MapLayer>();
        var n = this._world.Map.WorldMapChunks[this._chunkIndex].WorldMapLayers.Select(s => s.MapLayer).Distinct();
        if (ml.Length != n.Count())
        {
            throw new MapSetupException("map layers are not the same count.");
        }
    }
    
    private void SaveMapToFile(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            filename = this._mapDefaultFilename;
        }

        foreach (var worldMapChunk in this._world.Map.WorldMapChunks)
        {
            var fullFilename = $"{this._mapDirectory}/{filename}_{worldMapChunk.Id}.json";
        
            var saveContent = JsonConvert.SerializeObject(worldMapChunk);
            File.WriteAllText(fullFilename, saveContent);
        }
    }

    private void LoadMapFromFile(string selectedFilename)
    {
        var fullname = $"{this._mapDirectory}/{selectedFilename}";
        
        //if(!File.Exists(fullname)) return;

        if (string.IsNullOrEmpty(selectedFilename))
        {
            fullname = $"{this._mapDirectory}{this._mapDefaultFilename}";
        }

        try
        {
            var filenames = Directory.GetFiles(this._mapDirectory, "*.json");
            var worldMapChunks = new List<WorldMapChunk>();
            foreach (var filename in filenames)
            {
                var content = File.ReadAllText(filename);
                var worldMapChunk = JsonConvert.DeserializeObject<WorldMapChunk>(content);
                worldMapChunks.Add(worldMapChunk);
            }
            
            if(worldMapChunks == null || worldMapChunks.Count == 0)
            {
                throw new MapSetupException("map layers are not the same count.");
            }

            for (int i = 0; i < this._world.Map.WorldMapChunks.Length; i++)
            {
                var worldMapChunk = worldMapChunks.FirstOrDefault(s => s.Id == this._world.Map.WorldMapChunks[i].Id);
                
                if (worldMapChunk == null) continue;
                
                this._world.Map.WorldMapChunks[i].WorldMapLayers = WorldMapHelper
                    .CheckGetWorldMap(
                        this._world.Map.WorldMapChunks[i].WorldMapLayers, 
                        worldMapChunk.WorldMapLayers);
            }
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