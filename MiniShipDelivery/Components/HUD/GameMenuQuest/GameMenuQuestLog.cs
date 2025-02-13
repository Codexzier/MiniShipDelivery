using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.HUD.Base;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.GameMenuQuest;

public class GameMenuQuestLog(Game game) 
    : BaseMenu(game,
    new Vector2(
        GlobalGameParameters.ScreenWidthHalf - 100, 
        GlobalGameParameters.ScreenHeightHalf - 60),
    new SizeF(200, 120))
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
        var pos = this.Bus.Camera.GetPosition() + this.Position;
        
        spriteBatch.DrawRectangle(
            pos,
            this.Size,
            Color.Chocolate);
    }
}