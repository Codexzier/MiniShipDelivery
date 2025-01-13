using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.OptionsMenu;

public class OptionsMenuManager(Game game)
{
    private readonly OptionsMenuOptions _optionsMenuOptions = new(game);
    
    
    public void Update(GameTime gameTime)
    {
        this._optionsMenuOptions.Update(gameTime);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        this._optionsMenuOptions.Draw(spriteBatch);
    }
}