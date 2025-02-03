﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CodexzierGameEngine.DataModels.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended.Graphics;

namespace MiniShipDelivery.Components.World.Sprites;


[MapLayerSetup("Street", 1, true)]
public class SpriteMapStreetV2 : IMapEditableContent, ISpriteMapContent
{
    public SpriteMapStreetV2(Game game)
    {
        this.Texture = game.Content.Load<Texture2D>("Map/Street");
        
        this.SpriteContent = SpriteMapHelper.GetSpriteSetups(this.Texture);
    }

    public SpriteSetup GetSprite(int numberPart)
    {
        return this.SpriteContent[numberPart];
    }

    public bool IsLayer(MapLayer mapLayer) => this.Layer == mapLayer;

    public IDictionary<int, SpriteSetup> SpriteContent { get; } 
    public Texture2D Texture { get; }
    public int NumberPartForIcon => 1;
    public Type EnumType => null;
    public MapLayer Layer => MapLayer.Street;
    public int SpriteCount => this.SpriteContent.Count;
    public bool HasSpecificNumberPart => false;
    public int[] GetNumberParts() => this.SpriteContent.Select(s => s.Key).ToArray();
}