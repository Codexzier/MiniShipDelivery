using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public WorldMapChunk WorldMapChunk { get; set; } = new();

    public WorldMap()
    {
        this.WorldMapChunk.WorldMapLayers = WorldMapHelper.CreateWorldMapLayers();
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