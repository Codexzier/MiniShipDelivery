using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets.Packs
{
    internal class InterfacePack4x4 : ISpriteProperties<InterfacePart4x4>
    {
        public IDictionary<InterfacePart4x4, Rectangle> SpriteContent { get; private set; }

        public InterfacePack4x4()
        {
            this.SpriteContent = new Dictionary<InterfacePart4x4, Rectangle>
            {
                // BaseFrame4x4 Type1
                { InterfacePart4x4.BaseFrame_Type1_TopLeft, new Rectangle((10 * 16) + 0, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_TopMiddle, new Rectangle((10 * 16) + 4, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_TopRight, new Rectangle((10 * 16) + 12, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_MiddleLeft, new Rectangle((10 * 16) + 0, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_MiddleMiddle, new Rectangle((10 * 16) + 4, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_MiddleRight, new Rectangle((10 * 16) + 12, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_DownLeft, new Rectangle((10 * 16) + 0, 12, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_DownMiddle, new Rectangle((10 * 16) + 4, 12, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type1_DownRight, new Rectangle((10 * 16) + 12, 12, 4, 4) },
                // BaseFrame4x4 Type2
                { InterfacePart4x4.BaseFrame_Type2_TopLeft, new Rectangle((11 * 16) + 0, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_TopMiddle, new Rectangle((11 * 16) + 4, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_TopRight, new Rectangle((11 * 16) + 12, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_MiddleLeft, new Rectangle((11 * 16) + 0, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_MiddleMiddle, new Rectangle((11 * 16) + 4, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_MiddleRight, new Rectangle((11 * 16) + 12, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_DownLeft, new Rectangle((11 * 16) + 0, 12, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_DownMiddle, new Rectangle((11 * 16) + 4, 12, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type2_DownRight, new Rectangle((11 * 16) + 12, 12, 4, 4) },
                // BaseFrame4x4 Type3
                { InterfacePart4x4.BaseFrame_Type3_TopLeft, new Rectangle((12 * 16) + 0, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_TopMiddle, new Rectangle((12 * 16) + 4, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_TopRight, new Rectangle((12 * 16) + 12, 0, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_MiddleLeft, new Rectangle((12 * 16) + 0, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_MiddleMiddle, new Rectangle((12 * 16) + 4, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_MiddleRight, new Rectangle((12 * 16) + 12, 4, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_DownLeft, new Rectangle((12 * 16) + 0, 12, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_DownMiddle, new Rectangle((12 * 16) + 4, 12, 4, 4) },
                { InterfacePart4x4.BaseFrame_Type3_DownRight, new Rectangle((12 * 16) + 12, 12, 4, 4) }
            };
        }
    }
}