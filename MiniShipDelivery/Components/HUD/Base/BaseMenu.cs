using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Base;

public abstract class BaseMenu(
    Game game,
    Vector2 position,
    SizeF size)
{
    private readonly MenuFrame _menuFrame = new(game);
    protected readonly Game Game = game;
    
    public Vector2 Position => position;
    public SizeF Size => size;
        
    protected readonly ApplicationBus Bus = ApplicationBus.Instance;

    protected void DrawBaseFrame(SpriteBatch spriteBatch, MenuFrameType type)
    {
        this._menuFrame.DrawMenuFrame(spriteBatch,
            this.Bus.Camera.GetPosition() + position,
            size,
            type);
    }

    public virtual void Update()
    {
    }
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }
}