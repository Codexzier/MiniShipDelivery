using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;

namespace MiniShipDelivery.Components;

public class GameRenderer(
    Game game,
    HudManager hudManager,
    OrthographicCamera camera,
    CharacterManager characterManager,
    MapManager map)
    : DrawableGameComponent(game)
{
    private readonly SpriteBatch _spriteBatch = new(game.GraphicsDevice);

    public override void Update(GameTime gameTime)
    {
       var delta = characterManager.Player.GetScreenPosition() - camera.Position;
        camera.Position += delta * 0.08f;
        
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.CornflowerBlue);
        
        var transformMatrix = camera.GetViewMatrix();
        
        this._spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        
        map.Draw(this._spriteBatch);
        characterManager.Draw(this._spriteBatch);
        hudManager.Draw(this._spriteBatch, gameTime);
        
        this._spriteBatch.End();
    }
}