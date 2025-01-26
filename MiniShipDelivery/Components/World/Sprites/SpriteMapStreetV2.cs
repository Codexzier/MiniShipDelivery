using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended.Graphics;

namespace MiniShipDelivery.Components.World.Sprites;


[MapLayerSetup("Street", 1, true)]
public class SpriteMapStreetV2 : IMapEditableContent, ISpriteMapContent
{
    public SpriteMapStreetV2(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Map/Street");
        
        this.SpriteContent = SpriteMapHelper.GetSpriteSetups(this.Texture);
    }

    public SpriteSetup GetSprite(int numberPart)
    {
        return this.SpriteContent[numberPart];
    }

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;

    public IDictionary<int, SpriteSetup> SpriteContent { get; } 
    public Texture2D Texture { get; }
    public int NumberPartForIcon => 1;
    public Type EnumType { get; } = null;
    public MapLayer Layer { get; } = MapLayer.Street;
    public int SpriteCount => this.SpriteContent.Count;
    public bool HasSpecificNumberPart { get; } = false;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}

public static class SpriteMapHelper
{
    public static IDictionary<int,SpriteSetup> GetSpriteSetups(Texture2D texture)
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
                    IsBarrier = false,
                    IsTopLayer = false
                });
                index++;
            }
        }
        
        return collection;
    }
}