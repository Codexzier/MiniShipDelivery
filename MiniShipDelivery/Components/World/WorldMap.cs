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
        this.WorldMapChunk.WorldMapLevels = new WorldMapLevel[Enum.GetValues<LevelPart>().Length];
        
        // street
        this.WorldMapChunk.WorldMapLevels[(int)LevelPart.Street] = this.CreateWorldMapLevel(LevelPart.Street, true);
        
        // sidewalk and grass
        this.WorldMapChunk.WorldMapLevels[(int)LevelPart.Sidewalk] = this.CreateWorldMapLevel(LevelPart.Sidewalk, false);
        this.WorldMapChunk.WorldMapLevels[(int)LevelPart.Grass] = this.CreateWorldMapLevel(LevelPart.Grass, false);
        
        // TODO: add more levels
        
        // roof
        this.WorldMapChunk.WorldMapLevels[(int)LevelPart.GrayRoof] = this.CreateWorldMapLevel(LevelPart.GrayRoof, false);
        this.WorldMapChunk.WorldMapLevels[(int)LevelPart.BrownRoof] = this.CreateWorldMapLevel(LevelPart.BrownRoof, false);
    }
    
    private WorldMapLevel CreateWorldMapLevel(LevelPart levelPart, bool fill)
    {
        const int fieldXy = 10;
        
        var wml = new WorldMapLevel
        {
            LevelPart = levelPart,
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
                    NumberPart = GetDefaultNumberByLevelPart(levelPart, fill),
                    Position = new TilePosition(indexX * 16, indexY * 16)
                };
            }
        }

        return wml;
    }
    
    private int GetDefaultNumberByLevelPart(LevelPart levelPart, bool fill)
    {
        var defaultNumber = 0;
        
        if(!fill) return defaultNumber;
        
        switch (levelPart)
        {
            case LevelPart.Street:
                defaultNumber = (int)StreetPart.Street01;
                break;
            case LevelPart.Sidewalk:
            case LevelPart.Grass:
            case LevelPart.GrayRoof:
            case LevelPart.BrownRoof:
                defaultNumber = (int)TilemapPart.MiddleMiddle;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(levelPart), levelPart, null);
        }
        
        return defaultNumber;
    }

    public bool ValidTileNumber(int selectedTilemapPart, LevelPart tilemapLevel)
    {
        return this.WorldMapChunk.WorldMapLevels[(int)tilemapLevel]
            .ListOfValidateTileNumbers.Contains(selectedTilemapPart);   
    }

    public void DrawAllLevels(SpriteBatch spriteBatch, TexturesTilemap texturesTilemap, TexturesStreet texturesStreet)
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
                        tileNumber = this.GetDefaultNumberByLevelPart(worldMapLevel.LevelPart, true); // (int)TilemapPart.AroundOutBorder;
                    }
                    
                    this.DrawSpriteByLevelPart(
                        spriteBatch,
                        texturesTilemap,
                        texturesStreet,
                        worldMapLevel.LevelPart,
                        tileNumber,
                        worldMapLevel.Map[y][x].Position.TilePositionToVector());
                    
                    // spriteBatch.Draw(
                    //     texturesTilemap.Texture, 
                    //     worldMapLevel.Map[y][x].Position.TilePositionToVector(), 
                    //     texturesTilemap.GetSprite(worldMapLevel.LevelPart, (TilemapPart)tileNumber),
                    //     Color.White);
                }
            }
        }
    }
    
    private void DrawSpriteByLevelPart(SpriteBatch spriteBatch,
        TexturesTilemap texturesTilemap,
        TexturesStreet texturesStreet,
        LevelPart levelPart,
        int numberPart,
        Vector2 position)
    {
        switch (levelPart)
        {
            case LevelPart.Street:
                spriteBatch.Draw(
                    texturesStreet.Texture, 
                    position, 
                    texturesStreet.SpriteContent[(StreetPart)numberPart],
                    Color.White);
                break;
            case LevelPart.Sidewalk:
            case LevelPart.Grass:
            case LevelPart.GrayRoof:
            case LevelPart.BrownRoof:
                spriteBatch.Draw(
                    texturesTilemap.Texture, 
                    position, 
                    texturesTilemap.GetSprite(levelPart, (TilemapPart)numberPart),
                    Color.White);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(levelPart), levelPart, null);
        }
        
    }

    public bool TryTilemap(LevelPart levelPart, int x, int y, out MapTile mapTile)
    {
        mapTile = null;
        
        var map = this.WorldMapChunk.WorldMapLevels[(int)levelPart].Map;
        
        if(x < 0 || y < 0 || y >= map.Length || 
           x >= map[y].Length) return false;
        
        mapTile = map[y][x];
        
        return true;
    }
}