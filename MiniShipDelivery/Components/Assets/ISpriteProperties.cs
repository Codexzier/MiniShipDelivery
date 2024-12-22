using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteProperties<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, Rectangle> SpriteContent { get; }
    }
}