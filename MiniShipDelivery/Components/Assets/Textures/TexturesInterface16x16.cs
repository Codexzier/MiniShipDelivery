using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets.Textures
{
    internal class TexturesInterface16x16 : ISpriteProperties<InterfacePart16x16>
    {
        public IDictionary<InterfacePart16x16, Rectangle> SpriteContent { get; private set; }

        public TexturesInterface16x16()
        {
            this.SpriteContent = new Dictionary<InterfacePart16x16, Rectangle>
            {
                // Signs
                { InterfacePart16x16.Signs_ExclamationWihte, new Rectangle(8 * 16, 2 * 16, 16, 16) },
                { InterfacePart16x16.Signs_ExclamationYellow, new Rectangle(8 * 16, 3 * 16, 16, 16) },
                { InterfacePart16x16.Signs_ExclamationRed, new Rectangle(8 * 16, 4 * 16, 16, 16) },
                // Arrows
                { InterfacePart16x16.Arrow_Type1, new Rectangle(8 * 16, 0 * 16, 16, 16) },
                { InterfacePart16x16.Arrow_Type2, new Rectangle(9 * 16, 0 * 16, 16, 16) },
                { InterfacePart16x16.Arrow_Type3, new Rectangle(8 * 16, 1 * 16, 16, 16) },
                { InterfacePart16x16.Arrow_Type4, new Rectangle(9 * 16, 1 * 16, 16, 16) },
                
            };
        }
    }
}