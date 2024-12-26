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
                { InterfaceMenuEditorOptionPart.None, new Rectangle(0, 0, 0, 0) },
            };
        }

        public IDictionary<InterfaceMenuEditorOptionPart, Rectangle> SpriteContent { get; }

        public Texture2D Texture { get; }
    }
}