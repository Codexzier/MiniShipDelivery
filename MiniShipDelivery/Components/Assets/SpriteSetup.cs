using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.Assets;

public class SpriteSetup
{
    public Rectangle Cutout { get; set; }
    public bool IsTopLayer { get; set; }
    public bool IsBarrier { get; set; }
    public static SpriteSetup Empty { get; } = new() { Cutout = Rectangle.Empty };
}