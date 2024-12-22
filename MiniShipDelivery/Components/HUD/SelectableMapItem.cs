using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.Assets.Parts;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD;

internal class SelectableMapItem(Vector2 position, SizeF size, TilemapPart tilemapPart)
{
    public Vector2 Position { get; } = position;
    public SizeF Size { get; } = size;
    public TilemapPart TilemapPart { get; } = tilemapPart;
}