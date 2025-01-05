namespace MiniShipDelivery.Components.World
{
    public enum TilemapPart
    {
        None = 0,
        GrassAndBrick_TopLeft = 13,
        GrassAndBrick_TopMiddle = 14,
        GrassAndBrick_TopRight = 15,
        GrassAndBrick_MiddleLeft = 16,
        GrassAndBrick_MiddleMiddle = 17,
        GrassAndBrick_MiddleRight = 18,
        GrassAndBrick_DownLeft = 19,
        GrassAndBrick_DownMiddle = 20,
        GrassAndBrick_DownRight = 21,

        GrassAndBrick_OutBorderTopLeft_InBorder_RightDown = 22,
        GrassAndBrick_OutBorderTopRight_InBorder_LeftDown = 23,
        GrassAndBrick_OutBorderDownLeft_InBorder_RightTop = 24,
        GrassAndBrick_OutBorderDownRight_InBorder_LeftTop = 25,

        GrassAndBrick_TopLeft_InBorder_RightDown = 26,
        GrassAndBrick_TopRight_InBorder_LeftDown = 27,
        GrassAndBrick_DownLeft_InBorder_RightTop = 28,
        GrassAndBrick_DownRight_InBorder_LeftTop = 29,

        GrassAndBrick_HorizontalTopDownLeft_OutBorder = 30,
        GrassAndBrick_HorizontalTopDown_OutBorder = 31,
        GrassAndBrick_HorizontalTopDownRight_OutBorder = 32,

        GrassAndBrick_AroundOutBorder = 33,

        GrassAndBrick_VerticalLeftRightTop_OutBorder = 34,
        GrassAndBrick_VerticalLeftRight_OutBorder = 35,
        GrassAndBrick_VerticalLeftRightDown_OutBorder = 36,
    }
}