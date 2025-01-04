using System.Linq;
using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components;

public static class ComponentHelper
{
    public static T GetComponent<T>(this Game game) where T : GameComponent
    {
        return game.Components.First(f => f.GetType() == typeof(T)) as T;
    }
}