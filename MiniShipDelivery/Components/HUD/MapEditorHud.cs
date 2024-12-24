using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD
{
    internal class MapEditorHud
    {
        private AssetManager _spriteManager;
        private InputManager _input;
        private readonly OrthographicCamera _camera;
        private int _screenWidth;
        private int _screenHeight;
        
        // =================================
        // Top Menu
        private MenuFrame _menuTop;
        //private Vector2 _menuTopPosition = new (0, 0);
        private Size _menuTopSize;
        
        // =================================
        // side menu
        private Vector2 _menuSideOrigin;
        private int _sideMenuWidth = 60;
        private Size _sideMenuSize;
        private Vector2 _sideMenuMapOptionPosition = new (0, 0);
        private Vector2 _sideMenuMapTilePosition = new (0, 20);

        private readonly List<SelectableMapItem> _selectableMapItems = new ();
        private readonly List<MapEditorItem> _mapOptionItems = new ();

        public bool ShowGrid { get; private set; }

        private double _startPressMouseLeftButton;

        public MapEditorHud(AssetManager spriteManager, 
            InputManager input, 
            OrthographicCamera camera, 
            int screenWidth, int screenHeight)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._camera = camera;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;

            // ==============================================
            // Top Menu
            this._menuTop = new MenuFrame(spriteManager);
            this._menuTopSize = new Size(this._screenWidth, 20);
            
            // ==============================================
            // side Menu
            this._menuSideOrigin =  new Vector2(this._screenWidth - this._sideMenuWidth, 20);
            this._sideMenuSize = new Size(this._sideMenuWidth, this._screenHeight - 20);
            // tile map option
            this.AddMapoption(MapEditorOption.Deselect);
            this.AddMapoption(MapEditorOption.OnOffGrid);
            this.AddMapoption(MapEditorOption.Remove);
            
            // tile map
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_TopLeft);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_TopMiddle);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_TopRight);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_MiddleLeft);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_MiddleMiddle);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_MiddleRight);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_DownLeft);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_DownMiddle);
            this.AddSelectableMapItem(TilemapPart.GrassAndBrick_DownRight);
        }

        private void AddMapoption(MapEditorOption option)
        {
            this._mapOptionItems.Add(new MapEditorItem(
                this.GetPosition(this._mapOptionItems.Count),
                new SizeF(16, 16),
                option));
        }

        private void AddSelectableMapItem(TilemapPart item)
        {
            this._selectableMapItems.Add(new SelectableMapItem(
                this.GetPosition(this._selectableMapItems.Count), 
                new SizeF(18, 18),
                item));
        }

        private Vector2 GetPosition(int multiply)
        {
            var pasInX = multiply / 3;
            var multiplyX = multiply < 3 ? multiply : multiply - (pasInX * 3)  ;
            var x = this._screenWidth - this._sideMenuWidth + 3 + ((multiplyX * 16) + (multiplyX * 2));
            var y = (this._screenHeight - this._sideMenuSize.Height) + 3 + ((pasInX * 16) + (pasInX * 2));
            
            return new Vector2(x, y);
        }

        internal void Update(GameTime gameTime)
        {
            //this._menuTopPosition = this._camera.Position;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawGrid(spriteBatch);
            
            this.TopMenu(spriteBatch);
            this.SideMenu(spriteBatch, gameTime);

            // draw Tilemap on Sidemenu
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            if(!this.ShowGrid) return;

            var maxY = this._screenHeight / 16;
            var maxX = this._screenWidth / 16;
            for (int iY = 0; iY < maxY; iY++)
            {
                for (int iX = 0; iX < maxX; iX++)
                {
                    spriteBatch.DrawRectangle(
                        new Vector2(iX * 16, iY * 16), 
                        new SizeF(17, 17), 
                        Color.Gray);
                }
            }
        }

        private void TopMenu(SpriteBatch spriteBatch)
        {
            this._menuTop.DrawMenuFrame(spriteBatch,
                this._camera.Position,
                this._menuTopSize,
                MenuFrameType.Type3);
        }

        private void SideMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // base frome
            this._menuTop.DrawMenuFrame(spriteBatch, 
                this._camera.Position + this._menuSideOrigin,
                this._sideMenuSize,
                MenuFrameType.Type2);

            // map option
            foreach (var item in this._mapOptionItems)
            {
                this.DrawMapOption(spriteBatch, item, gameTime);
            }
            // map sprites
            foreach (var item in this._selectableMapItems)
            {
                this.DrawSelectableMapPart(spriteBatch, item);
            }
        }

        private void DrawMapOption(SpriteBatch spriteBatch, MapEditorItem item, GameTime gameTime)
        {
            var isInRangeColor = this.IsMouseInRange(item.Position, item.Size);
            if (isInRangeColor != Color.White)
            {
                if (this._input.MouseLeftButton)
                {
                        switch (item.MapEditorOption)
                        {
                            case MapEditorOption.Deselect:
                                break;
                            case MapEditorOption.OnOffGrid:
                            this.ShowGrid = true;
                            break;
                            case MapEditorOption.Remove:
                                break;
                        }
                }

                if (this._input.MouseRightButton)
                {
                    switch (item.MapEditorOption)
                    {
                        case MapEditorOption.Deselect:
                            break;
                        case MapEditorOption.OnOffGrid:
                            this.ShowGrid = false;
                            break;
                        case MapEditorOption.Remove:
                            break;
                    }
                }
            }

            if(this.ShowGrid && item.MapEditorOption == MapEditorOption.OnOffGrid)
            {
                isInRangeColor = Color.Yellow;
            }

            spriteBatch.DrawRectangle(
                this._camera.Position + item.Position + this._sideMenuMapOptionPosition, 
                item.Size,
                isInRangeColor);

            switch (item.MapEditorOption)
            {
                case MapEditorOption.Deselect:
                    this._spriteManager.Draw(
                        spriteBatch,
                        this._camera.Position + item.Position + this._sideMenuMapOptionPosition,
                        InterfacePart16x16.Arrow_Type1);
                    break;
                case MapEditorOption.OnOffGrid:
                    this._spriteManager.Draw(
                        spriteBatch,
                        this._camera.Position + item.Position + this._sideMenuMapOptionPosition,
                        InterfacePart16x16.Arrow_Type2);
                    break;
                case MapEditorOption.Remove:
                    this._spriteManager.Draw(
                        spriteBatch,
                        this._camera.Position + item.Position + this._sideMenuMapOptionPosition,
                        InterfacePart16x16.Arrow_Type3);
                    break;
            }
        }

        private void DrawSelectableMapPart(SpriteBatch spriteBatch, SelectableMapItem item)
        {
            var pos = this._camera.Position + item.Position + this._sideMenuMapTilePosition;
            var posSelectable = item.Position + this._sideMenuMapTilePosition;

            var isInRangeColor = this.IsMouseInRange(posSelectable, item.Size);
            if (isInRangeColor != Color.White)
            {
                if(this._input.MouseLeftButton)
                {
                    item.Selected = true;

                    // reset all other seleceted
                    foreach (var it in this._selectableMapItems.Where(it => !it.Equals(item)))
                    {
                        it.Selected = false;
                    }
                }
            }
            
            if (item.Selected)
            {
                isInRangeColor = Color.Yellow;   
            }
            
            spriteBatch.DrawRectangle(
                pos, 
                item.Size, 
                isInRangeColor);
            
            this._spriteManager.Draw(spriteBatch, 
                pos + new Vector2(1, 1),
                item.TilemapPart);

            // Used only for debug
            // spriteBatch.DrawString(this._spriteManager.Font, 
            //     $"{HudHelper.Vector2ToString(this._menuTopPosition)}", pos + new Vector2(-40, 1), 
            //     Color.White,
            //     0f,
            //     new Vector2(0, 0),
            //     0.3f,
            //     SpriteEffects.None, 1);
            //
            // spriteBatch.DrawString(this._spriteManager.Font, 
            //     $"{HudHelper.Vector2ToString(this._menuTopOrigin)}", pos + new Vector2(-40, 10), 
            //     Color.White,
            //     0f,
            //     new Vector2(0, 0),
            //     0.3f,
            //     SpriteEffects.None, 1);
        }

        private Color IsMouseInRange(Vector2 position, SizeF size)
        {
            return this._input.MousePosition.X > position.X &&
                this._input.MousePosition.Y > position.Y &&
                this._input.MousePosition.X < position.X + size.Width &&
                this._input.MousePosition.Y < position.Y + size.Height
                ? Color.DarkGray
                : Color.White;
        }
    }
}