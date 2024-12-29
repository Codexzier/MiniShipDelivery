using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

public interface ISelectableAreItem<out TAssetPart> where TAssetPart : Enum
{
    Vector2 Position { get; }
    SizeF Size { get; }
    TAssetPart AssetPart { get; }
    bool Selected { get; set; }
}