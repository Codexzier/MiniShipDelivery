﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Assets.Textures;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base
{
    public class MenuFrame
    {
        //private readonly AssetManager _assetManager;
        private readonly Dictionary<MenuFrameType, int> _menuShift = new()
        {
            { MenuFrameType.Type1, 0 },
            { MenuFrameType.Type2, 9 },
            { MenuFrameType.Type3, 18 }
        };

        private readonly TexturesUiMenuMapFrames _uiMenuMapFrames;

        public MenuFrame(Game game)
        {
            this._uiMenuMapFrames = new TexturesUiMenuMapFrames(
                game.Content.Load<Texture2D>("Interface/MenuMapOptions"));
            
            //this._assetManager = assetManager;
        }

        public UiMenuFramePart GetMenuFrameByType(UiMenuFramePart part, MenuFrameType mft)
        {
            return (UiMenuFramePart)((int)part + this._menuShift[mft]);
        }

        public void DrawMenuFrame(SpriteBatch spriteBatch,
            Vector2 position,
            Size size,
            MenuFrameType mft)
        {
            var shift = this._menuShift[mft];

            var countMiddleForWidth = ((size.Width - (2 * 4)) / 4);
            var countMiddleForHeight = (size.Height / 4) - 1;

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                position,
                UiMenuFramePart.BaseFrame_Type1_TopLeft,
                UiMenuFramePart.BaseFrame_Type1_TopMiddle,
                UiMenuFramePart.BaseFrame_Type1_TopRight,
                mft);

            for (var y = 0; y < countMiddleForHeight - 1; y++)
            {
                this.CreateScreenWidthFrame(spriteBatch,
                    countMiddleForWidth,
                    position + new Vector2(0, 4 + (y * 4)),
                    UiMenuFramePart.BaseFrame_Type1_MiddleLeft,
                    UiMenuFramePart.BaseFrame_Type1_MiddleMiddle,
                    UiMenuFramePart.BaseFrame_Type1_MiddleRight,
                    mft);
            }

            var countMiddleForHeightEnd = countMiddleForHeight * 4;

            this.CreateScreenWidthFrame(spriteBatch,
                countMiddleForWidth,
                position + new Vector2(0, countMiddleForHeightEnd),
                UiMenuFramePart.BaseFrame_Type1_DownLeft,
                UiMenuFramePart.BaseFrame_Type1_DownMiddle,
                UiMenuFramePart.BaseFrame_Type1_DownRight,
                mft);
        }

        private Func<Vector2, int, Vector2>[] _funcs = new Func<Vector2, int, Vector2>[]
        {
            (vec, m) => vec,
            (vec, m) => vec + new Vector2(4 + (m * 4), 0),
            (vec, m) => vec + new Vector2(4 + (m * 4), 0)
        };

        private void CreateScreenWidthFrame(SpriteBatch spriteBatch, 
            int countMiddleForWidth, 
            Vector2 shiftPosition, 
            UiMenuFramePart left, 
            UiMenuFramePart middle, 
            UiMenuFramePart right, 
            MenuFrameType mft)
        {
            left = this.GetMenuFrameByType(left, mft);
            middle = this.GetMenuFrameByType(middle, mft);
            right = this.GetMenuFrameByType(right, mft);

            this.DrawScreenWidthFramePart(spriteBatch, shiftPosition, left);
            
            // this._assetManager.Draw(spriteBatch, 
            //     shiftPosition, 
            //     left);
            
            for (var x = 0; x < countMiddleForWidth; x++)
            {
                this.DrawScreenWidthFramePart(
                    spriteBatch, 
                    shiftPosition  + new Vector2(4 + (x * 4), 0), 
                    middle);
                // this._assetManager.Draw(spriteBatch, 
                //     shiftPosition + new Vector2(4 + (x * 4), 0), 
                //     middle);
            }
            
            this.DrawScreenWidthFramePart(
                spriteBatch, 
                shiftPosition  + new Vector2(4 + (countMiddleForWidth * 4), 0), 
                right);
            
            // this._assetManager.Draw(spriteBatch, 
            //     shiftPosition + new Vector2(4 + (countMiddleForWidth * 4), 0), 
            //     right);
        }

        private void DrawScreenWidthFramePart(
            SpriteBatch spriteBatch, 
            Vector2 position,
            UiMenuFramePart part)
        {
            spriteBatch.Draw(
                this._uiMenuMapFrames.Texture,
                position,
                this._uiMenuMapFrames.SpriteContent[part],
                Color.Aquamarine);
        }
    }
}