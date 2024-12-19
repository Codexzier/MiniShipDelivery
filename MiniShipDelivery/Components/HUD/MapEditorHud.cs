using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.HUD
{
    internal class MapEditorHud
    {
        private AssetManager _spriteManager;
        private InputManager _input;
        private int _screenWidth;
        private int _screenHeight;
        private readonly InterfacePack _interfacePack;

        public MapEditorHud(AssetManager spriteManager, InputManager input, int screenWidth, int screenHeight, InterfacePack interfacePack)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this._interfacePack = interfacePack;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.TopMenu(spriteBatch);
            this.SideMenu(spriteBatch);

            // draw Tilemap on Sidemenu
        }

        private void TopMenu(SpriteBatch spriteBatch)
        {
            // draw the frame that fit on screen
            var countMiddleForWidth = this._screenWidth / 4;
            // start single frame
            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                0,
                0,
                InterfacePart.BaseFrame_TopLeft,
                InterfacePart.BaseFrame_TopMiddle,
                InterfacePart.BaseFrame_TopRight);

            for (var y = 0; y < 3; y++)
            {
                this.CreateScreenWidthFrame(spriteBatch,
                    countMiddleForWidth,
                    0,
                    4 + (y * 4),
                    InterfacePart.BaseFrame_MiddleLeft,
                    InterfacePart.BaseFrame_MiddleMiddle,
                    InterfacePart.BaseFrame_MiddleRight);
            }

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                0,
                16,
                InterfacePart.BaseFrame_DownLeft,
                InterfacePart.BaseFrame_DownMiddle,
                InterfacePart.BaseFrame_DownRight);
        }

        private void SideMenu(SpriteBatch spriteBatch)
        {
            int shiftTop = 20;
            this.CreateScreenWidthFrame(spriteBatch,
                10,
                this._screenWidth - 40,
                shiftTop,
                InterfacePart.BaseFrame_TopLeft,
                InterfacePart.BaseFrame_TopMiddle,
                InterfacePart.BaseFrame_TopRight);

            shiftTop += 4;
            int iteration = ((this._screenHeight - shiftTop) / 4) - 1;
            for (var y = 0; y < iteration; y++)
            {
                this.CreateScreenWidthFrame(spriteBatch,
                    10,
                    this._screenWidth - 40,
                    shiftTop + (y * 4),
                    InterfacePart.BaseFrame_MiddleLeft,
                    InterfacePart.BaseFrame_MiddleMiddle,
                    InterfacePart.BaseFrame_MiddleRight);
            }
            shiftTop += iteration * 4;

            this.CreateScreenWidthFrame(spriteBatch,
                10,
                this._screenWidth - 40,
                shiftTop,
                InterfacePart.BaseFrame_DownLeft,
                InterfacePart.BaseFrame_DownMiddle,
                InterfacePart.BaseFrame_DownRight);
        }

        private void CreateScreenWidthFrame(SpriteBatch spriteBatch, int countMiddleForWidth, int shiftLeft, int shiftTop, InterfacePart left, InterfacePart middle, InterfacePart right)
        {
            this._spriteManager.Draw(spriteBatch, new Vector2(shiftLeft, shiftTop), left, this._interfacePack);
            for (var x = 1; x < countMiddleForWidth - 1; x++)
            {
                this._spriteManager.Draw(spriteBatch, new Vector2(shiftLeft + (x * 4), shiftTop), middle, this._interfacePack);
            }
            this._spriteManager.Draw(spriteBatch, new Vector2(shiftLeft + ((countMiddleForWidth - 1) * 4), shiftTop), right, this._interfacePack);
        }
    }
}