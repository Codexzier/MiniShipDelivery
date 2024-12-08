using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery
{
    public interface ITilemapProperties
    {
        IDictionary<TilemapPart, Rectangle> Tilemaps { get; }
    }
}