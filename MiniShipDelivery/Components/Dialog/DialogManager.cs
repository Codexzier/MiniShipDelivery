using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Dialog;

public class DialogManager : DrawableGameComponent
{
    private readonly SpriteBatch _spriteBatch;
    
    private readonly DialogMenu _dialogMenu;
    
    public static bool ShowDialogBox = false; 
    
    public DialogManager(Game game) : base(game)
    {
        this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
        
        this._dialogMenu = new DialogMenu(game);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if(GlobaleGameParameters.HudView != HudOptionView.Game) return;
        if(!ShowDialogBox) return;
        
        this._dialogMenu.Update();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        if(GlobaleGameParameters.HudView != HudOptionView.Game) return;
        if(!ShowDialogBox) return;

        this._spriteBatch.BeginWithCameraViewMatrix();
        this._dialogMenu.Draw(this._spriteBatch);
        this._spriteBatch.End();
    }
}

internal class DialogMenu : BaseMenu
{
    
    private readonly SpriteFont _font;
    public DialogMenu(Game game)
        :base(
            game, 
            new Vector2(
                0,
                GlobaleGameParameters.ScreenHeight - 36),
            new Size(
                GlobaleGameParameters.ScreenWidth, 
                36))
    {
        
        this._font = game.Content.Load<SpriteFont>("Fonts/BaseFont");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);
        
        // black dialog box in frame
        
        spriteBatch.DrawString(
            this._font,
            "Dies ist ein Test",
            this.Position,
            Color.White);
    }
}