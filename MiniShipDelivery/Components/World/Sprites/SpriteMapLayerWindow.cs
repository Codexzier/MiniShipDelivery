using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Windows", 8, true)]
public class SpriteMapLayerWindow : BaseSpriteContent, IMapEditableContent
{
    public SpriteMapLayerWindow(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Map/Windows");
        this.SpriteContent = SpriteMapHelper.GetSpriteSetups(this.Texture, true);
    }

    public SpriteSetup GetSprite(int numberPart) => this.SpriteContent[numberPart];
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public int NumberPartForIcon => 1;
    public MapLayer Layer => MapLayer.Windows;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}