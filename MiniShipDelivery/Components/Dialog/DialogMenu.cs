using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Dialog;

internal class DialogMenu(Game game) : BaseMenu(game,
    new Vector2(
        0,
        GlobalGameParameters.ScreenHeight - 36),
    new SizeF(
        GlobalGameParameters.ScreenWidth,
        36))
{
    private readonly SpriteFont _font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
    
    private string _outputTextUser = string.Empty;
    private string _outputTextNpc = string.Empty;


    public override void Update()
    {
        base.Update();
        
        if(!ApplicationBus.Instance.TextMessage.IsOn) return;
        
        this._outputTextUser = ApplicationBus.Instance.TextMessage.Text;

        if (ApplicationBus.Instance.TextMessage.HasPressedEnter)
        {
            switch (this._outputTextUser.ToLower())
            {
                case "hello":
                    this._outputTextNpc = "Hello, how are you?";
                    break;
                case "fine":
                    this._outputTextNpc = "Nice to hear that.";
                    break;
                case "bye":
                    this._outputTextNpc = "Goodbye!";
                    ApplicationBus.Instance.TextMessage.CanLeave = true;
                    break;
            }
            
            ApplicationBus.Instance.TextMessage.HasPressedEnter = false;
            ApplicationBus.Instance.TextMessage.CanClearForNextMessage = true;
        }
    }

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

        if (!this.Bus.TextMessage.IsOn)
        {
            spriteBatch.DrawString(
                this._font,
                "Want you start the Dialog? Press ENTER",
                pos + new Vector2(2, 0),
                Color.White);    
        }
        else
        {
            this.DrawPlayerText(spriteBatch, pos);
            this.DrawNpcText(spriteBatch, pos);
        }
    }
    
    private void DrawPlayerText(SpriteBatch spriteBatch, Vector2 pos)
    {
        if(string.IsNullOrEmpty(this._outputTextUser)) return;
        Color c = Color.White;
        if (ApplicationBus.Instance.TextMessage.CanClearForNextMessage)
        {
            c = Color.WhiteSmoke;
        }
        
        spriteBatch.DrawString(
            this._font,
            this._outputTextUser,
            pos + new Vector2(2, 0),
            c);
            
        spriteBatch.DrawString(
            this._font,
            $"{this._outputTextUser.Length}",
            pos + new Vector2(GlobalGameParameters.ScreenWidth - 25, 0),
            Color.White);
    }
    
    private void DrawNpcText(SpriteBatch spriteBatch, Vector2 pos)
    {
        spriteBatch.DrawString(
            this._font,
            this._outputTextNpc,
            pos + new Vector2(2, 12),
            Color.White);
    }
    
    
}