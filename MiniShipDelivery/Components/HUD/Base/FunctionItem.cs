using System;
using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.HUD.Editor;
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
    //public MapEditorOption AssetPart { get; } = option;
}

// public class MenuOptionFunctionItem(
//     Vector2 position,
//     SizeF sizeF,
//     InterfaceMenuEditorOptionPart part) 
// {
//     public Vector2 Position { get; } = position;
//     public SizeF Size { get; } = sizeF;
//     //public InterfaceMenuEditorOptionPart AssetPart { get; } = part;
// }

// public interface IFunctionItem<out TAssetPart> where TAssetPart : Enum
// {
//     TAssetPart AssetPart { get; }
// }