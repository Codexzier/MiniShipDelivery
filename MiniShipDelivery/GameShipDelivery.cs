using Microsoft.Xna.Framework;
using MiniShipDelivery.Components;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Objects;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private const int ScreenWidth = 320;
        private const int ScreenHeight = 180;

        public GameShipDelivery()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            this.Content.RootDirectory = "Content";
            this.Window.Title = "Mini Ship Delivery";
            this.IsMouseVisible = true;
            
            var viewportAdapter = new BoxingViewportAdapter(
                this.Window, 
                this.GraphicsDevice, 
                ScreenWidth, 
                ScreenHeight);
            
            var camera = new OrthographicCamera(viewportAdapter);


            // add all components
            var input = new InputManager( 
                this,
                ScreenWidth / (float)graphics.PreferredBackBufferWidth , 
                ScreenHeight / (float)graphics.PreferredBackBufferHeight);
            input.UpdateOrder = 1;
            this.Components.Add(input);
            
            var assetManager = new AssetManager(this);
            assetManager.UpdateOrder = 2;
            this.Components.Add(assetManager);

            // Map
            var map = new MapManager(this, assetManager, camera);
            map.UpdateOrder = 3;
            map.DrawOrder = 1;
            this.Components.Add(map);

            // Player
            var characterManager = new CharacterManager(
                this, 
                new Vector2(152f, 82f),
                assetManager, 
                input,
                camera);
            characterManager.UpdateOrder = 3;
            characterManager.DrawOrder = 2;
            this.Components.Add(characterManager);
            
            var colliderManager = new ColliderManager(
                this,
                characterManager);
            colliderManager.UpdateOrder = 4;
            this.Components.Add(colliderManager);
            
            
            var hudManager = new HudManager(
                this,
                assetManager, 
                input,
                camera,
                characterManager,
                ScreenWidth,
                ScreenHeight);
            hudManager.UpdateOrder = 5;
            hudManager.DrawOrder = 6;
            this.Components.Add(hudManager);
            
            // var gameRenderer = new GameRenderer(
            //     this, 
            //     hudManager, 
            //     camera, 
            //     characterManager,
            //     map);
            // gameRenderer.UpdateOrder = 99;
            // gameRenderer.DrawOrder = 1;
            //
            // this.Components.Add(gameRenderer);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }
    }
}
