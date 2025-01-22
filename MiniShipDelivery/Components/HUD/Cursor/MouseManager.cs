using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;

namespace MiniShipDelivery.Components.HUD.Cursor;

public class MouseManager : DrawableGameComponent
{
    
    private readonly SpriteBatch _spriteBatch;
    private readonly UserInterfacesMouse _userInterfaceMouse;
    private readonly InputManager _input;
    private readonly CameraManager _camera;

    public MouseManager(Game game) : base(game)
    {
        this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
        this._userInterfaceMouse = new UserInterfacesMouse(game);
        this._input = game.GetComponent<InputManager>();
        this._camera = game.GetComponent<CameraManager>();
    }

    public override void Draw(GameTime gameTime)
    {
        this._spriteBatch.BeginWithCameraViewMatrix();
        
        this._spriteBatch.Draw(
            this._userInterfaceMouse.Texture,
            this._input.Inputs.MousePosition + this._camera.Camera.Position,
            this._userInterfaceMouse.SpriteContent[MousePart.Cursor].Cutout,
            Color.AliceBlue);
        
        this._spriteBatch.End();
    }
}