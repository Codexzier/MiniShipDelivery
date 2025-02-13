using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using WorldMapChunk = CodexzierGameEngine.DataModels.World.WorldMapChunk;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public WorldMapChunk[] WorldMapChunks { get; set; } = new WorldMapChunk[2];

    public MiniMapChunks[] MiniMapChunks { get; set; } = new[]
    {
        new MiniMapChunks{ WorldMapChunkId = 0 },
        new MiniMapChunks{ WorldMapChunkId = 1 }
    };

    public WorldMap()
    {
        this.WorldMapChunks[0] = new WorldMapChunk 
            { Id = 0, Position = new WorldMapChunkPosition { Id = 0, X = 0, Y = 0 } };
        this.WorldMapChunks[1] = new WorldMapChunk 
            { Id = 1, Position = new WorldMapChunkPosition { Id = 1, X = 1, Y = 0 } };

        foreach (var worldMapChunk in this.WorldMapChunks)
        {
            worldMapChunk.WorldMapLayers = WorldMapHelper.CreateWorldMapLayers();
        }
    }

    public void Update()
    {
        if(this.MiniMapChunks.All(a => this.WorldMapChunks.Any(ia => ia.Id == a.WorldMapChunkId))) return;
        for (int chunkIndex = 0; chunkIndex < this.WorldMapChunks.Length; chunkIndex++)
        {
            foreach (var worldMapLayer in this.WorldMapChunks[chunkIndex].WorldMapLayers)
            {
                if(worldMapLayer.MapLayer == MapLayer.Colliders) continue;

                for (int indexY = 0; indexY < worldMapLayer.Map.Length; indexY++)
                {
                    if (this.MiniMapChunks[chunkIndex].MiniMap == null)
                    {
                        this.MiniMapChunks[chunkIndex].MiniMap = new Color[10][];
                    }
                
                    if(this.MiniMapChunks[chunkIndex].MiniMap[indexY] == null)
                    {
                        this.MiniMapChunks[chunkIndex].MiniMap[indexY] = new Color[10];
                    }
                
                    for (int indexX = 0; indexX < worldMapLayer.Map[indexY].Length; indexX++)
                    {
                        if(worldMapLayer.Map[indexY][indexX].AssetNumber == 0) continue;
                    
                        Color color = Color.Transparent;
                        switch (worldMapLayer.MapLayer)
                        {
                            case MapLayer.Street:
                                color = Color.Gray;
                                break;
                            case MapLayer.Sidewalk:
                                color = Color.LightGray;
                                break;
                            case MapLayer.BuildingRed:
                                color = Color.Red;
                                break;
                            case MapLayer.BuildingBrown:
                                color = Color.Brown;
                                break;
                            case MapLayer.Grass:
                                color = Color.LightGreen;
                                break;
                        }
                    
                        if(color == Color.Transparent) continue;
                    
                        this.MiniMapChunks[chunkIndex].MiniMap[indexY][indexX] = color;
                    }
                }
            }
        }
        
    }
    
    public void DrawAllLayers(SpriteBatch spriteBatch, bool drawTop = false)
    {
        for (int chunkIndex = 0; chunkIndex < this.WorldMapChunks.Length; chunkIndex++)
        {
            var position = new Vector2(
                this.WorldMapChunks[chunkIndex].Position.X * 160,
                this.WorldMapChunks[chunkIndex].Position.Y * 160);
        foreach (var worldMapLayer in this.WorldMapChunks[chunkIndex].WorldMapLayers)
        {
            if(worldMapLayer.MapLayer == MapLayer.Colliders &&
               GlobalGameParameters.HudView != HudOptionView.MapEditor) continue;
            
            for (var y = 0; y < worldMapLayer.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLayer.Map[y].Length; x++)
                {           
                    var tileNumber = worldMapLayer.Map[y][x].AssetNumber;
                    
                    if(tileNumber == 0) continue;
                    
                    if (!worldMapLayer.ListOfValidateTileNumbers.Contains(tileNumber))
                    {
                        tileNumber = WorldMapHelper.GetDefaultNumberByLevelPart(worldMapLayer.MapLayer, true);
                    }
                    
                    spriteBatch.Draw(
                        worldMapLayer.Map[y][x].Position.TilePositionToVector() + position,
                        worldMapLayer.MapLayer,
                        tileNumber,
                        drawTop);
                }
            }
        }   
        }
    }
    
    #region world map help methods
    
    public bool TryTilemap(int chunkIndex, MapLayer mapLayer, int x, int y, out MapTile mapTile)
    {
        mapTile = null;
        
        var map = this.WorldMapChunks[chunkIndex].WorldMapLayers.FirstOrDefault(layer => layer.MapLayer == mapLayer)?.Map;
        
        if(map == null) return false;
        
        if(x < 0 || y < 0 || y >= map.Length || 
           x >= map[y].Length) return false;
        
        mapTile = map[y][x];
        
        return true;
    }
    
    public bool ValidTileNumber(int chunkIndex, int selectedTilemapPart, MapLayer tilemapMapLayer)
    {
        var wm = this.WorldMapChunks[chunkIndex].WorldMapLayers.FirstOrDefault(layer => layer.MapLayer == tilemapMapLayer);
        
        return wm != null && wm.ListOfValidateTileNumbers.Contains(selectedTilemapPart);
    }
    
    #endregion
}