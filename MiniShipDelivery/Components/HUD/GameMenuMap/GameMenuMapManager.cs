using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.GameMenuMap;

public class GameMenuMapManager(Game game)
{
    private readonly GameMenuMapOptions _gameMenuMapOptions = new(game);
    
    public static bool Show { get; set; }

    public void Update()
    {
        if(!Show) return;
        
        this._gameMenuMapOptions.Update();
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if(!Show) return;
        
        this._gameMenuMapOptions.Draw(spriteBatch);
    }
}