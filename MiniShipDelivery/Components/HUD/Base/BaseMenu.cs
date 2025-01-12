using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public abstract class BaseMenu(
    Game game,
    Vector2 position,
    Size size)
{
    private readonly MenuFrame _menuFrame = new(game);
    
    public Vector2 Position => position;
        
    protected readonly CameraManager Camera = game.GetComponent<CameraManager>();

    protected void DrawBaseFrame(SpriteBatch spriteBatch, MenuFrameType type)
    {
        this._menuFrame.DrawMenuFrame(spriteBatch,
            this.Camera.Camera.Position + position,
            size,
            type);
    }
        
    // protected Vector2 GetPositionArea(int multiply, int width, int columns)
    // {
    //     var pasInX = multiply / columns;
    //     var multiplyX = multiply < columns ? multiply : multiply - (pasInX * columns);
    //     var x = GlobaleGameParameters.ScreenWidth - width + 3 + ((multiplyX * 16) + (multiplyX * 2));
    //     var y = position.Y + 3 + ((pasInX * 16) + (pasInX * 2));
    //
    //     return new Vector2(x, y);
    // }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }
}