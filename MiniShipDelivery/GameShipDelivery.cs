using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Linq;
using System.Net.Mime;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private CharacterPlayer _player;
        private Vector2 _playerDirection;
        private float _playerSpeed = 40;

        public GameShipDelivery()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            var viewportAdapter = new BoxingViewportAdapter(
                Window, GraphicsDevice, 320, 180);
            _camera = new OrthographicCamera(viewportAdapter);

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new CharacterPlayer(Content)
            {
                Position = new Vector2(50, 50),
                Direction = new Vector2(0, 0),
                Speed = _playerSpeed
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var elapsedSec = gameTime.GetElapsedSeconds();

            if (_player.Position.X >= 320 - 16)
            {
                _playerDirection.X = -_playerSpeed;
            }
            else if (_player.Position.X <= 0)
            {
                _playerDirection.X = _playerSpeed;
            }

            if (_player.Position.Y >= 180 - 16)
            {
                _playerDirection.Y = -_playerSpeed;
            }
            else if(_player.Position.Y <= 0)
            {
                _playerDirection.Y = _playerSpeed;
            }

            _player.Position += _playerDirection * elapsedSec;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
            _player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private Texture2D GenerateRandomTexture(GraphicsDevice graphicsDevice, int width, int height)
        {
            Random random = new Random();

            var data = Enumerable.Range(0, width * height)
                .Select(_ => new Color(random.Next(256), random.Next(256), random.Next(256)))
                .ToArray();

            var texture = new Texture2D(graphicsDevice, width, height);
            texture.SetData(data);

            return texture;
        }
    }

    public class CharacterPlayer
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Sprite { get; set; }

        public CharacterPlayer(ContentManager content)
        {
            Sprite = content.Load<Texture2D>("Character/roguelikeChar_magenta");
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Base character sprite
            spriteBatch.Draw(Sprite, Position, new Rectangle(0, (16 * 3) + 3, 16, 16), Color.White);
        }
    }
}
