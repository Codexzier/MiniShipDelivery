using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets.Textures
{
    public interface ISpriteProperties<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, Rectangle> SpriteContent { get; }
        Texture2D Texture { get; }
    }
}