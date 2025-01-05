using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.HUD.Controls;

public class TexturesUiMenuMainButtons : ISpriteProperties<UiMenuMainPart>
{
    public TexturesUiMenuMainButtons(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Interface/MainMenuButtons");
            
        this.SpriteContent.Add(UiMenuMainPart.None, new Rectangle(0, 0, 0, 0));
        this.SpriteContent.Add(UiMenuMainPart.Start, new Rectangle(0, 0, 64, 16));
        this.SpriteContent.Add(UiMenuMainPart.Continue, new Rectangle(64, 0, 64, 16));
        this.SpriteContent.Add(UiMenuMainPart.Option, new Rectangle(128, 0, 64, 16));
        this.SpriteContent.Add(UiMenuMainPart.MapEditor, new Rectangle(192, 0, 64, 16));
        this.SpriteContent.Add(UiMenuMainPart.Back, new Rectangle(258, 0, 64, 16));
    }

    public IDictionary<UiMenuMainPart, Rectangle> SpriteContent { get; } =
        new Dictionary<UiMenuMainPart, Rectangle>();
    public Texture2D Texture { get; }
}