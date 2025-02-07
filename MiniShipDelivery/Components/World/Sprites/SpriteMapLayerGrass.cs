using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public class SpriteMapLayerGrass(SpriteBaseTilemap spriteBase) : BaseSpriteContent, IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart)=> spriteBase.GetSprite(MapLayer.Grass, numberPart);

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public Texture2D Texture => spriteBase.Texture;
    public int NumberPartForIcon => (int)TilemapPart.AroundOutBorder;
    public MapLayer Layer => MapLayer.Grass;
    public int SpriteCount => spriteBase.SpriteContent.Count;
    public int[] GetNumberParts() => spriteBase.SpriteContent.Select(s => (int)s.Key).ToArray();
}