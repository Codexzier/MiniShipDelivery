using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public WorldMapChunk WorldMapChunk { get; set; } = new();

    public WorldMap()
    {
        // sidewalk and grass
        this.WorldMapChunk.WorldMapLevels = new WorldMapLevel[Enum.GetValues<LevelPart>().Length];
        this.WorldMapChunk.WorldMapLevels[(int)LevelPart.Sidewalk] = this.CreateWorldMapLevel(LevelPart.Sidewalk, true);
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
                    TilemapPart = fill ? TilemapPart.MiddleMiddle : TilemapPart.None,
                    Position = new Vector2(indexX * 16, indexY * 16)
                };
            }
        }

        return wml;
    }

    public bool ValidTileNumber(int selectedTilemapPart, LevelPart tilemapLevel)
    {
        return this.WorldMapChunk.WorldMapLevels[(int)tilemapLevel]
            .ListOfValidateTileNumbers.Contains(selectedTilemapPart);   
    }

    public void DrawAllLevels(SpriteBatch spriteBatch, TexturesTilemap texturesTilemap)
    {
        foreach (var worldMapLevel in this.WorldMapChunk.WorldMapLevels)
        {
            for (var y = 0; y < worldMapLevel.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                {
                    var tileNumber = worldMapLevel.Map[y][x].TilemapPart;
                    
                    if(tileNumber == TilemapPart.None) continue;
                    
                    if (!worldMapLevel.ListOfValidateTileNumbers.Contains((int)tileNumber))
                    {
                        tileNumber = TilemapPart.AroundOutBorder;
                    }
                    
                    spriteBatch.Draw(
                        texturesTilemap.Texture, 
                        worldMapLevel.Map[y][x].Position, 
                        texturesTilemap.GetSprite(worldMapLevel.LevelPart, tileNumber),
                        Color.White);
                }
            }
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