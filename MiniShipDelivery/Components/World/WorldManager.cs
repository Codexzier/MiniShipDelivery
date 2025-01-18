using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Editor;
using MiniShipDelivery.Components.World.Textures;

namespace MiniShipDelivery.Components.World
{
    public class WorldManager : DrawableGameComponent
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly TexturesTilemap _texturesTilemap;
        private readonly TexturesStreet _texturesStreet;
        
        private readonly CameraManager _camera;

        public readonly WorldMap Map = new();
        
        private readonly WorldMapAdjuster _worldMapAdjuster;

        public WorldManager(Game game) : base(game)
        {
            this._spriteBatch = new SpriteBatch( game.GraphicsDevice );
            this._texturesTilemap = new TexturesTilemap(game);
            this._texturesStreet = new TexturesStreet(game);
            this._camera = game.GetComponent<CameraManager>();
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
            this._spriteBatch.BeginWithCameraViewMatrix(this._camera);
            
            this.Map.DrawAllLevels(
                this._spriteBatch, 
                this._texturesTilemap, 
                this._texturesStreet);
            
            this._worldMapAdjuster.Draw(
                this._spriteBatch,
                this._texturesTilemap.Texture,
                this._texturesTilemap.GetSprite(
                    MapEditorMenu.TilemapLevel, 
                    WorldMapAdjuster.SelectedTilemapPart));
            
            this._spriteBatch.End();
        }
    }
}