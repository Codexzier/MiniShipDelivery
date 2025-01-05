﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public abstract class BaseMenu(
    Game game,
    int screenWidth,
    int screenHeight,
    Vector2 position,
    Size size)
{
    private readonly MenuFrame _menuFrame = new(game);
        
    private readonly InputManager _input = game.GetComponent<InputManager>();
    protected readonly CameraManager Camera = game.GetComponent<CameraManager>();

    protected readonly int ScreenWidth = screenWidth;
    protected readonly int ScreenHeight = screenHeight;

    protected void DrawBaseFrame(SpriteBatch spriteBatch, MenuFrameType type)
    {
        this._menuFrame.DrawMenuFrame(spriteBatch,
            this.Camera.Camera.Position + position,
            size,
            type);
    }
        
    protected bool IsMouseInRange(Vector2 startPosition, SizeF areaSize)
    {
        return this._input.Inputs.MousePosition.X > startPosition.X &&
               this._input.Inputs.MousePosition.Y > startPosition.Y &&
               this._input.Inputs.MousePosition.X < startPosition.X + areaSize.Width &&
               this._input.Inputs.MousePosition.Y < startPosition.Y + areaSize.Height;
    }
    
    protected Vector2 GetPositionArea(int multiply, int width, int columns)
    {
        var pasInX = multiply / columns;
        var multiplyX = multiply < columns ? multiply : multiply - (pasInX * columns);
        var x = this.ScreenWidth - width + 3 + ((multiplyX * 16) + (multiplyX * 2));
        var y = position.Y + 3 + ((pasInX * 16) + (pasInX * 2));

        return new Vector2(x, y);
    }
}