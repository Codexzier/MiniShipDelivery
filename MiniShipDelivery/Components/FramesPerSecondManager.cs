﻿using System;
using Microsoft.Xna.Framework;
using MiniShipDelivery.Components.GameDebug;

namespace MiniShipDelivery.Components;

public class FramesPerSecondManager(Game game) : DrawableGameComponent(game)
{
    private TimeSpan _elapsedTime;
    private string _fps = string.Empty;

    public override void Draw(GameTime gameTime)
    {
        if(!GlobaleGameParameters.DebugMode) return;
        
        this._elapsedTime += gameTime.ElapsedGameTime;
        var fps = 1 / this._elapsedTime.TotalSeconds;
        this._elapsedTime = TimeSpan.Zero;
        this._fps = $"FPS: {fps:F1}";
        
        ConsoleManager.AddText(this._fps);
    }
}