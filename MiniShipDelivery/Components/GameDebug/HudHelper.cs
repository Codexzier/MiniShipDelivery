using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.GameDebug;

public static class HudHelper
{
    public static string Vector2ToString(Vector2 vector)
    {
        return $"{vector.X:F1}, {vector.Y:F1}";
    }
    
    public static bool IsMouseInRange(Vector2 position, SizeF buttonSize)
    {
        return IsMouseInRange(new RectangleF(position.X, position.Y, buttonSize.Width, buttonSize.Height));
    }
    
    public static bool IsMouseInRange(RectangleF area)
    {
        var bus = ApplicationBus.Instance;
        
        return bus.Inputs.MousePosition.X > area.X &&
               bus.Inputs.MousePosition.Y > area.Y &&
               bus.Inputs.MousePosition.X < area.X + area.Width &&
               bus.Inputs.MousePosition.Y < area.Y + area.Height;
    }
    
    public static Vector2 GetPositionArea(float positionY, int multiply, int width, int columns)
    {
        var pasInX = multiply / columns;
        var multiplyX = multiply < columns ? multiply : multiply - (pasInX * columns);
        var x = GlobalGameParameters.ScreenWidth - width + 3 + ((multiplyX * 16) + (multiplyX * 2));
        var y = positionY + 3 + ((pasInX * 16) + (pasInX * 2));

        return new Vector2(x, y);
    }
}