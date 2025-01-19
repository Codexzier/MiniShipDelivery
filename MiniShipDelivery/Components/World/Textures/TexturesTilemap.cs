using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Textures
{
    public class TexturesTilemap : ISpriteContent<TilemapPart>, IMapEditableContent
    {
        public bool IsLayer(MapLayer mapLayer)
        {
            return this.GetMapLayers().Contains(mapLayer);
        }

        public MapLayer[] GetMapLayers()
        {
            return
            [
                MapLayer.Sidewalk,
                MapLayer.Grass,
                MapLayer.GrayRoof,
                MapLayer.BrownRoof
            ];
        }

        public Texture2D Texture { get; }
        public int NumberPartForIcon { get; } = (int)TilemapPart.AroundOutBorder;
        public Type EnumType { get; } = typeof(TilemapPart);

        public IDictionary<TilemapPart, Rectangle> SpriteContent { get; }
        
        public TexturesTilemap(Game game)
        {
            this.Texture = game.Content.Load<Texture2D>("RpgUrban/TilemapBasement");
            var shiftX = 0;
            var shiftY = 0;
            this.SpriteContent = new Dictionary<TilemapPart, Rectangle>
            {
                { TilemapPart.None, new Rectangle(0, 0, 0, 0) },
                // brick border and green floor
                { TilemapPart.TopLeft, new Rectangle((16 * shiftX), (16 * shiftY), 16, 16) },
                { TilemapPart.TopMiddle, new Rectangle((16 * (1 + shiftX))  + shiftX, (16 * shiftY), 16, 16) },
                { TilemapPart.TopRight, new Rectangle((16 * (2 + shiftX))  + shiftX, (16 * shiftY), 16, 16) },
                { TilemapPart.MiddleLeft, new Rectangle((16 * shiftX), (16 * (1 + shiftY)) , 16, 16) },
                { TilemapPart.MiddleMiddle, new Rectangle((16 * (1 + shiftX)) , (16 * 1)  + shiftY, 16, 16) },
                { TilemapPart.MiddleRight, new Rectangle((16 * (2 +  shiftX)) , (16 * 1)  + shiftY, 16, 16) },
                { TilemapPart.DownLeft, new Rectangle((16 * shiftX) , (16 * 2)  + shiftY, 16, 16) },
                { TilemapPart.DownMiddle, new Rectangle((16 * (1 +  shiftX)) , (16 * 2)  + shiftY, 16, 16) },
                { TilemapPart.DownRight, new Rectangle((16 * (2 + shiftX)) , (16 * 2)  + shiftY, 16, 16) }
            };
            
            // brick border and green floor without and in borders
            shiftX = 3;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.OutBorderTopLeft_InBorder_RightDown, new Rectangle((16 * shiftX), (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.OutBorderTopRight_InBorder_LeftDown, new Rectangle((16 * (1 + shiftX)) , (16 * shiftY) + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.OutBorderDownLeft_InBorder_RightTop, new Rectangle((16 * shiftX) , (16 * (1 + shiftY))  + shiftY, 16, 16));
            this.SpriteContent.Add(TilemapPart.OutBorderDownRight_InBorder_LeftTop, new Rectangle((16 * (1 + shiftX)) , (16 * (1 + shiftY))  + shiftY, 16, 16));
            
            // brick border and green floor with in borders
            shiftX = 5;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.TopLeft_InBorder_RightDown, new Rectangle((16 * shiftX), (16 * shiftY), 16, 16));
            this.SpriteContent.Add(TilemapPart.TopRight_InBorder_LeftDown, new Rectangle((16 * (1 + shiftX)), (16 * shiftY), 16, 16));
            this.SpriteContent.Add(TilemapPart.DownLeft_InBorder_RightTop, new Rectangle((16 * shiftX), (16 * (1 + shiftY)), 16, 16));
            this.SpriteContent.Add(TilemapPart.DownRight_InBorder_LeftTop, new Rectangle((16 * (1 + shiftX)), (16 * (1 + shiftY)), 16, 16));
            
            // brick border and green small floor horizontal with in borders
            shiftX = 3;
            shiftY = 2;
            this.SpriteContent.Add(TilemapPart.HorizontalTopDownLeft_OutBorder, new Rectangle((16 * shiftX), (16 * shiftY), 16, 16));
            this.SpriteContent.Add(TilemapPart.HorizontalTopDown_OutBorder, new Rectangle((16 * (1 + shiftX)), (16 * shiftY), 16, 16));
            this.SpriteContent.Add(TilemapPart.HorizontalTopDownRight_OutBorder, new Rectangle((16 * (2 + shiftX)), (16 * shiftY), 16, 16));
            
            // brick border and green single floor with in borders
            shiftX = 6;
            shiftY = 2;
            this.SpriteContent.Add(TilemapPart.AroundOutBorder, new Rectangle((16 * shiftX), (16 * shiftY), 16, 16));
            
            // brick border and green small floor vertical with in borders
            shiftX = 7;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.VerticalLeftRightTop_OutBorder, new Rectangle((16 * shiftX), (16 * shiftY), 16, 16));
            this.SpriteContent.Add(TilemapPart.VerticalLeftRight_OutBorder, new Rectangle((16 * shiftX), (16 * (shiftY + 1)), 16, 16));
            this.SpriteContent.Add(TilemapPart.VerticalLeftRightDown_OutBorder, new Rectangle((16 * shiftX), (16 * (shiftY + 2)), 16, 16));
        }

        public Rectangle GetSprite(MapLayer mapLayer, int numberPart)
        {
            TilemapPart tilemapPart = (TilemapPart)numberPart;
            var mapTile = this.SpriteContent[tilemapPart];
            if (mapLayer == MapLayer.Grass)
            {
                return mapTile;
            }

            switch (mapLayer)
            {
                case MapLayer.Sidewalk:
                    mapTile.X += 16 * 8;
                    break;
                case MapLayer.GrayRoof:
                    mapTile.Y += 16 * 3;
                    break;
                case MapLayer.BrownRoof:
                    mapTile.X += 16 * 8;
                    mapTile.Y += 16 * 3;
                    break;
            }
            
            return mapTile;
        }
    }
}