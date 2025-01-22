using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.HUD.Controls;

public class TexturesUiMenuMainButtons : ISpriteContent<UiMenuMainPart>
{
    public TexturesUiMenuMainButtons(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Interface/MainMenuButtons");
            
        this.SpriteContent.Add(UiMenuMainPart.None, new SpriteSetup { Cutout = new Rectangle(0, 0, 0, 0)});
        this.SpriteContent.Add(UiMenuMainPart.Start, new SpriteSetup { Cutout = new Rectangle(0, 0, 64, 16)});
        this.SpriteContent.Add(UiMenuMainPart.Continue, new SpriteSetup { Cutout = new Rectangle(64, 0, 64, 16)});
        this.SpriteContent.Add(UiMenuMainPart.Options, new SpriteSetup { Cutout = new Rectangle(128, 0, 64, 16)});
        this.SpriteContent.Add(UiMenuMainPart.MapEditor, new SpriteSetup { Cutout = new Rectangle(192, 0, 64, 16)});
        this.SpriteContent.Add(UiMenuMainPart.Back, new SpriteSetup { Cutout = new Rectangle(256, 0, 64, 16)});
        this.SpriteContent.Add(UiMenuMainPart.Exit, new SpriteSetup { Cutout = new Rectangle(320, 0, 64, 16)});
    }

    public IDictionary<UiMenuMainPart, SpriteSetup> SpriteContent { get; } =
        new Dictionary<UiMenuMainPart, SpriteSetup>();
    public Texture2D Texture { get; }
    public Rectangle GetSprite(MapLayer mapLayer, UiMenuMainPart numberPart) => this.SpriteContent[numberPart].Cutout;
}