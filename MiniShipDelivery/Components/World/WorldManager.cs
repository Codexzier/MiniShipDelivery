using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.World
{
    public class WorldManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly WorldMapTextures _worldMapTextures;
        private readonly WorldMapAdjuster _worldMapAdjuster;
        
        public readonly WorldMap Map = new();
        
        public WorldManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._worldMapTextures = new WorldMapTextures(game);

            game.GetComponent<InputManager>();
            this._worldMapAdjuster = new (game, this.Map);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // HUD depended content
            if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
            if(WorldMapAdjuster.SelectedTilemapPart == TilemapPart.None) return;
            
            this._worldMapAdjuster.UpdateSetMapTile();
        }
       
        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.BeginWithCameraViewMatrix();
            
            this.Map.DrawAllLevels(
                this._spriteBatch, 
                this._worldMapTextures);
            
            this._worldMapAdjuster.Draw(
                this._spriteBatch,
                this._worldMapTextures);
            
            this._spriteBatch.End();
        }
    }
}