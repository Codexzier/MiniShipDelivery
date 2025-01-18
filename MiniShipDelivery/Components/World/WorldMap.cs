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
        this.WorldMapChunk.WorldMapLevels = new WorldMapLevel[Enum.GetValues<LayerPart>().Length];
        
        // street
        this.WorldMapChunk.WorldMapLevels[(int)LayerPart.Street] = this.CreateWorldMapLevel(LayerPart.Street, true);
        
        // sidewalk and grass
        this.WorldMapChunk.WorldMapLevels[(int)LayerPart.Sidewalk] = this.CreateWorldMapLevel(LayerPart.Sidewalk, false);
        this.WorldMapChunk.WorldMapLevels[(int)LayerPart.Grass] = this.CreateWorldMapLevel(LayerPart.Grass, false);
        
        // TODO: add more levels
        
        // roof
        this.WorldMapChunk.WorldMapLevels[(int)LayerPart.GrayRoof] = this.CreateWorldMapLevel(LayerPart.GrayRoof, false);
        this.WorldMapChunk.WorldMapLevels[(int)LayerPart.BrownRoof] = this.CreateWorldMapLevel(LayerPart.BrownRoof, false);
    }
    
    private WorldMapLevel CreateWorldMapLevel(LayerPart layerPart, bool fill)
    {
        const int fieldXy = 10;
        
        var wml = new WorldMapLevel
        {
            LayerPart = layerPart,
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
                    NumberPart = GetDefaultNumberByLevelPart(layerPart, fill),
                    Position = new TilePosition(indexX * 16, indexY * 16)
                };
            }
        }

        return wml;
    }
    
    private int GetDefaultNumberByLevelPart(LayerPart layerPart, bool fill)
    {
        var defaultNumber = 0;
        
        if(!fill) return defaultNumber;
        
        switch (layerPart)
        {
            case LayerPart.Street:
                defaultNumber = (int)StreetPart.Street01;
                break;
            case LayerPart.Sidewalk:
            case LayerPart.Grass:
            case LayerPart.GrayRoof:
            case LayerPart.BrownRoof:
                defaultNumber = (int)TilemapPart.MiddleMiddle;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(layerPart), layerPart, null);
        }
        
        return defaultNumber;
    }

    public bool ValidTileNumber(int selectedTilemapPart, LayerPart tilemapLayer)
    {
        return this.WorldMapChunk.WorldMapLevels[(int)tilemapLayer]
            .ListOfValidateTileNumbers.Contains(selectedTilemapPart);   
    }

    public void DrawAllLevels(
        SpriteBatch spriteBatch, 
        WorldMapTextures worldMapTextures)
    {
        foreach (var worldMapLevel in this.WorldMapChunk.WorldMapLevels)
        {
            for (var y = 0; y < worldMapLevel.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                {
                    var tileNumber = worldMapLevel.Map[y][x].NumberPart;
                    
                    if(tileNumber == 0) continue;
                    
                    if (!worldMapLevel.ListOfValidateTileNumbers.Contains((int)tileNumber))
                    {
                        tileNumber = this.GetDefaultNumberByLevelPart(worldMapLevel.LayerPart, true); 
                        // (int)TilemapPart.AroundOutBorder;
                    }
                    
                    this.DrawSpriteByLevelPart(
                        spriteBatch,
                        worldMapTextures,
                        worldMapLevel.LayerPart,
                        tileNumber,
                        worldMapLevel.Map[y][x].Position.TilePositionToVector());
                }
            }
        }
    }
    
    private void DrawSpriteByLevelPart(
        SpriteBatch spriteBatch,
        WorldMapTextures worldMapTextures,
        LayerPart layerPart,
        int numberPart,
        Vector2 position)
    {
        switch (layerPart)
        {
            case LayerPart.Street:
                spriteBatch.Draw(
                    worldMapTextures.TexturesStreet.Texture, 
                    position, 
                    worldMapTextures.TexturesStreet.SpriteContent[(StreetPart)numberPart],
                    Color.White);
                break;
            case LayerPart.Sidewalk:
            case LayerPart.Grass:
            case LayerPart.GrayRoof:
            case LayerPart.BrownRoof:
                spriteBatch.Draw(
                    worldMapTextures.TexturesTilemap.Texture, 
                    position, 
                    worldMapTextures.TexturesTilemap.GetSprite(layerPart, (TilemapPart)numberPart),
                    Color.White);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(layerPart), layerPart, null);
        }
        
    }

    public bool TryTilemap(LayerPart layerPart, int x, int y, out MapTile mapTile)
    {
        mapTile = null;
        
        var map = this.WorldMapChunk.WorldMapLevels[(int)layerPart].Map;
        
        if(x < 0 || y < 0 || y >= map.Length || 
           x >= map[y].Length) return false;
        
        mapTile = map[y][x];
        
        return true;
    }
}