using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.Dialog;

public class DialogManager : DrawableGameComponent
{
    private readonly SpriteBatch _spriteBatch;
    
    private readonly DialogMenu _dialogMenu;
    
    public DialogManager(Game game) : base(game)
    {
        this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
        
        this._dialogMenu = new DialogMenu(game);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if(GlobaleGameParameters.HudView != HudOptionView.Game) return;
        if(!GlobaleGameParameters.ShowDialogBox) return;
        
        this._dialogMenu.Update();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        if(GlobaleGameParameters.HudView != HudOptionView.Game) return;
        if(!GlobaleGameParameters.ShowDialogBox) return;

        this._spriteBatch.BeginWithCameraViewMatrix();
        this._dialogMenu.Draw(this._spriteBatch);
        this._spriteBatch.End();
    }
}