using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Editor;

internal class MapEditorItem(Vector2 position, SizeF sizeF, MapEditorOption option)
{
    public Vector2 Position { get; } = position;
    public SizeF Size { get; } = sizeF;
    public MapEditorOption MapEditorOption { get; } = option;
}