using System;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public class WorldMapTextures : IWorldMapTextures
{
    public TexturesTilemap TexturesTilemap { get; }
    public TexturesStreet TexturesStreet { get; }

    public WorldMapTextures(Game game)
    {
        this.TexturesTilemap = new TexturesTilemap(game);
        this.TexturesStreet = new TexturesStreet(game);
    }

    public bool TryGetTextureAndCutout(MapLayer mapLayer, int numberPart, out Texture2D texture, out Rectangle cutout)
    {
        texture = null;
        cutout = Rectangle.Empty;

        switch (mapLayer)
        {
            case MapLayer.Street:
                texture = this.TexturesStreet.Texture;
                cutout = this.TexturesStreet.SpriteContent[(StreetPart)numberPart];
                // spriteBatch.Draw(
                //     this._textures.TexturesStreet.Texture, 
                //     position, 
                //     this._textures.TexturesStreet.SpriteContent[(StreetPart)numberPart],
                //     Color.White);
                break;
            case MapLayer.Sidewalk:
            case MapLayer.Grass:
            case MapLayer.GrayRoof:
            case MapLayer.BrownRoof:
                texture = this.TexturesTilemap.Texture;
                cutout = this.TexturesTilemap.GetSprite(mapLayer, (TilemapPart)numberPart);
                // spriteBatch.Draw(
                //     this._textures.TexturesTilemap.Texture, 
                //     position, 
                //     this._textures.TexturesTilemap.GetSprite(layerPart, (TilemapPart)numberPart),
                //     Color.White);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mapLayer), numberPart, "Missing Texture Layer");
        }
        
        return texture != null && cutout != Rectangle.Empty;
    }
}