using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteContent<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, SpriteSetup> SpriteContent { get; }
        
        Texture2D Texture { get; }
    }

    public class SpriteSetup
    {
        public Rectangle Cutout { get; set; }
        public bool IsTopLayer { get; set; }
        public static SpriteSetup Empty { get; } = new() { Cutout = Rectangle.Empty };
    }
}