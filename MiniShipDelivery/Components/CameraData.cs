using System;
using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components;

public class CameraData : IGameCamera
{
    public Func<Matrix> GetViewMatrix { get; set; }
    public Func<Vector2> GetPosition { get; set; }
    public Action<Vector2> AddPosition { get; set; }
}