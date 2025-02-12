using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components;

public interface IInputData
{
    public Vector2 MovementCharacter { get; }
    public Vector2 MousePosition { get; }
    
    public Func<Vector2, SizeF, string, bool> GetMouseButtonReleasedStateLeft { get; }
    public Func<Vector2, SizeF, string, bool> GetMouseButtonReleasedStateRight { get; }
}