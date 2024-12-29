using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.HUD.Helpers;

public static class SimpleThinksHelper
{
    public static Color BoolToColor(bool value) => value ? Color.DarkGray : Color.White;
}