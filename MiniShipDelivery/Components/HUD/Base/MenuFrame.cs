using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base
{
    public class MenuFrame
    {
        private readonly AssetManager _assetManager;
        private readonly Dictionary<MenuFrameType, int> _menuShift = new()
        {
            { MenuFrameType.Type1, 0 },
            { MenuFrameType.Type2, 9 },
            { MenuFrameType.Type3, 18 }
        };

        public MenuFrame(AssetManager assetManager)
        {
            this._assetManager = assetManager;
        }

        public InterfacePart4x4 GetMenuFrameByType(InterfacePart4x4 part, MenuFrameType mft)
        {
            return (InterfacePart4x4)((int)part + this._menuShift[mft]);
        }

        public void DrawMenuFrame(SpriteBatch spriteBatch,
            Vector2 position,
            Size size,
            MenuFrameType mft)
        {
            var shift = this._menuShift[mft];

            var countMiddleForWidth = ((size.Width - (2 * 4)) / 4);
            var countMiddleForHeight = (size.Height / 4) - 1;

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                position,
                InterfacePart4x4.BaseFrame_Type1_TopLeft,
                InterfacePart4x4.BaseFrame_Type1_TopMiddle,
                InterfacePart4x4.BaseFrame_Type1_TopRight,
                mft);

            for (var y = 0; y < countMiddleForHeight - 1; y++)
            {
                this.CreateScreenWidthFrame(spriteBatch,
                    countMiddleForWidth,
                    position + new Vector2(0, 4 + (y * 4)),
                    InterfacePart4x4.BaseFrame_Type1_MiddleLeft,
                    InterfacePart4x4.BaseFrame_Type1_MiddleMiddle,
                    InterfacePart4x4.BaseFrame_Type1_MiddleRight,
                    mft);
            }

            var countMiddleForHeightEnd = countMiddleForHeight * 4;

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                position + new Vector2(0, countMiddleForHeightEnd),
                InterfacePart4x4.BaseFrame_Type1_DownLeft,
                InterfacePart4x4.BaseFrame_Type1_DownMiddle,
                InterfacePart4x4.BaseFrame_Type1_DownRight,
                mft);
        }

        private void CreateScreenWidthFrame(SpriteBatch spriteBatch, 
            int countMiddleForWidth, 
            Vector2 shiftPosition, 
            InterfacePart4x4 left, InterfacePart4x4 middle, InterfacePart4x4 right, 
            MenuFrameType mft)
        {
            left = this.GetMenuFrameByType(left, mft);
            middle = this.GetMenuFrameByType(middle, mft);
            right = this.GetMenuFrameByType(right, mft);

            this._assetManager.Draw(spriteBatch, 
                shiftPosition, 
                left);
            
            for (var x = 0; x < countMiddleForWidth; x++)
            {
                this._assetManager.Draw(spriteBatch, 
                    shiftPosition + new Vector2(4 + (x * 4), 0), 
                    middle);
            }
            this._assetManager.Draw(spriteBatch, 
                shiftPosition + new Vector2(4 + (countMiddleForWidth * 4), 0), 
                right);
        }
    }
}