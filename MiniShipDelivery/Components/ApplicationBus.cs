using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components;

public class ApplicationBus
{
    private static ApplicationBus _instance;
    
    private ApplicationBus()
    {
    }
    
    public static ApplicationBus Instance => _instance ??= new ApplicationBus();
    public IInputData Inputs { get; } = new InputData();
    public TextMessage TextMessage { get; } = new ();
    public IGameCamera Camera { get; set; } = new CameraData();
    
    public int MapChunkIndex { get; set; }
    public Vector2 MapChunkPosition { get; set; }
    public Vector2 CharacterPlayerPosition { get; set; }
}