using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public class SpriteMapLayerBrownRoof(SpriteBaseTilemap spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart) => spriteBase.GetSprite(MapLayer.BrownRoof, numberPart);
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public Texture2D Texture => spriteBase.Texture;
    public int NumberPartForIcon => (int)TilemapPart.AroundOutBorder;
    public MapLayer Layer => MapLayer.BrownRoof;
    public int SpriteCount => spriteBase.SpriteContent.Count;
    public bool HasSpecificNumberPart => true;
    public int[] GetNumberParts() => spriteBase.SpriteContent.Select(s => (int)s.Key).ToArray();
}