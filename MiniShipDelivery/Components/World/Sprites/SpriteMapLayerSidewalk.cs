using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

[MapLayerSetup("Sidewalk", 2, true)]
public class SpriteMapLayerSidewalk(SpriteBaseTilemap spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart)
    {
        return spriteBase.GetSprite(MapLayer.Sidewalk, numberPart);
    }

    public bool IsLayer(MapLayer mapLayer) => MapLayer.Sidewalk == mapLayer;

    public Texture2D Texture => spriteBase.Texture;
    public int NumberPartForIcon => (int)TilemapPart.AroundOutBorder;
    public MapLayer Layer => MapLayer.Sidewalk;
    public int SpriteCount => spriteBase.SpriteContent.Count;
    public int[] GetNumberParts() => spriteBase.SpriteContent.Select(s => (int)s.Key).ToArray();
}