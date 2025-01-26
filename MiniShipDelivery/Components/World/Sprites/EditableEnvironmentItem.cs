using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Sprites;

public class EditableEnvironmentItem(
    MapLayer mapLayer,
    Texture2D texture,
    Rectangle cutout,
    int[] numberParts)
{
    public MapLayer Layer { get; } = mapLayer;
    public Rectangle Cutout { get; } = cutout;
    public Texture2D Texture { get; } = texture;
    public int[] NumberParts { get; } = numberParts;
}