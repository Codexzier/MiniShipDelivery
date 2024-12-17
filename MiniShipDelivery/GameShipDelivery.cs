using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Tilemap;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private AssetManager _spriteManager;
        private ColliderManager _colliderManager;
        private List<CharacterNpc> characterNPCs = new List<CharacterNpc>();
        private CharacterPlayer _player;
        private EmoteManager _emote;
        private InputManager _input;
        private MapManager _map;
        private HudManager _hudManager;

        private int _screenWidth = 320;
        private int _screenHeight = 180;

        public GameShipDelivery()
        {
            this._graphics = new GraphicsDeviceManager(this);

            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this._graphics.PreferredBackBufferWidth = 1280;
            this._graphics.PreferredBackBufferHeight = 720;
            this._graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(this.Window, this.GraphicsDevice, this._screenWidth, this._screenHeight);
            this._camera = new OrthographicCamera(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this._input = new InputManager();
            this._spriteManager = new AssetManager(this.Content);
            this._emote = new EmoteManager(this._spriteManager);
            

            var characterNpc = new CharacterNpc(this._spriteManager, this._emote, new Vector2(20, 20))
            {
                Direction = Vector2.Zero,
                Speed = 20,
                FramesPerSecond = 10                
            };
            this.characterNPCs.Add(characterNpc);

            var screenPosition = new Vector2(this._screenWidth / 2 - 8, this._screenHeight / 2);
            this._player = new CharacterPlayer(this._spriteManager, this._input, this._emote, screenPosition)
            {
                Direction = Vector2.Zero,
                Speed = 40,
                FramesPerSecond = 10
            };


            this._colliderManager = new ColliderManager();
            this._colliderManager.Add(this._player);
            foreach (var npc in this.characterNPCs)
            {
                this._colliderManager.Add(npc);
            }

            this._map = new MapManager(this._spriteManager, this._player, this.characterNPCs);

            this._hudManager = new HudManager(this._spriteManager, 
                this._input, 
                this._player,
                this._screenWidth,
                this._screenHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (this._input.HasPressToClose())
            {
                this.Exit();
            }

            this._input.Update(gameTime);
            foreach (var npc in this.characterNPCs)
            {
                npc.Update(gameTime);
            }
            this._player.Update(gameTime);
            this._map.Update(gameTime);
            this._hudManager.Update(gameTime);
            this._colliderManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = this._camera.GetViewMatrix();
            this._spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            this._map.Draw(this._spriteBatch, gameTime);

            foreach (var npc in this.characterNPCs)
            {
                npc.Draw(this._spriteBatch, gameTime);
            }
            this._player.Draw(this._spriteBatch, gameTime);
            this._hudManager.Draw(this._spriteBatch, gameTime);

            this._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
