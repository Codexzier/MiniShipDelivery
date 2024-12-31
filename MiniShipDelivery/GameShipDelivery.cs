using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components;
using MiniShipDelivery.Components.Assets;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Emote;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Objects;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private OrthographicCamera _camera;
        
        private readonly InputManager _input;
        private readonly AssetManager _assetManager;
        
        
        private readonly GameRenderer _gameRenderer;
        
        private ColliderManager _colliderManager;
        private readonly List<CharacterNpc> _characterNpCs = new ();
        private CharacterPlayer _player;
        private EmoteManager _emote;
        
        private MapManager _map;
        private HudManager _hudManager;

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
            
            this._camera = new OrthographicCamera(viewportAdapter);
            
            
            // add all components
            
            // Input
            this._input = new InputManager( 
                this,
                ScreenWidth / (float)graphics.PreferredBackBufferWidth , 
                ScreenHeight / (float)graphics.PreferredBackBufferHeight);
            this._input.UpdateOrder = 1;
            this.Components.Add(this._input);
            
            // Assets
            this._assetManager = new AssetManager(this);
            this._assetManager.UpdateOrder = 2;
            this.Components.Add(this._assetManager);
            
            // Map
            this._map = new MapManager(this, this._assetManager);
            
            // Player
            var screenPosition = new Vector2(ScreenWidth / 2 - 8, ScreenHeight / 2);
            this._player = new CharacterPlayer(this._assetManager, this._input, screenPosition, CharacterType.Men)
            {
                Direction = Vector2.Zero,
                Speed = 40,
                FramesPerSecond = 10
            };
            this._emote = new EmoteManager(this._assetManager);
            
            var characterNpc = new CharacterNpc(this._assetManager, this._emote, new Vector2(20, 20), CharacterType.Women)
            {
                Direction = Vector2.Zero,
                Speed = 20,
                FramesPerSecond = 10                
            };
            this._characterNpCs.Add(characterNpc);
            
            
            this._hudManager = new HudManager(
                this,
                this._assetManager, 
                this._input,
                this._camera,
                this._player,
                this._characterNpCs,
                ScreenWidth,
                ScreenHeight);
            this._hudManager.UpdateOrder = 3;
            this.Components.Add(this._hudManager);
            
            
            // Renderer
            this._gameRenderer = new GameRenderer(
                this, 
                this._hudManager, 
                this._camera, 
                this._map,
                this._player,
                this._characterNpCs,
                ScreenWidth, ScreenHeight);
            this._gameRenderer.UpdateOrder = 99;
            this.Components.Add(this._gameRenderer);
        }

        protected override void LoadContent()
        {
            this._colliderManager = new ColliderManager();
            this._colliderManager.Add(this._player);
            foreach (var npc in this._characterNpCs)
            {
                this._colliderManager.Add(npc);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var npc in this._characterNpCs)
            {
                npc.Update(gameTime);
            }
            this._player.Update(gameTime);
            
            this._map.Update(gameTime);
            this._colliderManager.Update(gameTime);


            // TODO: camera follow player
            var delta = this._player.Collider.Position 
                         - this._gameRenderer.GetCameraPosition() - new Vector2(158, 84);
            this._gameRenderer.AddPosition(delta * 0.08f);
            

            base.Update(gameTime);
        }
    }
}
