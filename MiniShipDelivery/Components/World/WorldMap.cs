using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public WorldMapChunk WorldMapChunk { get; } = new(); 
    //public readonly IDictionary<LevelPart, WorldMapLevel> WorldMapLevels = new Dictionary<LevelPart, WorldMapLevel>();

    public WorldMap()
    {
        // this.WorldMapLevels.Add(LevelPart.Sidewalk, new WorldMapLevel(LevelPart.Sidewalk, true));
        // this.WorldMapLevels.Add(LevelPart.Grass, new WorldMapLevel(LevelPart.Grass, false));
        //
        // this.WorldMapLevels.Add(LevelPart.GrayRoof, new WorldMapLevel(LevelPart.GrayRoof, false));
        // this.WorldMapLevels.Add(LevelPart.BrownRoof, new WorldMapLevel(LevelPart.BrownRoof, false));
    }

    public bool ValidTileNumber(int selectedTilemapPart, LevelPart tilemapLevel)
    {
        return this.WorldMapChunk.WorldMapLevels[(int)tilemapLevel]
            .ListOfValidateTileNumbers.Contains(selectedTilemapPart);   
        //return this.WorldMapLevels[tilemapLevel].ListOfValidateTileNumbers.Contains(selectedTilemapPart);
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