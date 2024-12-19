using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MiniShipDelivery.Components.Assets
{
    internal class TilemapPack : ISpriteProperties<TilemapPart>
    {
        public IDictionary<TilemapPart, Rectangle> SpriteContent { get; private set; }
        public TilemapPack()
        {
            this.SpriteContent = new Dictionary<TilemapPart, Rectangle>
            {
                { TilemapPart.RoomGray_TopLeft, new Rectangle((16 * 0) + 0, (16 * 3) + 3, 16, 16) },
                { TilemapPart.RoomGray_TopMiddle, new Rectangle((16 * 1) + 1, (16 * 3) + 3, 16, 16) },
                { TilemapPart.RoomGray_TopRight, new Rectangle((16 * 2) + 2, (16 * 3) + 3, 16, 16) },
                { TilemapPart.RoomGray_MiddleLeft, new Rectangle((16 * 0) + 0, (16 * 4) + 4, 16, 16) },
                { TilemapPart.RoomGray_MiddleMiddle, new Rectangle((16 * 1) + 1, (16 * 4) + 4, 16, 16) },
                { TilemapPart.RoomGray_MiddleRight, new Rectangle((16 * 2) + 2, (16 * 4) + 4, 16, 16) },
                { TilemapPart.RoomGray_DownLeft, new Rectangle((16 * 0) + 0, (16 * 5) + 5, 16, 16) },
                { TilemapPart.RoomGray_DownMiddle, new Rectangle((16 * 1) + 1, (16 * 5) + 5, 16, 16) },
                { TilemapPart.RoomGray_DownRight, new Rectangle((16 * 2) + 2, (16 * 5) + 5, 16, 16) },
            };
        }
    }
}