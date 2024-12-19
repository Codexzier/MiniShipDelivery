using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteProperties<TAssetPart>
    {
        IDictionary<TAssetPart, Rectangle> SpriteContent { get; }
    }
}