using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor.Options;

namespace MiniShipDelivery.Components.HUD.Editor.Textures
{
    public class SpriteUiMenuEditorOptions(Game game)
        : ISpriteContent<InterfaceMenuEditorOptionPart>
    {
        public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
        {
            throw new System.NotImplementedException();
        }

        public IDictionary<InterfaceMenuEditorOptionPart, SpriteSetup> SpriteContent { get; } = new Dictionary<InterfaceMenuEditorOptionPart, SpriteSetup>
        {
            { InterfaceMenuEditorOptionPart.Save, new SpriteSetup { Cutout = new Rectangle(0, 0, 16, 16) }},
            { InterfaceMenuEditorOptionPart.Load, new SpriteSetup { Cutout = new Rectangle(16, 0, 16, 16) }},
            { InterfaceMenuEditorOptionPart.New, new SpriteSetup { Cutout = new Rectangle(32, 0, 16, 16)}},
            { InterfaceMenuEditorOptionPart.Grid, new SpriteSetup { Cutout = new Rectangle(48, 0, 16, 16) }},
            { InterfaceMenuEditorOptionPart.Close, new SpriteSetup { Cutout = new Rectangle(64, 0, 16, 16) }},
            { InterfaceMenuEditorOptionPart.ConsoleWindow, new SpriteSetup { Cutout = new Rectangle(80, 0, 16, 16) }},
        };

        public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/MenuEditorOptions");
    }
}