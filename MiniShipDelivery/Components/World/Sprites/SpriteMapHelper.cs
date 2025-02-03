using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public static class SpriteMapHelper
{
    public static IDictionary<int,SpriteSetup> GetSpriteSetups(Texture2D texture, bool IsTopLayer = false, bool IsBarrier = false)
    {
        var collection = new Dictionary<int, SpriteSetup>();
        
        collection.Add(0, SpriteSetup.Empty);

        var cols = texture.Width / 16;
        var rows = texture.Height / 16;
        var index = 1;
        for (int row = 0; row < rows; row++)
        {
            for (int colIndex = 0; colIndex < cols; colIndex++)
            {
                collection.Add(index, new SpriteSetup
                {
                    Cutout = new Rectangle(colIndex * 16, row * 16, 16, 16),
                    IsBarrier = IsBarrier,
                    IsTopLayer = IsTopLayer
                });
                index++;
            }
        }
        
        return collection;
    }
}