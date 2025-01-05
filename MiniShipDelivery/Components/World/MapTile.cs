using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.World;

public class MapTile(TilemapPart tilemapPart, Vector2 position)
{
    public TilemapPart TilemapPart { get; private set; } = tilemapPart;
    public Vector2 Position { get; } = position;

    public void UpdateTilemapPart(TilemapPart selectedTilemapPart)
    {
        this.TilemapPart = selectedTilemapPart;
    }
}