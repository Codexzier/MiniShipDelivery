﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.World.Textures;

public class WorldMapTextures : IWorldMapTextures
{
    private readonly IMapEditableContent[] _editorContents;

    public WorldMapTextures(Game game)
    {
        var spriteBaseTilemap = new SpriteBaseTilemap(game);
        var spriteBaseBuildingWalls = new SpriteBaseBuildingWalls(game);
        
        this._editorContents = [
            new TexturesStreet(game),
            new SpritesMapLayerSidewalk(spriteBaseTilemap),
            new SpriteMapLayerGrass(spriteBaseTilemap),
            new SpriteMapLayerBuildingWallRed(spriteBaseBuildingWalls),
            new SpriteMapLayerBuildingWallBrown(spriteBaseBuildingWalls),
            new SpriteMapLayerGrayRoof(spriteBaseTilemap),
            new SpriteMapLayerBrownRoof(spriteBaseTilemap)
        ];
    }

    public bool TryGetTextureAndCutout(
        MapLayer mapLayer, 
        int numberPart, 
        out Texture2D texture, 
        out Rectangle cutout,
        out bool drawTop)
    {
        texture = null;
        cutout = Rectangle.Empty;
        drawTop = false;

        foreach (var editorContent in this._editorContents)
        {
            if(!editorContent.IsLayer(mapLayer)) continue;
            
            texture = editorContent.Texture;
            //var mapTile = editorContent.GetSprite(mapLayer, numberPart);
            var mapTile = editorContent.GetSprite(numberPart);
            cutout = mapTile.Cutout;
            drawTop = mapTile.IsTopLayer;
            break;
        }

        if (texture == null || cutout == Rectangle.Empty)
        {
            throw new ArgumentOutOfRangeException(
                nameof(mapLayer), 
                numberPart, 
                "Missing Texture Layer");
        }

        return texture != null && cutout != Rectangle.Empty;
    }

    public IEnumerable<EditableEnvironmentItem> GetEditableEnvironments()
    {
        if (this._editorContents == null || this._editorContents.Length == 0)
        {
            throw new MissingMemberException("Missing Texture Layer");
        }

        var list = new List<EditableEnvironmentItem>();
        foreach (var editorContent in this._editorContents)
        {
            // var layers = editorContent.GetMapLayers();
            // foreach (var mapLayer in layers)
            // {
                list.Add(new EditableEnvironmentItem(
                    editorContent.Layer,
                    editorContent.NumberPartForIcon,
                    editorContent.Texture,
                    editorContent.GetSprite(editorContent.NumberPartForIcon).Cutout,
                    editorContent.EnumType));
            //}
        }

        return list;
    }
}