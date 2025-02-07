using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.Input;

public class InputData
{
    public Vector2 MovementCharacter { get; set; } = Vector2.Zero;
    public Vector2 MousePosition { get; set; } = Vector2.Zero;
    public bool MouseRightButton { get; set; }
}