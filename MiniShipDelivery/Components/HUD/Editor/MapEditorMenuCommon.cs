using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor
{
    public class MapEditorMenuCommon : BaseMenu
    {
        public MapEditorMenuCommon(AssetManager assetManager,
            InputManager input,
            OrthographicCamera camera,
            int screenWidth,
            int screenHeight) 
            : base(assetManager, input, camera, 
                screenWidth, screenHeight, 
                new Vector2(0, 0), new Size(screenWidth, 20))
        {
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawBaseFrame(spriteBatch, MenuFrameType.Type3);
        }
    }

    public abstract class BaseMenu
    {
        private readonly MenuFrame _menuFrame;
        
        protected readonly AssetManager _assetManager;
        protected readonly InputManager _input;
        protected readonly OrthographicCamera _camera;
        
        protected readonly Vector2 _position;
        protected readonly Size _size;
        protected readonly int _screenWidth;
        protected readonly int _screenHeight;
        
        protected BaseMenu(
            AssetManager assetManager,
            InputManager input,
            OrthographicCamera camera, 
            int screenWidth, 
            int screenHeight,
            Vector2 position,
            Size size)
        {
            this._menuFrame = new MenuFrame(assetManager);

            this._assetManager = assetManager;
            this._input = input;
            this._camera = camera;
            
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this._position = position;
            this._size = size;
        }

        protected void DrawBaseFrame(SpriteBatch spriteBatch, MenuFrameType type)
        {
            this._menuFrame.DrawMenuFrame(spriteBatch,
                this._camera.Position + this._position,
                this._size,
                type);
        }
        
        protected bool IsMouseInRange(Vector2 position, SizeF size)
        {
            return this._input.MousePosition.X > position.X &&
                   this._input.MousePosition.Y > position.Y &&
                   this._input.MousePosition.X < position.X + size.Width &&
                   this._input.MousePosition.Y < position.Y + size.Height;
        }
        
        protected Color BoolToColor(bool value) => value ? Color.DarkGray : Color.White;
    }
}
