using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenuCommon : BaseMenu
{
    private readonly FunctionBar _functionBar;

    public MapEditorMenuCommon(
        AssetManager assetManager,
        InputManager input,
        OrthographicCamera camera,
        int screenWidth,
        int screenHeight)
        : base(assetManager, input, camera,
            screenWidth, screenHeight,
            new Vector2(0, 0), new Size(screenWidth, 24))
    {
        this._functionBar = new FunctionBar(
            input,
            camera,
            new Vector2(0, 0),
            new Size(screenWidth, 24),
            this.GetPositionArea,
            this.IsMouseInRange);

        this._functionBar.FillOptions<InterfaceMenuEditorOptionPart>(6);
        this._functionBar.ButtonAreaWasPressedEvent += this.ButtonAreaPressed;
        this._functionBar.ButtonAreaHasExecutedEvent += this.ButtonAreaHasExecute;
    }

    private void ButtonAreaHasExecute(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        spriteBatch.DrawRectangle(
            position,
            new SizeF(18, 18),
            isInRangeColor);

        this._assetManager.Draw(spriteBatch,
            position + new Vector2(1, 1),
            (InterfaceMenuEditorOptionPart)functionItem.AssetPart);
    }

    private void ButtonAreaPressed(FunctionItem functionItem)
    {
        Debug.WriteLine($"ButtonAreaPressed: {functionItem.AssetPart}");
    }

    internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBar.Draw(spriteBatch);
    }
}