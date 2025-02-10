using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Input;

namespace MiniShipDelivery.Components.HUD.Cursor;

public class MouseManager : DrawableGameComponent
{
    private ApplicationBus Bus => ApplicationBus.Instance;
    private readonly SpriteBatch _spriteBatch;
    private readonly UserInterfacesMouse _userInterfaceMouse;

    public MouseManager(Game game) : base(game)
    {
        this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
        this._userInterfaceMouse = new UserInterfacesMouse(game);
    }

    public override void Draw(GameTime gameTime)
    {
        this._spriteBatch.BeginWithCameraViewMatrix();
        
        this._spriteBatch.Draw(
            this._userInterfaceMouse.Texture,
            ApplicationBus.Instance.Inputs.MousePosition + this.Bus.Camera.GetPosition(),
            this._userInterfaceMouse.SpriteContent[MousePart.Cursor].Cutout,
            Color.AliceBlue);
        
        this._spriteBatch.End();
    }
}