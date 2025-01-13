using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.GameMenu;

public class GameMenuManager(Game game)
{
    private readonly GameMenuCommon _gameMenuCommon = new(game);
    
    public void Draw(SpriteBatch spriteBatch)
    {
        this._gameMenuCommon.Draw(spriteBatch);
    }
}