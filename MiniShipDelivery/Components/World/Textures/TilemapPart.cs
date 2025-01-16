namespace MiniShipDelivery.Components.World.Textures
{
    public enum TilemapPart
    {
        None = 0,
        TopLeft = 1,
        TopMiddle = 2,
        TopRight = 3,
        MiddleLeft = 4,
        MiddleMiddle = 5,
        MiddleRight = 6,
        DownLeft = 7,
        DownMiddle = 8,
        DownRight = 9,

        OutBorderTopLeft_InBorder_RightDown = 10,
        OutBorderTopRight_InBorder_LeftDown = 11,
        OutBorderDownLeft_InBorder_RightTop = 12,
        OutBorderDownRight_InBorder_LeftTop = 13,

        TopLeft_InBorder_RightDown = 14,
        TopRight_InBorder_LeftDown = 15,
        DownLeft_InBorder_RightTop = 16,
        DownRight_InBorder_LeftTop = 17,

        HorizontalTopDownLeft_OutBorder = 18,
        HorizontalTopDown_OutBorder = 19,
        HorizontalTopDownRight_OutBorder = 20,

        AroundOutBorder = 21,

        VerticalLeftRightTop_OutBorder = 22,
        VerticalLeftRight_OutBorder = 23,
        VerticalLeftRightDown_OutBorder = 24
    }
}