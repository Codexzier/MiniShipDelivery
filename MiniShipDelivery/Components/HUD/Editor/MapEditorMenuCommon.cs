﻿using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Controls;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MiniShipDelivery.Components.Persistence;
using MiniShipDelivery.Components.Sound;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenuCommon : BaseMenu
{
    private readonly FunctionBar _functionBar;
    private readonly FunctionBar _functionBarWindow;
    private readonly SpriteUiMenuEditorOptions _textureUiMenuEditorOptions;
    private readonly SoundManager _sound;
    private readonly SaveDialog _saveDialog;
    private readonly OpenDialog _openDialog;

    public MapEditorMenuCommon(Game game)
        : base(
            game, 
            new Vector2(0, 0), 
            new SizeF(GlobalGameParameters.ScreenWidth, 24))
    {
        this._saveDialog = new SaveDialog(game);
        this._openDialog = new OpenDialog(game);
        
        this._textureUiMenuEditorOptions = new SpriteUiMenuEditorOptions(game);
        
        this._functionBar = new FunctionBar(
            this.Position,
            new Vector2(0, 0),
            new Size(GlobalGameParameters.ScreenWidth, 24),
            this.DrawButton,
            this.ChangeColorForActive);

        this._functionBar.AddOption(InterfaceMenuEditorOptionPart.New);
        this._functionBar.AddOption(InterfaceMenuEditorOptionPart.Load);
        this._functionBar.AddOption(InterfaceMenuEditorOptionPart.Save);
        this._functionBar.AddOption(InterfaceMenuEditorOptionPart.Grid);
        this._functionBar.AddOption(InterfaceMenuEditorOptionPart.ConsoleWindow);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        // on right side
        this._functionBarWindow = new FunctionBar(
            this.Position,
            new Vector2(GlobalGameParameters.ScreenWidth - 24, 0),
            new Size(GlobalGameParameters.ScreenWidth, 24),
            this.DrawButton,
            this.ChangeColorForActive);
        this._functionBarWindow.AddOption(InterfaceMenuEditorOptionPart.Close);
        this._functionBarWindow.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        MapEditorMenu.MenuField.Add(new RectangleF(0, 0, GlobalGameParameters.ScreenWidth, 24));
        
        this._sound = game.GetComponent<SoundManager>();
    }

    private void DrawButton(
        SpriteBatch spriteBatch,
        Vector2 position,
        FunctionItem functionItem)
    {
        var tt = (InterfaceMenuEditorOptionPart)functionItem.NumberPart;
        var rect = this._textureUiMenuEditorOptions.SpriteContent[tt].Cutout;
        spriteBatch.Draw(
            this._textureUiMenuEditorOptions.Texture,
            position + new Vector2(1, 1),
            rect,
            Color.AliceBlue);
    }

    private Color ChangeColorForActive(FunctionItem functionItem, Color color)
    {
        if (GlobalGameParameters.ShowGrid && 
            (InterfaceMenuEditorOptionPart)functionItem.NumberPart == InterfaceMenuEditorOptionPart.Grid)
        {
            return Color.Yellow;
        }
        
        return color;
    }

    private void ButtonAreaPressed(FunctionItem functionItem, Action<FunctionItem> itemSetup)
    {
        Debug.WriteLine($"ButtonAreaPressed: {functionItem.NumberPart}");
        switch ((InterfaceMenuEditorOptionPart)functionItem.NumberPart)
        {
            case InterfaceMenuEditorOptionPart.New:
                PersistenceManager.NewMap();
                break;
            case InterfaceMenuEditorOptionPart.Load:
                
                this._openDialog.IsVisible = true;
                HudManager.MouseIsOverMenu = true;
                //GlobaleGameParameters.SystemDialogBox = true;
                this.Bus.TextMessage.IsOn = true;
                PersistenceManager.ReloadMapList();
                
                break;
            case InterfaceMenuEditorOptionPart.Save:

                this._saveDialog.IsVisible = true;
                
                // TODO: I need a system bus to transport information to the other manager
                //GlobaleGameParameters.DialogState.DialogOn = true;
                
                //GlobaleGameParameters.SystemDialogBox = true;
                
                this.Bus.TextMessage.Text = string.Empty;
                this.Bus.TextMessage.IsOn = true;
                HudManager.MouseIsOverMenu = true;
                
                break;
            case InterfaceMenuEditorOptionPart.Grid:
                GlobalGameParameters.ShowGrid = !GlobalGameParameters.ShowGrid;
                break;
            case InterfaceMenuEditorOptionPart.ConsoleWindow:
                GlobalGameParameters.ShowConsoleWindow = !GlobalGameParameters.ShowConsoleWindow;
                break;
            case InterfaceMenuEditorOptionPart.Close:
                GlobalGameParameters.HudView = HudOptionView.MainMenu;
                break;
        }
        
        this._sound.PlayPressed();
    }
    
    public override void Update()
    {
        base.Update();
        
        this._saveDialog.Update();
        this._openDialog.Update();
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBar.Draw(spriteBatch);
        this._functionBarWindow.Draw(spriteBatch);
        
        this._saveDialog.Draw(spriteBatch);
        this._openDialog.Draw(spriteBatch);
    }
}