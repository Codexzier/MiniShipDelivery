using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteProperties<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, Rectangle> SpriteContent { get; }
        Texture2D Texture { get; }
    }
}