using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    internal class InterfacePack : ISpriteProperties<InterfacePart>
    {
        public IDictionary<InterfacePart, Rectangle> SpriteContent { get; private set; }

        public InterfacePack()
        {
            this.SpriteContent = new Dictionary<InterfacePart, Rectangle>
            {
                { InterfacePart.BaseFrame_TopLeft, new Rectangle((10 * 16) + 0, 0, 4, 4) },
                { InterfacePart.BaseFrame_TopMiddle, new Rectangle((10 * 16) + 4, 0, 4, 4) },
                { InterfacePart.BaseFrame_TopRight, new Rectangle((10 * 16) + 12, 0, 4, 4) },
                { InterfacePart.BaseFrame_MiddleLeft, new Rectangle((10 * 16) + 0, 4, 4, 4) },
                { InterfacePart.BaseFrame_MiddleMiddle, new Rectangle((10 * 16) + 4, 4, 4, 4) },
                { InterfacePart.BaseFrame_MiddleRight, new Rectangle((10 * 16) + 12, 4, 4, 4) },
                { InterfacePart.BaseFrame_DownLeft, new Rectangle((10 * 16) + 0, 12, 4, 4) },
                { InterfacePart.BaseFrame_DownMiddle, new Rectangle((10 * 16) + 4, 12, 4, 4) },
                { InterfacePart.BaseFrame_DownRight, new Rectangle((10 * 16) + 12, 12, 4, 4) }
            };
        }
    }
}