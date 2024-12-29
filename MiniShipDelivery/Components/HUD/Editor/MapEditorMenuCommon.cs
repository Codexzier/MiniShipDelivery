using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor
{
    public class MapEditorMenuCommon : BaseMenu
    {
        private readonly Vector2 _sideMenuMapTilePositionStart = new(1, 1);
        private readonly List<FunctionItem> _functionItems = new();
        
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
            foreach (var part in Enum.GetValues<InterfaceMenuEditorOptionPart>())
            {
                if(part == InterfaceMenuEditorOptionPart.None) continue;
                this.AddFunctionItem(part);
            }
        }

        private void AddFunctionItem(InterfaceMenuEditorOptionPart part)
        {
            this._functionItems.Add(
                new FunctionItem(
                    this.GetPositionArea(this._functionItems.Count, this._screenWidth, 6),
                    new SizeF(18, 18),
                    part));
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawBaseFrame(spriteBatch, MenuFrameType.Type2);

            foreach (var item in this._functionItems)
            {
                this.DrawSelectableArea(
                    spriteBatch, 
                    item);
            }
        }

        private void DrawSelectableArea(SpriteBatch spriteBatch, FunctionItem item)
        {
            var pos = this._camera.Position + 
                      item.Position + 
                      this._sideMenuMapTilePositionStart;
            
            var positionSelectable = item.Position +
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
                (InterfaceMenuEditorOptionPart)item.AssetPart);
            
            spriteBatch.DrawRectangle(
                pos,
                new SizeF(16, 16),
                isInRangeColor);
        }
    }
}
