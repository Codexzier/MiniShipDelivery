using System.Collections.Generic;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Sprites;

public interface IWorldMapSprites
{
    bool TryGetTextureAndCutout(
        MapLayer mapLayer, 
        int numberPart, 
        out Texture2D texture, 
        out Rectangle cutout,
        out bool drawTop);

    IEnumerable<EditableEnvironmentItem> GetEditableEnvironments();
    int[] GetListOfValidateTileNumbers(MapLayer mapLayer);
    MapLayer[] GetLayers();
}