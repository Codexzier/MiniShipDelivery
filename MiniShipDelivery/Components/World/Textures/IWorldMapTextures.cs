using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public interface IWorldMapTextures
{
    bool TryGetTextureAndCutout(MapLayer mapLayer, int numberPart, out Texture2D texture, out Rectangle cutout);
}