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
    private readonly FunctionBar _functionBarWindow;
    private readonly TexturesInterfaceMenuEditorOptions _textureUiMenuEditorOptions;
    
    public GameMenuCommon(Game game)
        : base(
            game,
            new Vector2(0, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24))
    {
        this._functionBar = new FunctionBar(
            game,
            new Vector2(0, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
            this.GetPositionArea,
            this.IsMouseInRange,
            this.DrawButtonGamingOptions,
            this.ChangeColorForActiveGamingOptions);
        
        
        this._textureUiMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game);
        this._functionBarWindow = new FunctionBar(
            game,
            new Vector2(GlobaleGameParameters.ScreenWidth - 24, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
            this.GetPositionArea,
            this.IsMouseInRange,
            this.DrawButtonOptions,
            this.ChangeColorForActiveOptions);
        this._functionBarWindow.AddOption(InterfaceMenuEditorOptionPart.Close);
        this._functionBarWindow.ButtonAreaWasPressedEvent += this.ButtonAreaPressedOptions;
    }

    private Color ChangeColorForActiveGamingOptions(FunctionItem arg1, Color arg2)
    {
        throw new System.NotImplementedException();
    }

    private void DrawButtonGamingOptions(SpriteBatch arg1, Vector2 arg2, FunctionItem arg3)
    {
        throw new System.NotImplementedException();
        
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

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type1);
        
        
        this._functionBarWindow.Draw(spriteBatch);
    }
}