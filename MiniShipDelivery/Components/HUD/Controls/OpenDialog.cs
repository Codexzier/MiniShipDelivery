using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.Input;
using MiniShipDelivery.Components.Persistence;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class OpenDialog : BaseMenu
{
    private readonly InputManager _input;
    private readonly SpriteFont _font;
    private readonly TextButton _buttonOpen;
    private readonly TextButton _buttonCancel;
    
    public bool IsVisible { get; set; }
    public int SelectedIndex { get; set; }
    public string SelectedFilename { get; set; }

    
    public OpenDialog(Game game)
        : base(
            game,
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 80,
                GlobaleGameParameters.ScreenHeightHalf - 50),
            new Size(
                160,
                100))
    {
        this._font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
        this._input = game.GetComponent<InputManager>();
        
        this._buttonOpen = new TextButton(
            game,
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf - 70,
                GlobaleGameParameters.ScreenHeightHalf + 25),
            "Open");
        this._buttonOpen.WasPressedEvent += this.ButtonPressed;
        
        this._buttonCancel = new TextButton(
            game,
            new Vector2(
                GlobaleGameParameters.ScreenWidthHalf + 6,
                GlobaleGameParameters.ScreenHeightHalf + 25),
            "Cancel");
        this._buttonCancel.WasPressedEvent += this.ButtonPressed;
        
        this._buttonOpen.ShiftPosition = new Vector2(20, 2);
        this._buttonCancel.ShiftPosition = new Vector2(17, 2);
    }
    
    private void ButtonPressed(string buttonText)
    {
        if (buttonText == "Open")
        {
            // Open dialog logic
            PersistenceManager.LoadMap(this.SelectedFilename);
        }
        
        this.IsVisible = false;
        
        // TODO: I need a system bus to transport information to the other manager
        //GlobaleGameParameters.DialogState.DialogExit = true;
        
        
        GlobaleGameParameters.SystemDialogBox = false;
        HudManager.MouseIsOverMenu = false;
    }
    
    public override void Update()
    {
        if (!this.IsVisible) return;
        
        this._buttonOpen.Update();
        this._buttonCancel.Update();
        
        this.ButtonSelect(0);
        this.ButtonSelect(1);
        this.ButtonSelect(2);
        this.ButtonSelect(3);
        this.ButtonSelect(4);
    }

    private void ButtonSelect(int index)
    {
        var inRange =  HudHelper.IsMouseInRange(
            this.Position + new Vector2(0, index * 13), 
            new SizeF(this.Size.Width - 10, 13));
        
        if (inRange && this._input.GetMouseLeftButtonReleasedState(
                this.Position + new Vector2(0, index * 13), 
                new SizeF(this.Size.Width - 10, 13)))
        {
            this.SelectedIndex = index;
            this.SelectedFilename = PersistenceManager.MapFilenames[index];
            
            PersistenceManager.LoadMap(this.SelectedFilename);
            //this._sound.PlayPressed();
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!this.IsVisible) return;
        
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);
        
        var pos = this.Camera.Camera.Position + this.Position + new Vector2(5, 5);
        
        // black dialog box in frame
        spriteBatch.FillRectangle(
            pos,
            new SizeF(this.Size.Width - 10, this.Size.Height - 34),
            Color.Black,
            0.5f);

        for (int index = 0; index < 5; index++)
        {
            if(PersistenceManager.MapFilenames.Count <= index) continue;
            
            var filename = PersistenceManager.MapFilenames[index];
            
            spriteBatch.DrawString(
                this._font,
                filename,
                pos + new Vector2(2, index * 13),
                Color.White);
        }
        
        this._buttonOpen.Draw(spriteBatch);
        this._buttonCancel.Draw(spriteBatch);
        
        this.ButtonInRange(spriteBatch, pos, 0);
        this.ButtonInRange(spriteBatch, pos, 1);
        this.ButtonInRange(spriteBatch, pos, 2);
        this.ButtonInRange(spriteBatch, pos, 3);
        this.ButtonInRange(spriteBatch, pos, 4);
    }
    
    private void ButtonInRange(SpriteBatch spriteBatch, Vector2 pos, int index)
    {
        var inRange =  HudHelper.IsMouseInRange(
            this.Position + new Vector2(0, index * 13), 
            new SizeF(this.Size.Width - 10, 13));
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);
        
        spriteBatch.DrawRectangle(
            pos + new Vector2(0, index * 13),
            new SizeF(this.Size.Width - 10, 13),
            isInRangeColor);
        
        var isSelected = SimpleThinksHelper.BoolToColor(index == this.SelectedIndex);
        spriteBatch.DrawRectangle(
            pos + new Vector2(0, index * 13),
            new SizeF(this.Size.Width - 10, 13),
            isSelected);
    }
}