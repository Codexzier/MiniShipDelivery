using System;
using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components;

public interface IGameCamera
{
    public Func<Matrix> GetViewMatrix { get; }
    public Func<Vector2> GetPosition { get; }
    public Action<Vector2> AddPosition { get; }
}