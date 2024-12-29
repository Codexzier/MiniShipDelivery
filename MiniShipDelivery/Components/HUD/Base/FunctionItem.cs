using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public class FunctionItem(
    Vector2 position, 
    SizeF sizeF, 
    object option) 
{
    public Vector2 Position { get; } = position;
    public SizeF Size { get; } = sizeF;
    public object AssetPart { get; } = option;
    public bool Selected { get; set; }
}