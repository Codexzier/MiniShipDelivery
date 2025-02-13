using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.World;

public static class WorldManagerHelper
{
    public static void DrawGrid(SpriteBatch spriteBatch, Vector2 position)
    {
        if (!GlobalGameParameters.ShowGrid) return;
            
        const int maxY = 5;
        const int maxX = 5;
            
        var posX = ((int)position.X / 16) * 16 + (maxX * 16) + (maxX * 16 / 2) - 8;
        var posY = ((int)position.Y / 16) * 16 + (maxY * 16 / 2) + 8;

        for (var iY = 0; iY < maxY; iY++)
        {
            for (var iX = 0; iX < maxX; iX++)
            {
                spriteBatch.DrawRectangle(
                    new Vector2(iX * 16 + posX, iY * 16 + posY),
                    new SizeF(16.5f, 16.5f),
                    Color.Gray,
                    .5f);
            }
        }
    }
}