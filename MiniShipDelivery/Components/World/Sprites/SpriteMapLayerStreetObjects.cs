using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("StreetObjects", 9, true)]
public class SpriteMapLayerStreetObjects : BaseSpriteContent, IMapEditableContent
{
    public SpriteMapLayerStreetObjects(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Map/StreetObjects");
        this.SpriteContent = SpriteMapHelper.GetSpriteSetups(this.Texture);
    }

    public SpriteSetup GetSprite(int numberPart) => this.SpriteContent[numberPart];
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public int NumberPartForIcon => 1;
    public MapLayer Layer => MapLayer.StreetObjects;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}