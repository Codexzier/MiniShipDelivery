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
    public Size Size => size;
        
    protected readonly CameraManager Camera = game.GetComponent<CameraManager>();

    protected void DrawBaseFrame(SpriteBatch spriteBatch, MenuFrameType type)
    {
        this._menuFrame.DrawMenuFrame(spriteBatch,
            this.Camera.Camera.Position + position,
            size,
            type);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }
}