using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor
{
    internal class MenuEditorOptions
    {
        private MenuFrame _menuFrame;
        private Size _menuSize;
        private AssetManager spriteManager;
        private readonly OrthographicCamera _camera;
        private readonly int _screenWidth;
        private readonly int _screenHeight;
        private readonly Vector2 _position;

        public MenuEditorOptions(
            AssetManager spriteManager, 
            OrthographicCamera camera, 
            int screenWidth, 
            int screenHeight, 
            Vector2 position, 
            Size size)
        {
            this._menuFrame = new MenuFrame(spriteManager);

            this.spriteManager = spriteManager;
            this._camera = camera;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this._position = position;
            this._menuSize = size;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this._menuFrame.DrawMenuFrame(spriteBatch,
                this._camera.Position + this._position,
                this._menuSize,
                MenuFrameType.Type3);
        }
    }
}
