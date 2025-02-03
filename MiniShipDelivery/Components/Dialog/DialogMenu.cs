using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Dialog;

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
        
        this._font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
        
    }

    public override void Update()
    {
        base.Update();
        
        
        if (!GlobaleGameParameters.DialogState.DialogOn) return;
        
        if(!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter) &&
           GlobaleGameParameters.DialogState.KeyIsPressed != Keys.Enter)
        {
            if(GlobaleGameParameters.DialogState.KeyIsPressed == Keys.Back)
            {
                if(GlobaleGameParameters.DialogState.TextPlayer.Length > 0)
                {
                    GlobaleGameParameters.DialogState.TextPlayer = 
                        GlobaleGameParameters.DialogState.TextPlayer.Remove(
                            GlobaleGameParameters.DialogState.TextPlayer.Length - 1);
                    GlobaleGameParameters.DialogState.DialogLetter = "";
                }
            }
            else
            {
                GlobaleGameParameters.DialogState.TextPlayer += GlobaleGameParameters.DialogState.DialogLetter;
                GlobaleGameParameters.DialogState.DialogLetter = "";
            }
        }
        
        if(!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter) &&
           GlobaleGameParameters.DialogState.DialogLetter == "ENTER" &&
           GlobaleGameParameters.DialogState.KeyIsPressed == Keys.Enter)
        {
            if(GlobaleGameParameters.DialogState.TextPlayer == "EXIT")
            {
                GlobaleGameParameters.DialogState.DialogExit = true;
            }

            switch (GlobaleGameParameters.DialogState.TextPlayer)
            {
                case "HELLO":
                    GlobaleGameParameters.DialogState.TextNpc = "Hello, how are you?";
                    break;
                case "GOOD":
                    GlobaleGameParameters.DialogState.TextNpc = "I'm fine, thank you!";
                    break;
                case "BYE":
                    GlobaleGameParameters.DialogState.TextNpc = "Goodbye!";
                    GlobaleGameParameters.DialogState.DialogExit = true;
                    break;
            }
            
            GlobaleGameParameters.DialogState.TextPlayer = string.Empty;
            GlobaleGameParameters.DialogState.DialogLetter = ""; 
        }

        if (!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter) &&
            GlobaleGameParameters.DialogState.KeyIsPressed == Keys.Back &&
            GlobaleGameParameters.DialogState.DialogLetter == "BACK")
        {
            GlobaleGameParameters.DialogState.DialogLetter = "";
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);
        
        var pos = this.Camera.Camera.Position + this.Position + new Vector2(5, 5);
        
        // black dialog box in frame
        spriteBatch.FillRectangle(
            pos,
            new SizeF(this.Size.Width - 10, this.Size.Height - 10),
            Color.Black,
            0.5f);

        if (!GlobaleGameParameters.DialogState.DialogOn)
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
            GlobaleGameParameters.DialogState.TextNpc,
            pos + new Vector2(2, 0),
            Color.White);
    }
    
    private void DrawPlayerText(SpriteBatch spriteBatch, Vector2 pos)
    {
        spriteBatch.DrawString(
            this._font,
            GlobaleGameParameters.DialogState.TextPlayer,
            pos + new Vector2(2, 12),
            Color.White);
            
        spriteBatch.DrawString(
            this._font,
            $"{GlobaleGameParameters.DialogState.TextPlayer.Length}",
            pos + new Vector2(GlobaleGameParameters.ScreenWidth - 25, 12),
            Color.White);
    }
}