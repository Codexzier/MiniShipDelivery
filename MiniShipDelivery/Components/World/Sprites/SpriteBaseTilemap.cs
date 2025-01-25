using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites
{
    public class SpriteBaseTilemap : ISpriteContent<TilemapPart>
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

        public IDictionary<TilemapPart, SpriteSetup> SpriteContent { get; }
        
        public SpriteBaseTilemap(Game game)
        {
            this.Texture = game.Content.Load<Texture2D>("RpgUrban/TilemapBasement");
            var shiftX = 0;
            var shiftY = 0;
            this.SpriteContent = new Dictionary<TilemapPart, SpriteSetup>
            {
                { TilemapPart.None, new SpriteSetup { Cutout = new Rectangle(0, 0, 0, 0)} },
                // brick border and green floor
                { TilemapPart.TopLeft, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * shiftY), 16, 16) }},
                { TilemapPart.TopMiddle, new SpriteSetup { Cutout = new Rectangle((16 * (1 + shiftX))  + shiftX, (16 * shiftY), 16, 16) }},
                { TilemapPart.TopRight,new SpriteSetup { Cutout =  new Rectangle((16 * (2 + shiftX))  + shiftX, (16 * shiftY), 16, 16) }},
                { TilemapPart.MiddleLeft, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * (1 + shiftY)) , 16, 16) }},
                { TilemapPart.MiddleMiddle,new SpriteSetup { Cutout =  new Rectangle((16 * (1 + shiftX)) , (16 * 1)  + shiftY, 16, 16) }},
                { TilemapPart.MiddleRight, new SpriteSetup { Cutout = new Rectangle((16 * (2 +  shiftX)) , (16 * 1)  + shiftY, 16, 16) }},
                { TilemapPart.DownLeft, new SpriteSetup { Cutout = new Rectangle((16 * shiftX) , (16 * 2)  + shiftY, 16, 16) }},
                { TilemapPart.DownMiddle, new SpriteSetup { Cutout = new Rectangle((16 * (1 +  shiftX)) , (16 * 2)  + shiftY, 16, 16)} },
                { TilemapPart.DownRight, new SpriteSetup { Cutout = new Rectangle((16 * (2 + shiftX)) , (16 * 2)  + shiftY, 16, 16) }}
            };
            
            // brick border and green floor without and in borders
            shiftX = 3;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.OutBorderTopLeft_InBorder_RightDown, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * shiftY) + shiftY, 16, 16)});
            this.SpriteContent.Add(TilemapPart.OutBorderTopRight_InBorder_LeftDown, new SpriteSetup { Cutout = new Rectangle((16 * (1 + shiftX)) , (16 * shiftY) + shiftY, 16, 16)});
            this.SpriteContent.Add(TilemapPart.OutBorderDownLeft_InBorder_RightTop, new SpriteSetup { Cutout = new Rectangle((16 * shiftX) , (16 * (1 + shiftY))  + shiftY, 16, 16)});
            this.SpriteContent.Add(TilemapPart.OutBorderDownRight_InBorder_LeftTop, new SpriteSetup { Cutout = new Rectangle((16 * (1 + shiftX)) , (16 * (1 + shiftY))  + shiftY, 16, 16)});
            
            // brick border and green floor with in borders
            shiftX = 5;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.TopLeft_InBorder_RightDown, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * shiftY), 16, 16)});
            this.SpriteContent.Add(TilemapPart.TopRight_InBorder_LeftDown, new SpriteSetup { Cutout = new Rectangle((16 * (1 + shiftX)), (16 * shiftY), 16, 16)});
            this.SpriteContent.Add(TilemapPart.DownLeft_InBorder_RightTop, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * (1 + shiftY)), 16, 16)});
            this.SpriteContent.Add(TilemapPart.DownRight_InBorder_LeftTop, new SpriteSetup { Cutout = new Rectangle((16 * (1 + shiftX)), (16 * (1 + shiftY)), 16, 16)});
            
            // brick border and green small floor horizontal with in borders
            shiftX = 3;
            shiftY = 2;
            this.SpriteContent.Add(TilemapPart.HorizontalTopDownLeft_OutBorder, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * shiftY), 16, 16)});
            this.SpriteContent.Add(TilemapPart.HorizontalTopDown_OutBorder, new SpriteSetup { Cutout = new Rectangle((16 * (1 + shiftX)), (16 * shiftY), 16, 16)});
            this.SpriteContent.Add(TilemapPart.HorizontalTopDownRight_OutBorder, new SpriteSetup { Cutout = new Rectangle((16 * (2 + shiftX)), (16 * shiftY), 16, 16)});
            
            // brick border and green single floor with in borders
            shiftX = 6;
            shiftY = 2;
            this.SpriteContent.Add(TilemapPart.AroundOutBorder, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * shiftY), 16, 16)});
            
            // brick border and green small floor vertical with in borders
            shiftX = 7;
            shiftY = 0;
            this.SpriteContent.Add(TilemapPart.VerticalLeftRightTop_OutBorder, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * shiftY), 16, 16)});
            this.SpriteContent.Add(TilemapPart.VerticalLeftRight_OutBorder, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * (shiftY + 1)), 16, 16)});
            this.SpriteContent.Add(TilemapPart.VerticalLeftRightDown_OutBorder, new SpriteSetup { Cutout = new Rectangle((16 * shiftX), (16 * (shiftY + 2)), 16, 16)});
        }

        public SpriteSetup GetSprite(MapLayer mapLayer, int numberPart)
        {
            TilemapPart tilemapPart = (TilemapPart)numberPart;
            var mapTile = this.SpriteContent[tilemapPart];
            if (mapLayer == MapLayer.Grass)
            {
                return mapTile;
            }

            var rec = mapTile.Cutout;
            var isTopLayer = mapTile.IsTopLayer;
            switch (mapLayer)
            {
                case MapLayer.Sidewalk:
                    //rec.X += 16 * 8;
                    rec = new Rectangle(mapTile.Cutout.X + 16 * 8, mapTile.Cutout.Y, 16, 16);
                    break;
                case MapLayer.GrayRoof:
                    //rec.Y += 16 * 3;
                    rec = new Rectangle(mapTile.Cutout.X , mapTile.Cutout.Y + 16 * 3, 16, 16);
                    isTopLayer = true;
                    break;
                case MapLayer.BrownRoof:
                    // rec.X += 16 * 8;
                    // rec.Y += 16 * 3;
                    rec = new Rectangle(mapTile.Cutout.X + 16 * 8, mapTile.Cutout.Y + 16 * 3, 16, 16);
                    isTopLayer = true;
                    break;
            }
            
            return new SpriteSetup { Cutout = rec, IsTopLayer = isTopLayer };
        }
    }
}