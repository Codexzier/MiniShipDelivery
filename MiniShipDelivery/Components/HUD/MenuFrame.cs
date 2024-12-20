using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;

namespace MiniShipDelivery.Components.HUD
{
    internal class MenuFrame
    {
        private AssetManager _spriteManager;

        public MenuFrame(AssetManager spriteManager)
        {
            this._spriteManager = spriteManager;
        }

        public void DrawMenuFrame(SpriteBatch spriteBatch,
            int positionX,
            int positionY,
            int width,
            int height)
        {
            var countMiddleForWidth = width / 4;
            var countMiddleForHeight = (height / 4) - 1;

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                positionX,
                positionY,
                InterfacePart.BaseFrame_TopLeft,
                InterfacePart.BaseFrame_TopMiddle,
                InterfacePart.BaseFrame_TopRight);

            for (var y = 0; y < countMiddleForHeight - 1; y++)
            {
                this.CreateScreenWidthFrame(spriteBatch,
                    countMiddleForWidth,
                    positionX,
                    positionY + 4 + (y * 4),
                    InterfacePart.BaseFrame_MiddleLeft,
                    InterfacePart.BaseFrame_MiddleMiddle,
                    InterfacePart.BaseFrame_MiddleRight);
            }

            var countMiddleForHeightEnd = countMiddleForHeight * 4;

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                positionX,
                positionY + countMiddleForHeightEnd,
                InterfacePart.BaseFrame_DownLeft,
                InterfacePart.BaseFrame_DownMiddle,
                InterfacePart.BaseFrame_DownRight);
        }

        private void CreateScreenWidthFrame(SpriteBatch spriteBatch, int countMiddleForWidth, int shiftLeft, int shiftTop, InterfacePart left, InterfacePart middle, InterfacePart right)
        {
            this._spriteManager.Draw(spriteBatch, new Vector2(shiftLeft, shiftTop), left, this._spriteManager.InterfacePack);
            for (var x = 1; x < countMiddleForWidth - 1; x++)
            {
                this._spriteManager.Draw(spriteBatch, new Vector2(shiftLeft + (x * 4), shiftTop), middle, this._spriteManager.InterfacePack);
            }
            this._spriteManager.Draw(spriteBatch, new Vector2(shiftLeft + ((countMiddleForWidth - 1) * 4), shiftTop), right, this._spriteManager.InterfacePack);
        }
    }
}