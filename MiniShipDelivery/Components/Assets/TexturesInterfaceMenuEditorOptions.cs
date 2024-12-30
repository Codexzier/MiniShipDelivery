using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    public class TexturesInterfaceMenuEditorOptions : ISpriteProperties<InterfaceMenuEditorOptionPart>
    {
        public TexturesInterfaceMenuEditorOptions(Texture2D texture2D)
        {
            this.Texture = texture2D;

            this.SpriteContent = new Dictionary<InterfaceMenuEditorOptionPart, Rectangle>
            {
                { InterfaceMenuEditorOptionPart.Save, new Rectangle(0, 0, 16, 16) },
                { InterfaceMenuEditorOptionPart.Load, new Rectangle(16, 0, 16, 16) },
                { InterfaceMenuEditorOptionPart.New, new Rectangle(32, 0, 16, 16)},
                { InterfaceMenuEditorOptionPart.Grid, new Rectangle(48, 0, 16, 16) },
            };
        }

        public IDictionary<InterfaceMenuEditorOptionPart, Rectangle> SpriteContent { get; }

        public Texture2D Texture { get; }
    }
}