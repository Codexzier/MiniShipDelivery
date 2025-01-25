using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.World.Sprites;

namespace MiniShipDelivery.Components.World
{
    public class WorldManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly WorldMapAdjuster _adjuster;
        
        public readonly WorldMap Map;
        
        public WorldManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            
            WorldMapHelper.SetMapTextures(new WorldMapSprites(game));
            
            this.Map = new WorldMap();
            this._adjuster = new WorldMapAdjuster(game, this.Map);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // HUD depended content
            if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            if(WorldMapAdjuster.SelectedNumberPart < 0) return;
            
            this._adjuster.UpdateSetMapTile();
        }
       
        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix();
            
            this.Map.DrawAllLayers(this._spriteBatch);
            this._adjuster.Draw(this._spriteBatch);
            
            this._spriteBatch.End();
        }
    }
}