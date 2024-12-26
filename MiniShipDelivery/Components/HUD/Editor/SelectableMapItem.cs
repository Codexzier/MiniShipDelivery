using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

internal class SelectableMapItem(Vector2 position, SizeF size, TilemapPart tilemapPart)
{
    public Vector2 Position { get; } = position;
    public SizeF Size { get; } = size;
    public TilemapPart TilemapPart { get; } = tilemapPart;
    public bool Selected { get; set; }
}