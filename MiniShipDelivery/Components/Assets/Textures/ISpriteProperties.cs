using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.Assets.Textures
{
    public interface ISpriteProperties<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, Rectangle> SpriteContent { get; }
    }
}