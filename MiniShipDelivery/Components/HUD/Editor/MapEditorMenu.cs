using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets.Parts;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MiniShipDelivery.Components.World;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenu : BaseMenu
{
    private readonly TexturesTilemap _texturesTilemap;
    
    private const int MenuWidth = 60;

    private readonly FunctionBar _functionBarMapOption;
    private readonly FunctionBar _functionBarMapSprites;
    private readonly TexturesUiMenuMapOptions _texturesUiMenuMapOptions;

    public MapEditorMenu(Game game) : base(
        game,
        new Vector2(GlobaleGameParameters.ScreenWidth - MenuWidth, 24),
        new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24))
    {
        this._texturesTilemap = new TexturesTilemap(game);
        this._texturesUiMenuMapOptions = new TexturesUiMenuMapOptions(game);
        
        this._functionBarMapOption = new FunctionBar(
            game,
            new Vector2(0, 0),
            new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24),
            this.GetPositionArea,
            this.IsMouseInRange,
            this.DrawButtonMapOption,
            this.ChangeColorForActive);

        this._functionBarMapOption.FillOptions<MapEditorOption>(3);
        this._functionBarMapOption.ButtonAreaWasPressedEvent += this.MapMapOptionButtonAreaPressed;
        //this._functionBarMapOption.ButtonAreaHasExecutedEvent += this.MapMapOptionButtonAreaHasExecutedEvent;


        this._functionBarMapSprites = new FunctionBar(
            game,
            new Vector2(0, 20),
            new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24),
            this.GetPositionArea,
            this.IsMouseInRange,
            this.DrawButtonMapSprite,
            this.ChangeColorForActiveMapSprite);

        this._functionBarMapSprites.FillOptions<TilemapPart>(3);
        this._functionBarMapSprites.ButtonAreaWasPressedEvent += this.MapSpritesButtonAreaWasPressed;
        //this._functionBarMapSprites.ButtonAreaHasExecutedEvent += this.MapSpritesButtonAreaHasExecuted;
    }

    #region map sprite buttons

    private void DrawButtonMapSprite(SpriteBatch spriteBatch, Vector2 position, FunctionItem functionItem)
    {
        spriteBatch.Draw(
            this._texturesTilemap.Texture, 
            position + new Vector2(1, 1), 
            this._texturesTilemap.SpriteContent[(TilemapPart)functionItem.AssetPart],
            Color.White);
    }
    
    private Color ChangeColorForActiveMapSprite(FunctionItem functionItem, Color color)
    {
        if (functionItem.Selected)
        {
            color = Color.Yellow;
        }
        
        return color;
    }

  
    
    private void MapSpritesButtonAreaWasPressed(FunctionItem item)
    {
        item.Selected = true;
        this._functionBarMapSprites.ResetAllSelected(item);
    }

    #endregion

    #region map option

    private void MapMapOptionButtonAreaPressed(FunctionItem functionItem)
    {
        switch (functionItem.AssetPart)
        {
            case MapEditorOption.OnOffGrid:
                //MapManager.ShowGrid = !MapManager.ShowGrid;
                break;
            case MapEditorOption.ArrowLeft:
                break;
            case MapEditorOption.ArrowRight:
                break;
        }
    }
    
    private Color ChangeColorForActive(FunctionItem arg1, Color arg2)
    {
        return arg2;
    }
    
    private void DrawButtonMapOption(
        SpriteBatch spriteBatch,
        Vector2 position,
        FunctionItem functionItem)
    {
        var shiftPosition = position + new Vector2(1, 1);
        var menuMapOption = UiMenuMapOptionPart.None;
        switch (functionItem.AssetPart)
        {
            case MapEditorOption.OnOffGrid:
                menuMapOption = UiMenuMapOptionPart.ExclamationWithe;
                break;
            case MapEditorOption.ArrowLeft:
                menuMapOption = UiMenuMapOptionPart.ArrowLeft;
                break;
            case MapEditorOption.ArrowRight:
                menuMapOption = UiMenuMapOptionPart.ArrowRight;
                break;
        }
        
        spriteBatch.Draw(
            this._texturesUiMenuMapOptions.Texture,
            shiftPosition,
            this._texturesUiMenuMapOptions.SpriteContent[menuMapOption],
            Color.AliceBlue);
    }

    #endregion
    
    

    public void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBarMapOption.Draw(spriteBatch);
        this._functionBarMapSprites.Draw(spriteBatch);
    }
    
    private void MapMapOptionButtonAreaHasExecutedEvent(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        spriteBatch.DrawRectangle(
            position,
            functionItem.Size,
            isInRangeColor);
        
        var shiftPosition = position + new Vector2(1, 1);

        var menuMapOption = UiMenuMapOptionPart.None;
        switch (functionItem.AssetPart)
        {
            case MapEditorOption.OnOffGrid:
                menuMapOption = UiMenuMapOptionPart.ExclamationWithe;
                break;
            case MapEditorOption.ArrowLeft:
                menuMapOption = UiMenuMapOptionPart.ArrowLeft;
                break;
            case MapEditorOption.ArrowRight:
                menuMapOption = UiMenuMapOptionPart.ArrowRight;
                break;
        }
        
        spriteBatch.Draw(
            this._texturesUiMenuMapOptions.Texture,
            shiftPosition,
            this._texturesUiMenuMapOptions.SpriteContent[menuMapOption],
            Color.AliceBlue);
    }

    
    

    private void MapSpritesButtonAreaHasExecuted(
        SpriteBatch spriteBatch,
        bool inRange,
        Vector2 position,
        FunctionItem functionItem)
    {
        var isInRangeColor = SimpleThinksHelper.BoolToColor(inRange);

        if (functionItem.Selected)
        {
            isInRangeColor = Color.Yellow;
        }

        spriteBatch.DrawRectangle(
            position,
            functionItem.Size,
            isInRangeColor);

        spriteBatch.Draw(
            this._texturesTilemap.Texture, 
            position + new Vector2(1, 1), 
            this._texturesTilemap.SpriteContent[(TilemapPart)functionItem.AssetPart],
            Color.White);
        
        // Used only for debug
        //spriteBatch.WriteLine(this._spriteManager.Font,
        //    $"{item.TilemapPart}",
        //    pos + new Vector2(-40, 1));
    }
}