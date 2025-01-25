using System;
using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets
{
    public interface ISpriteContent<TAssetPart> where TAssetPart : Enum
    {
        SpriteSetup GetSprite(MapLayer mapLayer, int numberPart);
        IDictionary<TAssetPart, SpriteSetup> SpriteContent { get; }
        
        Texture2D Texture { get; }
    }
}