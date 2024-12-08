using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private SpriteManager _spriteManager;
        private CharacterNpc characterNpc;
        private CharacterPlayer _player;
        private ComponentInput _input;
        private ComponentMap _map;

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

            this._spriteManager = new SpriteManager(this.Content);
            this.characterNpc = new CharacterNpc(this._spriteManager)
            {
                Position = new Vector2(10, 10),
                Direction = Vector2.Zero,
                Speed = 20,
                FramesPerSecond = 10
            };
            this._player = new CharacterPlayer(this._spriteManager)
            {
                Position = new Vector2(50, 50),
                Direction = Vector2.Zero,
                Speed = 40,
                FramesPerSecond = 10
            };

            this._input = new ComponentInput(this._player);

            this._map = new ComponentMap(this._spriteManager);
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
            
            this._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
