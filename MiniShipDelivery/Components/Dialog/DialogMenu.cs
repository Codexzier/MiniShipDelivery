using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Dialog.Chatbots;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Dialog;

internal class DialogMenu : BaseMenu
{
    private readonly SpriteFont _font;
    
    private string _outputTextUser = string.Empty;
    private string _outputTextNpc = string.Empty;
    
    private readonly ChatbotType1 _chatbotType1 = new();
    private bool _chatBotStart;
    private int _countWords = 0;

    public DialogMenu(Game game) : base(game,
        new Vector2(
            0,
            GlobalGameParameters.ScreenHeight - 36),
        new SizeF(
            GlobalGameParameters.ScreenWidth,
            36))
    {
        this._font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
        this._chatbotType1.ChatAnswerPartEvent += this.ChatAnswerPartEvent;
        this._chatbotType1.ChatAnswerEvent += this.ChatAnswerEvent;
    }

    private void ChatAnswerPartEvent(string message)
    {
        this._outputTextNpc = message;
        this._countWords++;
    }

    private void ChatAnswerEvent(string message)
    {
        // Assistant: Hallo! Ich bin hier, um Ihnen zu helfen. Was möchten Sie heute erreichen?
        //     User:
        var coreMessage = message.Replace("Assistant: ", "").Split('\r', '\n')[0];
        
        this._outputTextNpc = coreMessage;
        
        
        ApplicationBus.Instance.TextMessage.CanClearForNextMessage = true;
        this._chatBotStart = false;
    }

    public override void Update()
    {
        base.Update();
        
        if(!ApplicationBus.Instance.TextMessage.IsOn) return;
        
        this._outputTextUser = ApplicationBus.Instance.TextMessage.Text;

        if (ApplicationBus.Instance.TextMessage.HasPressedEnter && !this._chatBotStart)
        {
            ApplicationBus.Instance.TextMessage.HasPressedEnter = false;
            this._chatBotStart = true;
            // switch (this._outputTextUser.ToLower())
            // {
            //     case "hello":
            //         this._outputTextNpc = "Hello, how are you?";
            //         break;
            //     case "fine":
            //         this._outputTextNpc = "Nice to hear that.";
            //         break;
            //     case "bye":
            //         this._outputTextNpc = "Goodbye!";
            //         ApplicationBus.Instance.TextMessage.CanLeave = true;
            //         break;
            // }
            
            Debug.WriteLine($"Output text: {this._outputTextUser}");
            this._countWords = 0;
            Task.Run(() => this._chatbotType1.SetUserInput(this._outputTextUser));
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
        
        spriteBatch.DrawString(
            this._font,
            $"{this._countWords}",
            pos + new Vector2(GlobalGameParameters.ScreenWidth - 25, 12),
            Color.White);
    }
    
    
}