using System;
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
    private const int MenuWidth = 60;

    private readonly FunctionBar _functionBarMapOption;
    private readonly FunctionBar _functionBarMapTilemapBasement;
    private readonly TexturesUiMenuMapOptions _texturesUiMenuMapOptions;
    
    public static readonly List<RectangleF> MenuField = new();

    public MapEditorMenu(Game game) : base(
        game,
        new Vector2(GlobaleGameParameters.ScreenWidth - MenuWidth, 24),
        new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24))
    {
        this._texturesUiMenuMapOptions = new TexturesUiMenuMapOptions(game);
        
        this._functionBarMapOption = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 0),
            new Size(MenuWidth, 40),
            this.DrawButtonMapOption,
            this.ChangeColorForActive);
        this.InitMapOption();
        
        this._functionBarMapTilemapBasement = new FunctionBar(
            game,
            this.Position,
            new Vector2(0, 36),
            new Size(MenuWidth, GlobaleGameParameters.ScreenHeight - 24),
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

    private void MapMapOptionButtonAreaPressed(FunctionItem functionItem, Action<FunctionItem> itemSetup)
    {
        switch (functionItem.AssetPart)
        {
            case UiMenuMapOptionPart.TilemapSelect:
                WorldMapAdjuster.SelectedMapMapLayer += 1;

                if (WorldMapAdjuster.SelectedMapMapLayer == MapLayer.BuildingRed)
                {
                    WorldMapAdjuster.SelectedMapMapLayer += 1;
                }
                if (WorldMapAdjuster.SelectedMapMapLayer == MapLayer.BuildingBrown)
                {
                    WorldMapAdjuster.SelectedMapMapLayer += 1;
                }
                
                if(WorldMapAdjuster.SelectedMapMapLayer > MapLayer.BrownRoof)
                {
                    WorldMapAdjuster.SelectedMapMapLayer = MapLayer.Sidewalk;
                }
                break;
            case UiMenuMapOptionPart.ArrowLeft:
                this._functionBarMapTilemapBasement.PageDown();
                break;
            case UiMenuMapOptionPart.ArrowRight:
                this._functionBarMapTilemapBasement.PageUp();
                break;
            case UiMenuMapOptionPart.Street:
                WorldMapAdjuster.SelectedMapMapLayer = MapLayer.Street;
                WorldMapAdjuster.SelectedTilemapPart = 0;
                functionItem.Selected = true;
                itemSetup(functionItem);
                break;
            case UiMenuMapOptionPart.Sidewalk:
                WorldMapAdjuster.SelectedMapMapLayer = MapLayer.Sidewalk;
                WorldMapAdjuster.SelectedTilemapPart = 0;
                functionItem.Selected = true;
                itemSetup(functionItem);
                break;
            case UiMenuMapOptionPart.Building:
                WorldMapAdjuster.SelectedMapMapLayer = MapLayer.BuildingRed;
                WorldMapAdjuster.SelectedTilemapPart = 0;
                functionItem.Selected = true;
                itemSetup(functionItem);
                break;
        }
    }
    
    private Color ChangeColorForActive(FunctionItem functionItem, Color color)
    {
        if ((UiMenuMapOptionPart)functionItem.AssetPart == UiMenuMapOptionPart.Street ||
            (UiMenuMapOptionPart)functionItem.AssetPart == UiMenuMapOptionPart.Sidewalk||
            (UiMenuMapOptionPart)functionItem.AssetPart == UiMenuMapOptionPart.Building)
        {
            if (functionItem.Selected)
            {
                color = Color.Yellow;
            }
        }
        
        return color;
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
            menuMapOption = WorldMapAdjuster.SelectedMapMapLayer switch
            {
                MapLayer.Street => UiMenuMapOptionPart.Street,
                MapLayer.Grass => UiMenuMapOptionPart.TilemapGrass,
                MapLayer.Sidewalk => UiMenuMapOptionPart.TilemapSidewalk,
                MapLayer.GrayRoof => UiMenuMapOptionPart.TilemapGrayRoof,
                MapLayer.BrownRoof => UiMenuMapOptionPart.TilemapBrownRoof,
                MapLayer.BuildingRed => UiMenuMapOptionPart.Building,
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
        this._functionBarMapTilemapBasement.FillOptions<TilemapPart>(3);
        this._functionBarMapTilemapBasement.ButtonAreaWasPressedEvent += this.MapTilemapBasementButtonAreaWasPressed;
    }

    private void DrawButtonMapSprite(SpriteBatch spriteBatch, Vector2 position, FunctionItem functionItem)
    {
        spriteBatch.Draw(
            position + new Vector2(1, 1),
            WorldMapAdjuster.SelectedMapMapLayer,
            (int)functionItem.AssetPart);
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

    private void MapTilemapBasementButtonAreaWasPressed(FunctionItem item, Action<FunctionItem> itemSetup)
    {
        item.Selected = true;
        if (item.AssetPart is TilemapPart tilemapPart)
        {
            WorldMapAdjuster.SelectedTilemapPart = (int)tilemapPart;
        }
        
        itemSetup(item);
    }

    #endregion
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

        this._functionBarMapOption.Draw(spriteBatch);
        this._functionBarMapTilemapBasement.Draw(spriteBatch);
    }
}