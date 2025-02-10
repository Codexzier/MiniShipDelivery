using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery.Components;

public class CameraManager : GameComponent
{
    private OrthographicCamera _camera;
    public CameraManager(Game game) : base(game)
    {
        var viewportAdapter = new BoxingViewportAdapter(
            this.Game.Window, 
            this.Game.GraphicsDevice, 
            GlobaleGameParameters.ScreenWidth, 
            GlobaleGameParameters.ScreenHeight);
        
        this._camera = new OrthographicCamera(viewportAdapter);

        ((CameraData)ApplicationBus.Instance.Camera).GetViewMatrix = () => this._camera.GetViewMatrix();
        ((CameraData)ApplicationBus.Instance.Camera).GetPosition = () => this._camera.Position;
        ((CameraData)ApplicationBus.Instance.Camera).AddPosition = position => this._camera.Position += position;
    }
}