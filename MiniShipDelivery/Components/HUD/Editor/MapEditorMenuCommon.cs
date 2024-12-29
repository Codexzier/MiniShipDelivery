using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor
{
    public class MapEditorMenuCommon : BaseMenu
    {
        private readonly Vector2 _sideMenuMapTilePositionStart = new(0, 0);
        
        public MapEditorMenuCommon(AssetManager assetManager,
            InputManager input,
            OrthographicCamera camera,
            int screenWidth,
            int screenHeight) 
            : base(assetManager, input, camera, 
                screenWidth, screenHeight, 
                new Vector2(0, 0), new Size(screenWidth, 26))
        {
            foreach (var part in Enum.GetValues<InterfaceMenuEditorOptionPart>())
            {
                
            }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

            
            this.DrawSelectableArea(
                spriteBatch, 
                this.GetPositionArea(0, this._screenWidth, 3));
        }

        private void DrawSelectableArea(SpriteBatch spriteBatch, Vector2 position)
        {
            var pos = this._camera.Position + 
                      position + 
                      this._sideMenuMapTilePositionStart;
            
            var positionSelectable = position +
                                     this._sideMenuMapTilePositionStart;
            
            var inRange = this.IsMouseInRange(positionSelectable, new SizeF(16, 16));
            if (inRange)
            {
                if (this._input.GetMouseLeftButtonReleasedState())
                {
                    // save map
                }
            }

            var isInRangeColor = this.BoolToColor(inRange);
            
            this._assetManager.Draw(spriteBatch,
                pos,
                InterfaceMenuEditorOptionPart.Save);
            
            spriteBatch.DrawRectangle(
                pos,
                new SizeF(16, 16),
                isInRangeColor);
        }
    }
}
