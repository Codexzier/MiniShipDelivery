using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MiniShipDelivery.Components.World;

public class WorldMapLevel
{
    public readonly LevelPart LevelPart;
    public readonly MapTile[][] Map;
    public readonly IEnumerable<int> ListOfValidateTileNumbers;

    public WorldMapLevel(LevelPart levelPart, bool fill)
    {
        this.LevelPart = levelPart;
        // collected validate tiles
        this.ListOfValidateTileNumbers = Enum.GetValues<TilemapPart>().Select(s => (int)s);
            
        // y, x
        this.Map = new MapTile[10][];
        for (int indexY = 0; indexY < 10; indexY++)
        {
            this.Map[indexY] = new MapTile[20];
            for (int indexX = 0; indexX < 20; indexX++)
            {
                this.Map[indexY][indexX] = new MapTile(
                    fill ? TilemapPart.MiddleMiddle : TilemapPart.None,
                    new Vector2(indexX * 16, indexY * 16));
            }
        }
    }
}