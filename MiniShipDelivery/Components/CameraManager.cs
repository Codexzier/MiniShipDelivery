using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery.Components;

public class CameraManager : GameComponent
{
    public CameraManager(Game game) : base(game)
    {
        var viewportAdapter = new BoxingViewportAdapter(
            this.Game.Window, 
            this.Game.GraphicsDevice, 
            GlobaleGameParameters.ScreenWidth, 
            GlobaleGameParameters.ScreenHeight);
        
        this.Camera = new OrthographicCamera(viewportAdapter);
    }

    public OrthographicCamera Camera { get; private set; }
}