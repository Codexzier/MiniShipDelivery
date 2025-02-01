using Microsoft.Xna.Framework;

namespace MiniShipDelivery.Components.World;

public class MiniMapChunks
{
    public int WorldMapChunkId { get; set; } = -99;
    public Color[][] MiniMap { get; set; }
}