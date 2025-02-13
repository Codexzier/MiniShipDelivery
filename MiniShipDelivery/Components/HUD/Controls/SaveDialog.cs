using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.Persistence;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class SaveDialog : BaseMenu
{
    private readonly SpriteFont _font;
    private readonly TextButton _buttonSave;
    private readonly TextButton _buttonCancel;

    public bool IsVisible { get; set; }

    public SaveDialog(Game game)
        : base(
            game,
            new Vector2(
                GlobalGameParameters.ScreenWidthHalf - 80,
                GlobalGameParameters.ScreenHeightHalf - 30),
            new SizeF(
                160,
                60))
    {
        this._font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");

         this._buttonSave = new TextButton(
             game,
             new Vector2(
                 GlobalGameParameters.ScreenWidthHalf - 70,
                 GlobalGameParameters.ScreenHeightHalf + 5),
             "Save");
        this._buttonSave.WasPressedEvent += this.ButtonPressed;

        this._buttonCancel = new TextButton(
            game,
            new Vector2(
                GlobalGameParameters.ScreenWidthHalf + 6,
                GlobalGameParameters.ScreenHeightHalf + 5),
            "Cancel");
        this._buttonCancel.WasPressedEvent += this.ButtonPressed;
        
        this._buttonSave.TextPosition = new Vector2(20, 2);
        this._buttonCancel.TextPosition = new Vector2(17, 2);
    }

    private void ButtonPressed(string buttonText)
    {
        if (buttonText == "Save")
        {
            if (!ApplicationBus.Instance.TextMessage.IsOn ||
                string.IsNullOrEmpty(ApplicationBus.Instance.TextMessage.Text))
            {
                Debug.WriteLine("No text to save.");
                return;
            }
            
            PersistenceManager.SaveMap(ApplicationBus.Instance.TextMessage.Text);
        }

        this.IsVisible = false;
        
        // TODO: I need a system bus to transport information to the other manager
        //GlobaleGameParameters.DialogState.DialogExit = true;
        
        this.Bus.TextMessage.IsOn = false;
        HudManager.MouseIsOverMenu = false;
    }

    public override void Update()
    {
        if (!this.IsVisible) return;

        this._buttonSave.Update();
        this._buttonCancel.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!this.IsVisible) return;

        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);

        var pos = this.Bus.Camera.GetPosition() + this.Position + new Vector2(5, 5);

        // black dialog box in frame
        spriteBatch.FillRectangle(
            pos,
            new SizeF(this.Size.Width - 10, this.Size.Height - 34),
            Color.Black,
            0.5f);

        // frame around dialog box
        spriteBatch.DrawRectangle(
            pos,
            new SizeF(this.Size.Width - 10, this.Size.Height - 34),
            Color.White);

        // text in dialog box for used to save the actual map
        spriteBatch.DrawString(
            this._font,
            this.Bus.TextMessage.Text,
            pos + new Vector2(6, 6),
            Color.White);

        this._buttonSave.Draw(spriteBatch);
        this._buttonCancel.Draw(spriteBatch);
    }
}