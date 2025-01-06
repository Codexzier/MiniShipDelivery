using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World;

public class WorldMap
{
    public readonly IDictionary<LevelPart, WorldMapLevel> WorldMapLevels = new Dictionary<LevelPart, WorldMapLevel>();

    public WorldMap()
    {
        this.WorldMapLevels.Add(LevelPart.Sidewalk, new WorldMapLevel(LevelPart.Sidewalk, true));
        this.WorldMapLevels.Add(LevelPart.Grass, new WorldMapLevel(LevelPart.Grass, false));
        
        this.WorldMapLevels.Add(LevelPart.GrayRoof, new WorldMapLevel(LevelPart.GrayRoof, false));
        this.WorldMapLevels.Add(LevelPart.BrownRoof, new WorldMapLevel(LevelPart.BrownRoof, false));
    }

    public bool ValidTileNumber(int selectedTilemapPart, LevelPart tilemapLevel)
    {
        return this.WorldMapLevels[tilemapLevel].ListOfValidateTileNumbers.Contains(selectedTilemapPart);
    }

    public void DrawAllLevels(SpriteBatch spriteBatch, TexturesTilemap texturesTilemap)
    {
        foreach (var worldMapLevel in this.WorldMapLevels.Values)
        {
            for (var y = 0; y < worldMapLevel.Map.Length; y++)
            {
                for (var x = 0; x < worldMapLevel.Map[y].Length; x++)
                {
                    var tileNumber = worldMapLevel.Map[y][x].TilemapPart;
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
        
        if(x < 0 || y < 0 || y >= this.WorldMapLevels[levelPart].Map.Length || 
           x >= this.WorldMapLevels[levelPart].Map[y].Length) return false;
        
        mapTile = this.WorldMapLevels[levelPart].Map[y][x];
        
        return true;
    }
}