using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;

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
        foreach (var worldMapLevel in this.WorldMapChunk.WorldMapLayers)
        {
            for (var y = 0; y < worldMapLevel.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                {
                    var tileNumber = worldMapLevel.Map[y][x].AssetNumber;
                    
                    if(tileNumber == 0) continue;
                    
                    //if(drawTop != worldMapLevel.Map[y][x].DrawTop) continue;
                    
                    if (!worldMapLevel.ListOfValidateTileNumbers.Contains(tileNumber))
                    {
                        tileNumber = WorldMapHelper.GetDefaultNumberByLevelPart(worldMapLevel.MapLayer, true);
                    }
                    
                    spriteBatch.Draw(
                        worldMapLevel.Map[y][x].Position.TilePositionToVector(),
                        worldMapLevel.MapLayer,
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
        
        var map = this.WorldMapChunk.WorldMapLayers[(int)mapLayer].Map;
        
        if(x < 0 || y < 0 || y >= map.Length || 
           x >= map[y].Length) return false;
        
        mapTile = map[y][x];
        
        return true;
    }
    
    public bool ValidTileNumber(int selectedTilemapPart, MapLayer tilemapMapLayer)
    {
        return this.WorldMapChunk.WorldMapLayers[(int)tilemapMapLayer]
            .ListOfValidateTileNumbers.Contains(selectedTilemapPart);   
    }
    
    #endregion
}