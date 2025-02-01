using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public WorldMapChunk WorldMapChunk { get; set; } = new();

    public MiniMapChunks[] MiniMapChunks { get; set; } = new[] { new MiniMapChunks() };

    public WorldMap()
    {
        this.WorldMapChunk.WorldMapLayers = WorldMapHelper.CreateWorldMapLayers();
    }

    public void Update()
    {
        if(this.MiniMapChunks.All(a => this.WorldMapChunk.Id == a.WorldMapChunkId)) return;

        foreach (var worldMapLayer in WorldMapChunk.WorldMapLayers)
        {
            if(worldMapLayer.MapLayer == MapLayer.Colliders) continue;

            for (int indexY = 0; indexY < worldMapLayer.Map.Length; indexY++)
            {
                if (this.MiniMapChunks[0].MiniMap == null)
                {
                    this.MiniMapChunks[0].MiniMap = new Color[10][];
                }
                
                if(this.MiniMapChunks[0].MiniMap[indexY] == null)
                {
                    this.MiniMapChunks[0].MiniMap[indexY] = new Color[10];
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
                    
                    this.MiniMapChunks[0].MiniMap[indexY][indexX] = color;
                }
            }
        }
    }
    
    public void DrawAllLayers(SpriteBatch spriteBatch, bool drawTop = false)
    {
        foreach (var worldMapLayer in this.WorldMapChunk.WorldMapLayers)
        {
            if(worldMapLayer.MapLayer == MapLayer.Colliders &&
               GlobaleGameParameters.HudView != HudOptionView.MapEditor) continue;
            
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
                        worldMapLayer.Map[y][x].Position.TilePositionToVector(),
                        worldMapLayer.MapLayer,
                        tileNumber,
                        drawTop);
                }
            }
        }
    }
    
    #region world map help methods
    
    public bool TryTilemap(MapLayer mapLayer, int x, int y, out MapTile mapTile)
    {
        mapTile = null;
        
        var map = this.WorldMapChunk.WorldMapLayers.FirstOrDefault(layer => layer.MapLayer == mapLayer)?.Map;
        
        if(map == null) return false;
        
        if(x < 0 || y < 0 || y >= map.Length || 
           x >= map[y].Length) return false;
        
        mapTile = map[y][x];
        
        return true;
    }
    
    public bool ValidTileNumber(int selectedTilemapPart, MapLayer tilemapMapLayer)
    {
        var wm = this.WorldMapChunk.WorldMapLayers.FirstOrDefault(layer => layer.MapLayer == tilemapMapLayer);
        
        return wm != null && wm.ListOfValidateTileNumbers.Contains(selectedTilemapPart);
    }
    
    #endregion
}