using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Collider", 100, true)]
public class SpriteMapLayerCollider(Game game) : IMapEditableContent, ISpriteMapContent
{
    public SpriteSetup GetSprite(int numberPart)
    {
        return this.SpriteContent[numberPart];
    }

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;

    public IDictionary<int, SpriteSetup> SpriteContent { get; } = new Dictionary<int, SpriteSetup>()
    {
        { 1, new SpriteSetup{ IsTopLayer = true, IsBarrier = true, Cutout = new Rectangle(0, 0, 16, 16)} }
    };
    public Texture2D Texture { get; } = game.Content.Load<Texture2D>(@"Map\CollisionHelper");
    public int NumberPartForIcon => 1;
    public Type EnumType => null;
    public MapLayer Layer => MapLayer.Colliders;
    public int SpriteCount => this.SpriteContent.Count;
    public bool HasSpecificNumberPart => false;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}