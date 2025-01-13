using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.GameMenuMap;

public class GameMenuMapManager
{
    private readonly GameMenuMapOptions _gameMenuMapOptions;
    
    public static bool ShowGameMap { get; set; }
    
    public GameMenuMapManager(Game game)
    {
        this._gameMenuMapOptions = new GameMenuMapOptions(game);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!ShowGameMap) return;
        
        this._gameMenuMapOptions.Draw(spriteBatch);
    }
}