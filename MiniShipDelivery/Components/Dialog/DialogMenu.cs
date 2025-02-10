using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Dialog;

internal class DialogMenu(Game game) : BaseMenu(game,
    new Vector2(
        0,
        GlobaleGameParameters.ScreenHeight - 36),
    new SizeF(
        GlobaleGameParameters.ScreenWidth,
        36))
{
    private readonly SpriteFont _font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");

    public override void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);
        
        var pos = this.Bus.Camera.GetPosition() + this.Position + new Vector2(5, 5);
        
        // black dialog box in frame
        spriteBatch.FillRectangle(
            pos,
            new SizeF(this.Size.Width - 10, this.Size.Height - 10),
            Color.Black,
            0.5f);

        if (!GlobaleGameParameters.ShowDialogBox)
        {
            spriteBatch.DrawString(
                this._font,
                "Want you start the Dialog? Press ENTER",
                pos + new Vector2(2, 0),
                Color.White);    
        }
        else
        {
            this.DrawNpcText(spriteBatch, pos);
            this.DrawPlayerText(spriteBatch, pos);
        }
    }
    
    private void DrawNpcText(SpriteBatch spriteBatch, Vector2 pos)
    {
        spriteBatch.DrawString(
            this._font,
            GlobaleGameParameters.DialogTextNpc,
            pos + new Vector2(2, 0),
            Color.White);
    }
    
    private void DrawPlayerText(SpriteBatch spriteBatch, Vector2 pos)
    {
        spriteBatch.DrawString(
            this._font,
            GlobaleGameParameters.DialogTextUser,
            pos + new Vector2(2, 12),
            Color.White);
            
        spriteBatch.DrawString(
            this._font,
            $"{GlobaleGameParameters.DialogTextUser.Length}",
            pos + new Vector2(GlobaleGameParameters.ScreenWidth - 25, 12),
            Color.White);
    }
}