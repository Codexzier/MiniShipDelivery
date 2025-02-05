using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Doors", 9, true)]
public class SpriteMapLayerDoor : IMapEditableContent, ISpriteMapContent
{
    public SpriteMapLayerDoor(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Map/Doors");
        this.SpriteContent = SpriteMapHelper.GetSpriteSetups(this.Texture);
    }

    public SpriteSetup GetSprite(int numberPart) => this.SpriteContent[numberPart];
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public IDictionary<int, SpriteSetup> SpriteContent { get; }
    public Texture2D Texture { get; }
    public int NumberPartForIcon => 1;
    public Type EnumType => null;
    public MapLayer Layer => MapLayer.Doors;
    public int SpriteCount => this.SpriteContent.Count;
    public bool HasSpecificNumberPart => false;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}