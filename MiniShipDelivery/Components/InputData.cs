using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components;

public class InputData : IInputData
{
    public Vector2 MovementCharacter { get; set; } = Vector2.Zero;
    public Vector2 MousePosition { get; set; } = Vector2.Zero;
    public Func<Vector2, SizeF, bool> GetMouseButtonReleasedStateLeft { get; set; }
    public Func<Vector2, SizeF, bool> GetMouseButtonReleasedStateRight { get; set; }
}