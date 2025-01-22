using System;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.World;

public static class WorldMapHelper
{
    public static IWorldMapTextures MapTextures {get; private set;}

    public static void SetMapTextures(IWorldMapTextures textureMaps)
    {
        MapTextures = textureMaps;
    }

    public static WorldMapLayer[] CreateWorldMapLayers()
    {
        var worldMapLayers = new WorldMapLayer[Enum.GetValues<MapLayer>().Length];
        
        // street
        worldMapLayers[(int)MapLayer.Street] = CreateWorldMapLayer(MapLayer.Street, true);
        
        // sidewalk and grass
        worldMapLayers[(int)MapLayer.Sidewalk] = CreateWorldMapLayer(MapLayer.Sidewalk, false);
        worldMapLayers[(int)MapLayer.Grass] = CreateWorldMapLayer(MapLayer.Grass, false);
        
        worldMapLayers[(int)MapLayer.BuildingRed] = CreateWorldMapLayer(MapLayer.BuildingRed, false);
        worldMapLayers[(int)MapLayer.BuildingBrown] = CreateWorldMapLayer(MapLayer.BuildingBrown, false);
        
        // roof
        worldMapLayers[(int)MapLayer.GrayRoof] = CreateWorldMapLayer(MapLayer.GrayRoof, false);
        worldMapLayers[(int)MapLayer.BrownRoof] = CreateWorldMapLayer(MapLayer.BrownRoof, false);
        
        return worldMapLayers;
    }
    
    public static WorldMapLayer CreateWorldMapLayer(MapLayer mapLayer, bool fill)
    {
        const int fieldXy = 10;
        
        var wml = new WorldMapLayer
        {
            MapLayer = mapLayer,
            // collected validate tiles
            ListOfValidateTileNumbers = Enum
                .GetValues<TilemapPart>()
                .Select(s => (int)s).ToArray(),
            // y, x
            Map = new MapTile[fieldXy][]
        };

        for (int indexY = 0; indexY < fieldXy; indexY++)
        {
            wml.Map[indexY] = new MapTile[fieldXy];
            for (int indexX = 0; indexX < fieldXy; indexX++)
            {
                wml.Map[indexY][indexX] = new MapTile
                {
                    AssetNumber = GetDefaultNumberByLevelPart(mapLayer, fill),
                    Position = new TilePosition(indexX * 16, indexY * 16),
                    WorldMapLayerID = (int)mapLayer
                };
            }
        }

        return wml;
    }
    
    public static int GetDefaultNumberByLevelPart(MapLayer mapLayer, bool fill)
    {
        var defaultNumber = 0;
        
        if(!fill) return defaultNumber;
        
        switch (mapLayer)
        {
            case MapLayer.Street:
                defaultNumber = (int)StreetPart.Street01;
                break;
            case MapLayer.Sidewalk:
            case MapLayer.Grass:
            case MapLayer.GrayRoof:
            case MapLayer.BrownRoof:
                defaultNumber = (int)TilemapPart.MiddleMiddle;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mapLayer), mapLayer, null);
        }
        
        return defaultNumber;
    }
        
    public static void Draw(
        this SpriteBatch spriteBatch, 
        Vector2 position,
        MapLayer mapLayer,
        int numberPart,
        bool isDrawTop)
    {
        if (!MapTextures.TryGetTextureAndCutout(
                mapLayer,
                numberPart,
                out Texture2D texture,
                out Rectangle cutout,
                out bool drawTop))
        {
            throw new MissingMapTexturesAndCutout(numberPart, mapLayer);
        }

        if (isDrawTop != drawTop)
        {
            return;
        }
        
        spriteBatch.Draw(texture, position, cutout, Color.White);
    }
    
    public static void Draw(
        this SpriteBatch spriteBatch, 
        Vector2 position,
        MapLayer mapLayer,
        int numberPart)
    {
        if (!MapTextures.TryGetTextureAndCutout(
                mapLayer,
                numberPart,
                out Texture2D texture,
                out Rectangle cutout,
                out _))
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
        if (!MapTextures.TryGetTextureAndCutout(
                mapLayer,
                numberPart,
                out Texture2D texture,
                out Rectangle cutout,
                out bool drawTop))
        {
            throw new MissingMapTexturesAndCutout(numberPart, mapLayer);
        }
            
        spriteBatch.Draw(texture, position, cutout, new Color(Color.Gray, 0.8f));
    }
}