using System;
using Microsoft.Xna.Framework.Input;

namespace MiniShipDelivery.Components.Input;

public class KeyboardController
{
    public void Update(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Enter))
        {
            
        }
        
        
        if (keyboardState.IsKeyUp(Keys.Enter))
        {
            
        }
    }
}