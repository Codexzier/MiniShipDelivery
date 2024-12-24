using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.Assets.Textures
{
    internal class TexturesTilemap : ISpriteProperties<TilemapPart>
    {
        public Texture2D Texture { get; }
        public IDictionary<TilemapPart, Rectangle> SpriteContent { get; }
        public TexturesTilemap(Texture2D texture)
        {
            this.Texture = texture;
            int shiftX = 0;
            int shiftY = 0;
            this.SpriteContent = new Dictionary<TilemapPart, Rectangle>
            {
                { TilemapPart.None, new Rectangle(0, 0, 2, 2) },
                // brick border and green floor
                { TilemapPart.GrassAndBrick_TopLeft, new Rectangle((16 * shiftX) + shiftX, (16 * shiftY) + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_TopMiddle, new Rectangle((16 * (1 + shiftX)) + 1 + shiftX, (16 * shiftY) + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_TopRight, new Rectangle((16 * (2 + shiftX)) + 2 + shiftX, (16 * shiftY) + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_MiddleLeft, new Rectangle((16 * shiftX) + shiftX, (16 * (1 + shiftY)) + 1 + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_MiddleMiddle, new Rectangle((16 * (1 + shiftX)) + 1, (16 * 1) + 1 + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_MiddleRight, new Rectangle((16 * (2 +  shiftX)) + 2, (16 * 1) + 1 + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_DownLeft, new Rectangle((16 * shiftX) + 0, (16 * 2) + 2 + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_DownMiddle, new Rectangle((16 * (1 +  shiftX)) + 1, (16 * 2) + 2 + shiftY, 16, 16) },
                { TilemapPart.GrassAndBrick_DownRight, new Rectangle((16 * (2 + shiftX)) + 2, (16 * 2) + 2 + shiftY, 16, 16) }
            };

            // brick border and green floor with Out and in borders
            shiftX = 3;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_OutBorderTopLeft_InBorder_RightDown, new Rectangle((16 * shiftX) + shiftX, (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_OutBorderTopRight_InBorder_LeftDown, new Rectangle((16 * (1 + shiftX)) + 1 + shiftX, (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_OutBorderDownLeft_InBorder_RightTop, new Rectangle((16 * shiftX) + shiftX, (16 * (1 + shiftY)) + 1 + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_OutBorderDownRight_InBorder_LeftTop, new Rectangle((16 * (1 + shiftX)) + 1 + shiftX, (16 * (1 + shiftY)) + 1 + shiftY, 16, 16));

            // brick border and green floor with in borders
            shiftX = 5;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_TopLeft_InBorder_RightDown, new Rectangle((16 * shiftX) + shiftX, (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_TopRight_InBorder_LeftDown, new Rectangle((16 * (1 + shiftX)) + 1 + shiftX, (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_DownLeft_InBorder_RightTop, new Rectangle((16 * shiftX) + shiftX, (16 * (1 + shiftY)) + 1 + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_DownRight_InBorder_LeftTop, new Rectangle((16 * (1 + shiftX)) + 1 + shiftX, (16 * (1 + shiftY)) + 1 + shiftY, 16, 16));

            // brick border and green small floor horizontal with in borders
            shiftX = 3;
            shiftY = 2;
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_HorizontalTopDownLeft_OutBordern, new Rectangle((16 * shiftX) + shiftX, (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_HorizontalTopDown_OutBorder, new Rectangle((16 * (1 + shiftX)) + 1 + shiftX, (16 * shiftY) +  shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_HorizontalTopDownRight_OutBorder, new Rectangle((16 * (2 + shiftX)) + 2 + shiftX, (16 * shiftY) + shiftY, 16, 16));

            // brick border and green single floor with in borders
            shiftX = 6;
            shiftY = 2;
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_AroundOutBorder, new Rectangle((16 * shiftX) + shiftX, (16 * shiftY) + shiftY, 16, 16));

            // brick border and green small floor vertical with in borders
            shiftX = 7;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_VerticalLeftRightTop_OutBordern, new Rectangle((16 * shiftX) + shiftX, (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_VerticalLeftRight_OutBorder, new Rectangle((16 * shiftX) + shiftX, (16 * (shiftY + 1)) + 1 + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.GrassAndBrick_VerticalLeftRightDown_OutBorder, new Rectangle((16 * shiftX) + shiftX, (16 * (shiftY + 2)) + 1 + shiftY, 16, 16));

            // gray wall and brown floor
            //{ TilemapPart.RoomGray_TopLeft, new Rectangle((16 * 0) + 0, (16 * 3) + 3, 16, 16) },
            //{ TilemapPart.RoomGray_TopMiddle, new Rectangle((16 * 1) + 1, (16 * 3) + 3, 16, 16) },
            //{ TilemapPart.RoomGray_TopRight, new Rectangle((16 * 2) + 2, (16 * 3) + 3, 16, 16) },
            //{ TilemapPart.RoomGray_MiddleLeft, new Rectangle((16 * 0) + 0, (16 * 4) + 4, 16, 16) },
            //{ TilemapPart.RoomGray_MiddleMiddle, new Rectangle((16 * 1) + 1, (16 * 4) + 4, 16, 16) },
            //{ TilemapPart.RoomGray_MiddleRight, new Rectangle((16 * 2) + 2, (16 * 4) + 4, 16, 16) },
            //{ TilemapPart.RoomGray_DownLeft, new Rectangle((16 * 0) + 0, (16 * 5) + 5, 16, 16) },
            //{ TilemapPart.RoomGray_DownMiddle, new Rectangle((16 * 1) + 1, (16 * 5) + 5, 16, 16) },
            //{ TilemapPart.RoomGray_DownRight, new Rectangle((16 * 2) + 2, (16 * 5) + 5, 16, 16) },
        }
    }
}