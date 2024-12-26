using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniShipDelivery.Components.HUD.Editor
{
    internal class MapEditorMenu
    {
        private MenuFrame _menuFrame;
        private int _sideMenuWidth = 60;
        private AssetManager _spriteManager;
        private readonly InputManager _input;
        private readonly OrthographicCamera _camera;
        private readonly int _screenWidth;
        private readonly int _screenHeight;
        private Vector2 _menuSideOrigin;
        private Size _sideMenuSize;
        private Vector2 _sideMenuMapOptionPosition = new(0, 0);
        private Vector2 _sideMenuMapTilePosition = new(0, 20);

        private readonly List<SelectableMapItem> _selectableMapItems = new();
        private readonly List<MapEditorItem> _mapOptionItems = new();

        public MapEditorMenu(
            AssetManager spriteManager,
            InputManager input,
            OrthographicCamera camera, 
            int screenWidth, 
            int screenHeight)
        {
            this._spriteManager = spriteManager;
            this._input = input;
            this._camera = camera;
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this._menuSideOrigin = new Vector2(this._screenWidth - this._sideMenuWidth, 20);
            this._sideMenuSize = new Size(this._sideMenuWidth, this._screenHeight - 20);

            this._menuFrame = new MenuFrame(spriteManager);

            // tile map option
            this.AddMapoption(MapEditorOption.Deselect);
            this.AddMapoption(MapEditorOption.OnOffGrid);
            this.AddMapoption(MapEditorOption.Remove);

            // tile map
            foreach (var tilemapPart in Enum.GetValues<TilemapPart>())
            {
                if (tilemapPart == TilemapPart.None) continue;
                this.AddSelectableMapItem(tilemapPart);
            }
        }

        public bool ShowGrid { get; private set; }

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
            var multiplyX = multiply < 3 ? multiply : multiply - (pasInX * 3);
            var x = this._screenWidth - this._sideMenuWidth + 3 + ((multiplyX * 16) + (multiplyX * 2));
            var y = (this._screenHeight - this._sideMenuSize.Height) + 3 + ((pasInX * 16) + (pasInX * 2));

            return new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // base frome
            this._menuFrame.DrawMenuFrame(spriteBatch,
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
                if (this._input.GetMouseLeftButtonReleasedState())
                {
                    switch (item.MapEditorOption)
                    {
                        case MapEditorOption.Deselect:
                            this.ResetAllSelected();
                            break;
                        case MapEditorOption.OnOffGrid:
                            this.ShowGrid = !this.ShowGrid;
                            break;
                        case MapEditorOption.Remove:
                            break;
                    }
                }
            }

            if (this.ShowGrid && item.MapEditorOption == MapEditorOption.OnOffGrid)
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
                if (this._input.GetMouseLeftButtonReleasedState())
                {
                    item.Selected = true;

                    this.ResetAllSelected(item);
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
            //spriteBatch.WriteLine(this._spriteManager.Font,
            //    $"{item.TilemapPart}",
            //    pos + new Vector2(-40, 1));
        }

        /// <summary>
        /// Reset all selected items.
        /// </summary>
        private void ResetAllSelected(SelectableMapItem item = null)
        {
            foreach (var mapItem in this._selectableMapItems.Where(smi => !smi.Equals(item)))
            {
                mapItem.Selected = false;
            }
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
