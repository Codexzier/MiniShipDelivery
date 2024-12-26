using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShipDelivery.Components.HUD.Editors
{
    internal class MenuEditorOptions
    {
        private MenuFrame _menuFrame;
        private Size _menuTopSize;
        private AssetManager spriteManager;
        private readonly OrthographicCamera _camera;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public MenuEditorOptions(AssetManager spriteManager, OrthographicCamera camera, int screenWidth, int screenHeight)
        {
            this.spriteManager = spriteManager;
            this._camera = camera;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this._menuFrame = new MenuFrame(spriteManager);
            this._menuTopSize = new Size(this._screenWidth, 20);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this._menuFrame.DrawMenuFrame(spriteBatch,
                this._camera.Position,
                this._menuTopSize,
                MenuFrameType.Type3);
        }
    }
}
