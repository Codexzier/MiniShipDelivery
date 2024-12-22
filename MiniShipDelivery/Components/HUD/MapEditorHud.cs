using System.Collections.Generic;
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
        private MenuFrame _menuTop;

        private Vector2 _menuTopPosition = new Vector2(0, 0);

        private Size _menuTopSize;
        private Vector2 _menuTopOrigin;
        private int _sideMenuWidth = 60;
        private Size _sideMenuSize;

        private readonly List<SelectableMapItem> _selectableMapItems = new ();

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

            this._menuTop = new MenuFrame(spriteManager);
this._menuTopOrigin =  new Vector2(this._screenWidth - this._sideMenuWidth, 20);
            this._menuTopSize = new Size(this._screenWidth, 20);
            
            
            this._sideMenuSize = new Size(this._sideMenuWidth, this._screenHeight - 20);
            
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

        private void AddSelectableMapItem(TilemapPart item)
        {
            var multiply = this._selectableMapItems.Count;
            var pasInX = multiply / 3;
            var multiplyX = multiply < 3 ? multiply : multiply - (pasInX * 3)  ;
            var x = this._screenWidth - this._sideMenuWidth + 3 + ((multiplyX * 16) + (multiplyX * 2));
            var y = (this._screenHeight - this._sideMenuSize.Height) + 3 + ((pasInX * 16) + (pasInX * 2));
            
            var position = new Vector2(x, y); 
            
            this._selectableMapItems.Add(new SelectableMapItem(
                position, 
                new SizeF(18, 18),
                item));
        }

        internal void Update(GameTime gameTime)
        {
            this._menuTopPosition = this._camera.Position;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.TopMenu(spriteBatch);
            this.SideMenu(spriteBatch);

            // draw Tilemap on Sidemenu
        }

        private void TopMenu(SpriteBatch spriteBatch)
        {
            this._menuTop.DrawMenuFrame(spriteBatch,
                this._menuTopPosition,
                this._menuTopSize,
                MenuFrameType.Type3);
        }

        private void SideMenu(SpriteBatch spriteBatch)
        {
            // base frome
            this._menuTop.DrawMenuFrame(spriteBatch, 
                this._menuTopPosition + this._menuTopOrigin,
                this._sideMenuSize,
                MenuFrameType.Type2);

            // map sprites
            foreach (var item in this._selectableMapItems)
            {
                this.DrawSelectableMapPart(
                    spriteBatch,
                     item.Position,
                    item.Size,
                    item.TilemapPart);   
            }
            
        }

        private void DrawSelectableMapPart(SpriteBatch spriteBatch, Vector2 position, SizeF size, TilemapPart part)
        {
            var pos = this._menuTopPosition + position;
            var posSelectable = pos + (this._menuTopPosition * -1);
            spriteBatch.DrawRectangle(
                pos, 
                size, 
                this.IsMouseInRange(posSelectable, size));
            
            this._spriteManager.Draw(spriteBatch, 
                pos + new Vector2(1, 1),
                part);
            
            
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
            if (this._input.MousePosition.X > position.X && 
                this._input.MousePosition.Y > position.Y &&
                this._input.MousePosition.X < position.X + size.Width && 
                this._input.MousePosition.Y < position.Y + size.Height)
            {
                return Color.Green;
            }
            
            return Color.White;
        }
    }
}