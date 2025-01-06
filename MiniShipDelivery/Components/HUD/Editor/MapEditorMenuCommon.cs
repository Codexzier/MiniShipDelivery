using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenuCommon : BaseMenu
{
    private readonly FunctionBar _functionBar;
    private readonly TexturesInterfaceMenuEditorOptions _textureUiMenuEditorOptions;

    public MapEditorMenuCommon(Game game)
        : base(
            game, 
            new Vector2(0, 0), new Size(GlobaleGameParameters.ScreenWidth, 24))
    {
        this._textureUiMenuEditorOptions = new TexturesInterfaceMenuEditorOptions(game);
        
        this._functionBar = new FunctionBar(
            game,
            new Vector2(0, 0),
            new Size(GlobaleGameParameters.ScreenWidth, 24),
            this.GetPositionArea,
            this.IsMouseInRange,
            this.DrawButton,
            this.ChangeColorForActive);

        this._functionBar.FillOptions<InterfaceMenuEditorOptionPart>(6);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        
        MapEditorMenu.MenuField.Add(new RectangleF(0, 0, GlobaleGameParameters.ScreenWidth, 24));
    }

    private void DrawButton(
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

    private Color ChangeColorForActive(FunctionItem functionItem, Color color)
    {
        if (MapManager.ShowGrid && 
            (InterfaceMenuEditorOptionPart)functionItem.AssetPart == InterfaceMenuEditorOptionPart.Grid)
        {
            return Color.Yellow;
        }
        
        return color;
    }

    private void ButtonAreaPressed(FunctionItem functionItem)
    {
        Debug.WriteLine($"ButtonAreaPressed: {functionItem.AssetPart}");
        switch (functionItem.AssetPart)
        {
            case InterfaceMenuEditorOptionPart.Grid:
                MapManager.ShowGrid = !MapManager.ShowGrid;
                break;
            case InterfaceMenuEditorOptionPart.Close:
                GlobaleGameParameters.HudView = HudOptionView.MainMenu;
                break;
        }
    }
    
    internal void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBar.Draw(spriteBatch);
    }
}