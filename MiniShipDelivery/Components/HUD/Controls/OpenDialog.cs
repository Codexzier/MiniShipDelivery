﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.Persistence;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Controls;

public class OpenDialog : BaseMenu
{
    private readonly SpriteFont _font;
    private readonly TextButton _buttonOpen;
    private readonly TextButton _buttonCancel;
    
    public bool IsVisible { get; set; }
    private int _selectedIndex;
    private string _selectedFilename;

    public OpenDialog(Game game)
        : base(
            game,
            new Vector2(
                GlobalGameParameters.ScreenWidthHalf - 80,
                GlobalGameParameters.ScreenHeightHalf - 50),
            new SizeF(
                160,
                100))
    {
        this._font = game.Content.Load<SpriteFont>("Fonts/KennyMiniSquare");
        
        this._buttonOpen = new TextButton(
            game,
            new Vector2(
                GlobalGameParameters.ScreenWidthHalf - 70,
                GlobalGameParameters.ScreenHeightHalf + 25),
            "Open");
        this._buttonOpen.WasPressedEvent += this.ButtonPressed;
        
        this._buttonCancel = new TextButton(
            game,
            new Vector2(
                GlobalGameParameters.ScreenWidthHalf + 6,
                GlobalGameParameters.ScreenHeightHalf + 25),
            "Cancel");
        this._buttonCancel.WasPressedEvent += this.ButtonPressed;
        
        this._buttonOpen.TextPosition = new Vector2(20, 2);
        this._buttonCancel.TextPosition = new Vector2(17, 2);
    }
    
    private void ButtonPressed(string buttonText)
    {
        if (buttonText == "Open")
        {
            // Open dialog logic
            PersistenceManager.LoadMap(this._selectedFilename);
        }
        
        this.IsVisible = false;
        this.Bus.TextMessage.IsOn = false;
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
        
        if (inRange && this.Bus.Inputs.GetMouseButtonReleasedStateLeft(
                this.Position + new Vector2(0, index * 13), 
                new SizeF(this.Size.Width - 10, 13), $"select {index}"))
        {
            if(PersistenceManager.MapFilenames.Count <= index) return;
            
            this._selectedIndex = index;
            this._selectedFilename = PersistenceManager.MapFilenames[index];
        }
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

        for (int index = 0; index < 5; index++)
        {
            if(PersistenceManager.MapFilenames.Count <= index) continue;
            
            this.ButtonInRange(spriteBatch, pos, index);
        }
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
        
        var isSelected = SimpleThinksHelper.BoolToColor(index == this._selectedIndex);
        spriteBatch.DrawRectangle(
            pos + new Vector2(0, index * 13),
            new SizeF(this.Size.Width - 10, 13),
            isSelected);
    }
}