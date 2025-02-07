using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Collider", 100, true)]
public class SpriteMapLayerCollider : BaseSpriteContent, IMapEditableContent
{
    public SpriteMapLayerCollider(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>(@"Map\CollisionHelper");
        this.SpriteContent = new Dictionary<int, SpriteSetup>
        {
            { 1, new SpriteSetup{ IsTopLayer = true, IsBarrier = true, Cutout = new Rectangle(0, 0, 16, 16)} },
            { 2, new SpriteSetup{ IsTopLayer = true, IsBarrier = true, Cutout = new Rectangle(16, 0, 16, 16)} },
            { 3, new SpriteSetup{ IsTopLayer = true, IsBarrier = true, Cutout = new Rectangle(32, 0, 16, 16)} }
        };
    }

    public SpriteSetup GetSprite(int numberPart)
    {
        if (numberPart == 3)
        {
            
        }
        
        return this.SpriteContent[numberPart];
    }
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public int NumberPartForIcon => 1;
    public MapLayer Layer => MapLayer.Colliders;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}