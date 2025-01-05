using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.HUD.Base;
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
            this.IsMouseInRange);

        this._functionBar.FillOptions<InterfaceMenuEditorOptionPart>(6);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        this._functionBar.ButtonAreaHasExecutedEvent += this.ButtonAreaHasExecute;
    }

    private void ButtonAreaPressed(FunctionItem functionItem)
    {
        Debug.WriteLine($"ButtonAreaPressed: {functionItem.AssetPart}");
    }
    
    private void ButtonAreaHasExecute(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        // TODO: Wrong area to draw the button
        var tt = (InterfaceMenuEditorOptionPart)functionItem.AssetPart;
        spriteBatch.Draw(
            this._textureUiMenuEditorOptions.Texture,
            position + new Vector2(1, 1),
            this._textureUiMenuEditorOptions.SpriteContent[tt],
            Color.AliceBlue);

        // this._assetManager.Draw(spriteBatch,
        //     position + new Vector2(1, 1),
        //     (InterfaceMenuEditorOptionPart)functionItem.AssetPart);
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBar.Draw(spriteBatch);
    }
}