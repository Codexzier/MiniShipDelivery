using System.Linq;
using System.Runtime.CompilerServices;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public class SpriteMapLayerGrayRoof(SpriteBaseTilemap spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart) => spriteBase.GetSprite(MapLayer.GrayRoof, numberPart);
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public Texture2D Texture => spriteBase.Texture;
    public int NumberPartForIcon => (int)TilemapPart.AroundOutBorder;
    public MapLayer Layer => MapLayer.GrayRoof;
    public int[] GetNumberParts() => spriteBase.SpriteContent.Select(s => (int)s.Key).ToArray();
}