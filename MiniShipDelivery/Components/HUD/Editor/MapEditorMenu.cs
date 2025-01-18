using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.GameDebug;
using MonoGame.Extended;
using MiniShipDelivery.Components.HUD.Base;
using MiniShipDelivery.Components.HUD.Editor.Options;
using MiniShipDelivery.Components.HUD.Editor.Textures;
using MiniShipDelivery.Components.World;
using CodexzierGameEngine.DataModels.World;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.HUD.Editor;

public class MapEditorMenu : BaseMenu
{
    private readonly TexturesTilemap _texturesTilemap;
    
    private const int MenuWidth = 60;

    private readonly FunctionBar _functionBarMapOption;
    private readonly FunctionBar _functionBarMapSprites;
    private readonly TexturesUiMenuMapOptions _texturesUiMenuMapOptions;
    
    public static LayerPart TilemapLayer = LayerPart.Sidewalk;
    public static readonly List<RectangleF> MenuField = new();

    public MapEditorMenu(Game game) : base(
        game,
        new Vector2(GlobaleGameParameters.ScreenWidth - MenuWidth, 24),
        new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24))
    {
        this._texturesTilemap = new TexturesTilemap(game);
        this._texturesUiMenuMapOptions = new TexturesUiMenuMapOptions(game);
        

        this._functionBarMapOption = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 0),
            new Size(MenuWidth, 40),
            this.DrawButtonMapOption,
            this.ChangeColorForActive);
        this.InitMapOption();
        
        this._functionBarMapSprites = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 36),
            new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 40),
            this.DrawButtonMapSprite,
            this.ChangeColorForActiveMapSprite);

        this.InitMapSprites();

        const int left = GlobaleGameParameters.ScreenWidth - MenuWidth;
        MenuField.Add(new RectangleF(left, 0, MenuWidth, 24));
        MenuField.Add(new RectangleF(left, 24, MenuWidth, GlobaleGameParameters.ScreenHeight - 24));
    }

    #region map option
    
    private void InitMapOption()
    {
        this._functionBarMapOption.ManuelOptions(
        [
            UiMenuMapOptionPart.Street,
            UiMenuMapOptionPart.Sidewalk,
            UiMenuMapOptionPart.Building,
            
            UiMenuMapOptionPart.ArrowLeft,
            UiMenuMapOptionPart.TilemapSelect,
            UiMenuMapOptionPart.ArrowRight
        ], 3);

        this._functionBarMapOption.SetOption(UiMenuMapOptionPart.Sidewalk);
        
        this._functionBarMapOption.ButtonAreaWasPressedEvent += this.MapMapOptionButtonAreaPressed;
    }

    private void MapMapOptionButtonAreaPressed(FunctionItem functionItem)
    {
        switch (functionItem.AssetPart)
        {
            case UiMenuMapOptionPart.TilemapSelect:
                TilemapLayer += 1;
                if(TilemapLayer > LayerPart.BrownRoof)
                {
                    TilemapLayer = LayerPart.Sidewalk;
                }
                break;
            case UiMenuMapOptionPart.ArrowLeft:
                this._functionBarMapSprites.PageDown();
                break;
            case UiMenuMapOptionPart.ArrowRight:
                this._functionBarMapSprites.PageUp();
                break;
            case UiMenuMapOptionPart.Street:
                WorldMapAdjuster.SelectedMapLayer = LayerPart.Street;
                WorldMapAdjuster.SelectedTilemapPart = 0;
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
        var menuMapOption = (UiMenuMapOptionPart)functionItem.AssetPart;
        if (menuMapOption == UiMenuMapOptionPart.TilemapSelect)
        {
            menuMapOption = TilemapLayer switch
            {
                LayerPart.Grass => UiMenuMapOptionPart.TilemapGrass,
                LayerPart.Sidewalk => UiMenuMapOptionPart.TilemapSidewalk,
                LayerPart.GrayRoof => UiMenuMapOptionPart.TilemapGrayRoof,
                LayerPart.BrownRoof => UiMenuMapOptionPart.TilemapBrownRoof,
                _ => menuMapOption
            };
        }
        
        spriteBatch.Draw(
            this._texturesUiMenuMapOptions.Texture,
            shiftPosition,
            this._texturesUiMenuMapOptions.SpriteContent[menuMapOption],
            Color.AliceBlue);
    }

    #endregion
    
    #region map sprite buttons

    private void InitMapSprites()
    {
        this._functionBarMapSprites.FillOptions<TilemapPart>(3);
        this._functionBarMapSprites.ButtonAreaWasPressedEvent += this.MapSpritesButtonAreaWasPressed;
    }

    private void DrawButtonMapSprite(SpriteBatch spriteBatch, Vector2 position, FunctionItem functionItem)
    {
        spriteBatch.Draw(
            this._texturesTilemap.Texture, 
            position + new Vector2(1, 1), 
            this._texturesTilemap.GetSprite(TilemapLayer, (TilemapPart)functionItem.AssetPart),
            Color.White);
    }
    
    private Color ChangeColorForActiveMapSprite(FunctionItem functionItem, Color color)
    {
        if (functionItem.Selected)
        {
            color = Color.Yellow;
            
            ConsoleManager.AddText($"Select map tile: {functionItem.AssetPart}");
        }
        
        return color;
    }

    private void MapSpritesButtonAreaWasPressed(FunctionItem item)
    {
        item.Selected = true;
        if (item.AssetPart is TilemapPart tilemapPart)
        {
            WorldMapAdjuster.SelectedTilemapPart = (int)tilemapPart;
        }
        
        this._functionBarMapSprites.ResetAllSelected(item);
    }

    #endregion
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBarMapOption.Draw(spriteBatch);
        this._functionBarMapSprites.Draw(spriteBatch);
    }
}