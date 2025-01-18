using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public class WorldMapTextures(Game game) : IWorldMapTextures
{
    private readonly TexturesTilemap _texturesTilemap = new(game);
    private readonly TexturesStreet _texturesStreet = new(game);

    public bool TryGetTextureAndCutout(MapLayer mapLayer, int numberPart, out Texture2D texture, out Rectangle cutout)
    {
        texture = null;
        cutout = Rectangle.Empty;

        switch (mapLayer)
        {
            case MapLayer.Street:
                texture = this._texturesStreet.Texture;
                cutout = this._texturesStreet.SpriteContent[(StreetPart)numberPart];
                break;
            case MapLayer.Sidewalk:
            case MapLayer.Grass:
            case MapLayer.GrayRoof:
            case MapLayer.BrownRoof:
                texture = this._texturesTilemap.Texture;
                cutout = this._texturesTilemap.GetSprite(mapLayer, (TilemapPart)numberPart);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mapLayer), numberPart, "Missing Texture Layer");
        }
        
        return texture != null && cutout != Rectangle.Empty;
    }
}