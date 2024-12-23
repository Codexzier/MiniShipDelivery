using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.HUD;

public static class HudHelper
{
    public static string Vector2ToString(Vector2 vector)
    {
        return $"{vector.X:F1}, {vector.Y:F1}";
    }

}