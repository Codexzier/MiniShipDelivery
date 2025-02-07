using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Trees", 11, true)]
public class SpriteMapLayerTrees : BaseSpriteContent, IMapEditableContent
{
    public SpriteMapLayerTrees(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Map/Trees");
        this.SpriteContent = SpriteMapHelper.GetSpriteSetups(this.Texture);
    }

    public SpriteSetup GetSprite(int numberPart) => this.SpriteContent[numberPart];
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public int NumberPartForIcon => 7;
    public MapLayer Layer => MapLayer.Trees;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}