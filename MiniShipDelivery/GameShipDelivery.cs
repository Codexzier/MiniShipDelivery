using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Tilemap;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private AssetManager _spriteManager;
        private ColliderManager _colliderManager;
        private CharacterNpc characterNpc;
        private CharacterPlayer _player;
        private EmoteManager _emote;
        private InputManager _input;
        private MapManager _map;
        private HudManager _hudManager;

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
            var viewportAdapter = new BoxingViewportAdapter(this.Window, this.GraphicsDevice, 320, 180);
            this._camera = new OrthographicCamera(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this._input = new InputManager();
            this._spriteManager = new AssetManager(this.Content);
            this._emote = new EmoteManager(this._spriteManager);
            

            this.characterNpc = new CharacterNpc(this._spriteManager, this._emote)
            {
                Direction = Vector2.Zero,
                Speed = 20,
                FramesPerSecond = 10
            };
            this.characterNpc.Collider.Position = new Vector2(150, 150);
            this._player = new CharacterPlayer(this._spriteManager, this._input, this._emote)
            {
                Direction = Vector2.Zero,
                Speed = 40,
                FramesPerSecond = 10
            };
            this._player.Collider.Position = new Vector2(100, 100);


            this._colliderManager = new ColliderManager();
            this._colliderManager.Add(this._player);
            this._colliderManager.Add(this.characterNpc);

            this._map = new MapManager(this._spriteManager);

            this._hudManager = new HudManager(this._spriteManager, this._input, this._player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (this._input.HasPressToClose())
            {
                this.Exit();
            }

            this._input.Update(gameTime);
            this.characterNpc.Update(gameTime);
            this._player.Update(gameTime);
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

            this.characterNpc.Draw(this._spriteBatch, gameTime);
            this._player.Draw(this._spriteBatch, gameTime);
            this._hudManager.Draw(this._spriteBatch, gameTime);

            this._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
