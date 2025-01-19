using System;
using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteProperties<TAssetPart> where TAssetPart : Enum
    {
        IDictionary<TAssetPart, Rectangle> SpriteContent { get; }
        
        Texture2D Texture { get; }

        Rectangle GetSprite(MapLayer mapLayer, TAssetPart numberPart);
    }
}