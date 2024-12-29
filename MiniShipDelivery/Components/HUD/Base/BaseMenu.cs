using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

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
    
    protected Vector2 GetPositionArea(int multiply, int width, int columns)
    {
        var pasInX = multiply / columns;
        var multiplyX = multiply < columns ? multiply : multiply - (pasInX * columns);
        var x = this._screenWidth - width + 3 + ((multiplyX * 16) + (multiplyX * 2));
        var y = this._position.Y + 3 + ((pasInX * 16) + (pasInX * 2));

        return new Vector2(x, y);
    }
}