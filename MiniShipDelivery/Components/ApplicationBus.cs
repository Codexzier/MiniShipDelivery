using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components;

public class ApplicationBus
{
    private static ApplicationBus _instance;
    
    private ApplicationBus()
    {
    }
    
    public static ApplicationBus Instance => _instance ??= new ApplicationBus();
    public IInputData Inputs { get; } = new InputData();

    public IGameCamera Camera { get; set; } = new CameraData();
}

public interface IInputData
{
    public Vector2 MovementCharacter { get; }
    public Vector2 MousePosition { get; }
    
    public Func<Vector2, SizeF, bool> GetMouseButtonReleasedStateLeft { get; }
    public Func<Vector2, SizeF, bool> GetMouseButtonReleasedStateRight { get; }
}

public interface IGameCamera
{
    public Func<Matrix> GetViewMatrix { get; }
    public Func<Vector2> GetPosition { get; }
    public Action<Vector2> AddPosition { get; }
}

public class CameraData : IGameCamera
{
    public Func<Matrix> GetViewMatrix { get; set; }
    public Func<Vector2> GetPosition { get; set; }
    public Action<Vector2> AddPosition { get; set; }
}