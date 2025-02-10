using System;

namespace MiniShipDelivery.Components.World.Sprites;

public class MapLayerSetupAttribute : Attribute
{
    public MapLayerSetupAttribute(string name, int order, bool visible)
    {
    }
}