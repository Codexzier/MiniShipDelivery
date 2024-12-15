using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Tilemap
{
    public interface ITilemapProperties
    {
        IDictionary<TilemapPart, Rectangle> Tilemaps { get; }
    }
}