using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteContent<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, SpriteSetup> SpriteContent { get; }
        
        Texture2D Texture { get; }
    }
}