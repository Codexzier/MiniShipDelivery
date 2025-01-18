using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.World;

public static class WorldMapHelper
{
    private static IWorldMapTextures _mapTextures;

    public static void SetMapTextures(IWorldMapTextures textureMaps)
    {
        _mapTextures = textureMaps;
    }
        
    public static void Draw(
        this SpriteBatch spriteBatch, 
        Vector2 position,
        MapLayer mapLayer,
        int numberPart)
    {
        if (!_mapTextures.TryGetTextureAndCutout(
                mapLayer,
                numberPart,
                out Texture2D texture,
                out Rectangle cutout))
        {
            throw new MissingMapTexturesAndCutout(numberPart, mapLayer);
        }
            
        spriteBatch.Draw(texture, position, cutout, Color.White);
    }
    
    public static void DrawWithTransparency(
        this SpriteBatch spriteBatch, 
        Vector2 position,
        MapLayer mapLayer,
        int numberPart)
    {
        if (!_mapTextures.TryGetTextureAndCutout(
                mapLayer,
                numberPart,
                out Texture2D texture,
                out Rectangle cutout))
        {
            throw new MissingMapTexturesAndCutout(numberPart, mapLayer);
        }
            
        spriteBatch.Draw(texture, position, cutout, new Color(Color.Gray, 0.8f));
    }
}