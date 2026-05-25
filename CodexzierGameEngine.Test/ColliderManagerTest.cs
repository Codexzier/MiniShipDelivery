using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Objects;

namespace CodexzierGameEngine.Test;

[TestClass]
public sealed class ColliderManagerTest
{
    [TestMethod]
    public void UpdateClearColliders()
    {
        // arrange
        var game = new Game();
        var instance = new ColliderManager(game);

        // act
        instance.Update(null);

        // assert
        
    }
}