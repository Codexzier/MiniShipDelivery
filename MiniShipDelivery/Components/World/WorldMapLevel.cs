using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;

namespace MiniShipDelivery.Components.World;

public class WorldMapLevel
{
    public LevelPart LevelPart;
    public MapTile[][] Map;
    
    public int[] ListOfValidateTileNumbers;
}