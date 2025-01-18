using System;
using CodexzierGameEngine.DataModels.World;

namespace MiniShipDelivery.Components.World;

public class MissingMapTexturesAndCutout : Exception
{
    public MissingMapTexturesAndCutout(int numberPart, MapLayer mapLayer)
        : base($"Numberpart: {numberPart} has not been cut out of {mapLayer}")
    {
    }
}