using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery.Components;

public class CameraManager : GameComponent
{
    public CameraManager(Game game, int screenWidth, int screenHeight) : base(game)
    {
        var viewportAdapter = new BoxingViewportAdapter(
            this.Game.Window, 
            this.Game.GraphicsDevice, 
            screenWidth, 
            screenHeight);
        
        this.Camera = new OrthographicCamera(viewportAdapter);
    }

    public OrthographicCamera Camera { get; private set; }
}