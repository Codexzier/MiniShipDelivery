using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.GameMenu;

internal class GameMenuCommon : BaseMenu
{
    private readonly FunctionBar _functionBar;
    private readonly TexturesGameMenu _texturesGameMenu;
    private readonly FunctionBar _functionBarWindow;
    private readonly TexturesInterfaceMenuEditorOptions _textureUiMenuEditorOptions;
    
    public GameMenuCommon(Game game)
        : base(
            game,
            new Vector2(0, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24))
    {
        this._texturesGameMenu = new TexturesGameMenu(game);
        this._functionBar = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
            this.DrawButtonGameMenu,
            this.ChangeColorForActiveGameMenu);
        this._functionBar.AddOption(GameMenuPart.QuestLog);
        this._functionBar.AddOption(GameMenuPart.Map);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressedGameMenu;
        
        this._textureUiMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game);
        this._functionBarWindow = new FunctionBar(
            game,
            this.Position,
            new Vector2(GlobaleGameParameters.ScreenWidth - 24, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
            this.DrawButtonOptions,
            this.ChangeColorForActiveOptions);
        this._functionBarWindow.AddOption(InterfaceMenuEditorOptionPart.Close);
        this._functionBarWindow.ButtonAreaWasPressedEvent += this.ButtonAreaPressedOptions;
    }

    #region Game Menu
    
    private Color ChangeColorForActiveGameMenu(FunctionItem functionItem, Color color)
    {
        if (functionItem.Selected)
        {
            return Color.Yellow;
        }
        
        return color;
    }
    
    private void ButtonAreaPressedGameMenu(FunctionItem functionItem)
    {
        switch (functionItem.AssetPart)
        {
            case GameMenuPart.QuestLog:
                break;
        }
    }

    private void DrawButtonGameMenu(SpriteBatch spriteBatch, Vector2 position, FunctionItem functionItem)
    {
        var tt = (GameMenuPart)functionItem.AssetPart;
        var rect = this._texturesGameMenu.SpriteContent[tt];
        spriteBatch.Draw(
            this._texturesGameMenu.Texture,
            position + new Vector2(1, 1),
            rect,
            Color.AliceBlue);
    }

    #endregion

    #region Menu Options
    
    private Color ChangeColorForActiveOptions(FunctionItem functionItem, Color color)
    {
        if (GlobaleGameParameters.ShowGrid && 
            (InterfaceMenuEditorOptionPart)functionItem.AssetPart == InterfaceMenuEditorOptionPart.Grid)
        {
            return Color.Yellow;
        }
        
        return color;
    }
    
    private void ButtonAreaPressedOptions(FunctionItem functionItem)
    {
        if ((InterfaceMenuEditorOptionPart)functionItem.AssetPart == InterfaceMenuEditorOptionPart.Close)
        {
            GlobaleGameParameters.HudView = HudOptionView.MainMenu;
        }
    }
    
    private void DrawButtonOptions(
        SpriteBatch spriteBatch,
        Vector2 position,
        FunctionItem functionItem)
    {
        var tt = (InterfaceMenuEditorOptionPart)functionItem.AssetPart;
        var rect = this._textureUiMenuEditorOptions.SpriteContent[tt];
        spriteBatch.Draw(
            this._textureUiMenuEditorOptions.Texture,
            position + new Vector2(1, 1),
            rect,
            Color.AliceBlue);
    }
    
    #endregion

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);
        
        this._functionBar.Draw(spriteBatch);
        this._functionBarWindow.Draw(spriteBatch);
    }
}