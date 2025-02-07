using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.World.Sprites;

public class SpriteMapLayerBuildingWallBrown(SpriteBaseBuildingWalls spriteBase) : IMapEditableContent
{
    public SpriteSetup GetSprite(int numberPart) => spriteBase.GetSprite(MapLayer.BuildingBrown, numberPart);
    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;
    public Texture2D Texture { get; } = spriteBase.Texture;
    public int NumberPartForIcon { get; } = spriteBase.NumberPartForIcon;
    public MapLayer Layer => MapLayer.BuildingBrown;
    public int SpriteCount => spriteBase.SpriteContent.Count;
    public int[] GetNumberParts() => spriteBase.SpriteContent.Select(s => (int)s.Key).ToArray();
}