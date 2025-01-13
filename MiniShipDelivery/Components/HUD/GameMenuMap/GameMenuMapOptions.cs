using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.GameMenuMap;

public class GameMenuMapOptions : BaseMenu
{
    private readonly CameraManager _camera;

    public GameMenuMapOptions(Game game)
        : base(
            game,
            new Vector2(GlobaleGameParameters.ScreenWidthHalf - 100, GlobaleGameParameters.ScreenHeightHalf - 60),
            new Size(200, 120))
    {
        this._camera = game.GetComponent<CameraManager>();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        spriteBatch.DrawRectangle(
            new RectangleF(
                this.Position.X + this._camera.Camera.Position.X, 
                this.Position.Y + this._camera.Camera.Position.Y, 
                this.Size.Width, 
                this.Size.Height), 
            Color.Chocolate);
    }
}