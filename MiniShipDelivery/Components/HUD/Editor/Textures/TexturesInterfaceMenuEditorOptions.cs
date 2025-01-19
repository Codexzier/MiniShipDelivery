using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor.Options;

namespace MiniShipDelivery.Components.HUD.Editor.Textures
{
    public class TexturesInterfaceMenuEditorOptions(Game game)
        : ISpriteContent<InterfaceMenuEditorOptionPart>
    {
        public IDictionary<InterfaceMenuEditorOptionPart, Rectangle> SpriteContent { get; } = new Dictionary<InterfaceMenuEditorOptionPart, Rectangle>
        {
            { InterfaceMenuEditorOptionPart.Save, new Rectangle(0, 0, 16, 16) },
            { InterfaceMenuEditorOptionPart.Load, new Rectangle(16, 0, 16, 16) },
            { InterfaceMenuEditorOptionPart.New, new Rectangle(32, 0, 16, 16)},
            { InterfaceMenuEditorOptionPart.Grid, new Rectangle(48, 0, 16, 16) },
            { InterfaceMenuEditorOptionPart.Close, new Rectangle(64, 0, 16, 16) },
            { InterfaceMenuEditorOptionPart.ConsoleWindow, new Rectangle(80, 0, 16, 16) },
        };

        public Texture2D Texture { get; } = game.Content.Load<Texture2D>("Interface/MenuEditorOptions");
        public Rectangle GetSprite(MapLayer mapLayer, InterfaceMenuEditorOptionPart numberPart)
        {
            return this.SpriteContent[numberPart];
        }
    }
}