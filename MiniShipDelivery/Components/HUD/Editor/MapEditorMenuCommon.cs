using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MiniShipDelivery.Components.Persistence;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenuCommon : BaseMenu
{
    private readonly FunctionBar _functionBar;
    private readonly FunctionBar _functionBarWindow;
    private readonly TexturesInterfaceMenuEditorOptions _textureUiMenuEditorOptions;

    public MapEditorMenuCommon(Game game)
        : base(
            game, 
            new Vector2(0, 0), 
            new Size(GlobaleGameParameters.ScreenWidth, 24))
    {
        this._textureUiMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game);
        
        this._functionBar = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
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
            game,
            this.Position,
            new Vector2(GlobaleGameParameters.ScreenWidth - 24, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
            this.DrawButton,
            this.ChangeColorForActive);
        this._functionBarWindow.AddOption(InterfaceMenuEditorOptionPart.Close);
        this._functionBarWindow.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        MapEditorMenu.MenuField.Add(new RectangleF(0, 0, GlobaleGameParameters.ScreenWidth, 24));
    }

    private void DrawButton(
        SpriteBatch spriteBatch,
        Vector2 position,
        FunctionItem functionItem)
    {
        var tt = (InterfaceMenuEditorOptionPart)functionItem.NumberPart;
        var rect = this._textureUiMenuEditorOptions.SpriteContent[tt];
        spriteBatch.Draw(
            this._textureUiMenuEditorOptions.Texture,
            position + new Vector2(1, 1),
            rect,
            Color.AliceBlue);
    }

    private Color ChangeColorForActive(FunctionItem functionItem, Color color)
    {
        if (GlobaleGameParameters.ShowGrid && 
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
                PersistenceManager.LoadMap();
                break;
            case InterfaceMenuEditorOptionPart.Save:
                PersistenceManager.SaveMap();
                break;
            case InterfaceMenuEditorOptionPart.Grid:
                GlobaleGameParameters.ShowGrid = !GlobaleGameParameters.ShowGrid;
                break;
            case InterfaceMenuEditorOptionPart.ConsoleWindow:
                GlobaleGameParameters.ShowConsoleWindow = !GlobaleGameParameters.ShowConsoleWindow;
                break;
            case InterfaceMenuEditorOptionPart.Close:
                GlobaleGameParameters.HudView = HudOptionView.MainMenu;
                break;
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBar.Draw(spriteBatch);
        this._functionBarWindow.Draw(spriteBatch);
    }
}