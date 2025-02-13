using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.OptionsMenu;

public class OptionsMenuManager(Game game)
{
    private readonly OptionsMenuOptions _optionsMenuOptions = new(game);
    
    
    public void Update()
    {
        
        if(GlobalGameParameters.HudView != HudOptionView.Options) return;
        this._optionsMenuOptions.Update();
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        this._optionsMenuOptions.Draw(spriteBatch);
    }
}