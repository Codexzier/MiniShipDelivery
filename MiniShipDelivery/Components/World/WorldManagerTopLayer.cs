using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery.Components.World;

public class WorldManagerTopLayer : DrawableGameComponent
{
    private readonly SpriteBatch _spriteBatch;
    private readonly WorldManager _worldManagerDownLayer;


    public WorldManagerTopLayer(Game game) : base(game)
    {
        this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
        this._worldManagerDownLayer = game.GetComponent<WorldManager>();
    }
       
    public override void Draw(GameTime gameTime)
    {
        this._spriteBatch.BeginWithCameraViewMatrix();
            
        this._worldManagerDownLayer
            .Map
            .DrawAllLayers(this._spriteBatch, true);
        
        this._spriteBatch.End();
    }
}