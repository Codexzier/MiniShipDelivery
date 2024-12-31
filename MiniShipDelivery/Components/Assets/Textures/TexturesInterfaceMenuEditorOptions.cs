using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.Assets.Textures
{
    public class TexturesInterfaceMenuEditorOptions(Texture2D texture2D)
        : ISpriteProperties<InterfaceMenuEditorOptionPart>, IAssetTexture
    {
        public IDictionary<InterfaceMenuEditorOptionPart, Rectangle> SpriteContent { get; } = new Dictionary<InterfaceMenuEditorOptionPart, Rectangle>
        {
            { InterfaceMenuEditorOptionPart.Save, new Rectangle(0, 0, 16, 16) },
            { InterfaceMenuEditorOptionPart.Load, new Rectangle(16, 0, 16, 16) },
            { InterfaceMenuEditorOptionPart.New, new Rectangle(32, 0, 16, 16)},
            { InterfaceMenuEditorOptionPart.Grid, new Rectangle(48, 0, 16, 16) },
        };

        public Texture2D Texture { get; } = texture2D;
    }
}