using System;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public WorldMapChunk WorldMapChunk { get; set; } = new();

    public WorldMap()
    {
        this.WorldMapChunk.WorldMapLevels = new WorldMapLevel[Enum.GetValues<MapLayer>().Length];
        
        // street
        this.WorldMapChunk.WorldMapLevels[(int)MapLayer.Street] = this.CreateWorldMapLevel(MapLayer.Street, true);
        
        // sidewalk and grass
        this.WorldMapChunk.WorldMapLevels[(int)MapLayer.Sidewalk] = this.CreateWorldMapLevel(MapLayer.Sidewalk, false);
        this.WorldMapChunk.WorldMapLevels[(int)MapLayer.Grass] = this.CreateWorldMapLevel(MapLayer.Grass, false);
        
        // TODO: add more layers
        
        // roof
        this.WorldMapChunk.WorldMapLevels[(int)MapLayer.GrayRoof] = this.CreateWorldMapLevel(MapLayer.GrayRoof, false);
        this.WorldMapChunk.WorldMapLevels[(int)MapLayer.BrownRoof] = this.CreateWorldMapLevel(MapLayer.BrownRoof, false);
    }
    
    private WorldMapLevel CreateWorldMapLevel(MapLayer mapLayer, bool fill)
    {
        const int fieldXy = 10;
        
        var wml = new WorldMapLevel
        {
            MapLayer = mapLayer,
            // collected validate tiles
            ListOfValidateTileNumbers = Enum.GetValues<TilemapPart>().Select(s => (int)s).ToArray(),
            // y, x
            Map = new MapTile[fieldXy][]
        };

        for (int indexY = 0; indexY < fieldXy; indexY++)
        {
            wml.Map[indexY] = new MapTile[fieldXy];
            for (int indexX = 0; indexX < fieldXy; indexX++)
            {
                wml.Map[indexY][indexX] = new MapTile
                {
                    AssetNumber = GetDefaultNumberByLevelPart(mapLayer, fill),
                    Position = new TilePosition(indexX * 16, indexY * 16),
                    WorldMapLayerID = (int)mapLayer
                };
            }
        }

        return wml;
    }
    
    private int GetDefaultNumberByLevelPart(MapLayer mapLayer, bool fill)
    {
        var defaultNumber = 0;
        
        if(!fill) return defaultNumber;
        
        switch (mapLayer)
        {
            case MapLayer.Street:
                defaultNumber = (int)StreetPart.Street01;
                break;
            case MapLayer.Sidewalk:
            case MapLayer.Grass:
            case MapLayer.GrayRoof:
            case MapLayer.BrownRoof:
                defaultNumber = (int)TilemapPart.MiddleMiddle;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mapLayer), mapLayer, null);
        }
        
        return defaultNumber;
    }

    public bool ValidTileNumber(int selectedTilemapPart, MapLayer tilemapMapLayer)
    {
        return this.WorldMapChunk.WorldMapLevels[(int)tilemapMapLayer]
            .ListOfValidateTileNumbers.Contains(selectedTilemapPart);   
    }

    public void DrawAllLayers(SpriteBatch spriteBatch)
    {
        foreach (var worldMapLevel in this.WorldMapChunk.WorldMapLevels)
        {
            for (var y = 0; y < worldMapLevel.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                {
                    var tileNumber = worldMapLevel.Map[y][x].AssetNumber;
                    
                    if(tileNumber == 0) continue;
                    
                    if (!worldMapLevel.ListOfValidateTileNumbers.Contains(tileNumber))
                    {
                        tileNumber = this.GetDefaultNumberByLevelPart(worldMapLevel.MapLayer, true);
                    }
                    
                    spriteBatch.Draw(
                        worldMapLevel.Map[y][x].Position.TilePositionToVector(),
                        worldMapLevel.MapLayer,
                        tileNumber);
                }
            }
        }
    }

    public bool TryTilemap(MapLayer mapLayer, int x, int y, out MapTile mapTile)
    {
        mapTile = null;
        
        var map = this.WorldMapChunk.WorldMapLevels[(int)mapLayer].Map;
        
        if(x < 0 || y < 0 || y >= map.Length || 
           x >= map[y].Length) return false;
        
        mapTile = map[y][x];
        
        return true;
    }
}