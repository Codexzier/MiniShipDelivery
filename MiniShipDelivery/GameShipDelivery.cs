using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Linq;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _playerSprite;
        private OrthographicCamera _camera;
        private Vector2 _playerPosition;
        private Vector2 _playerRotation;
        private Vector2 _playerScale;
        private Vector2 _playerDirection;
        private float _playerSpeed = 90;

        public GameShipDelivery()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            _playerSprite = GenerateRandomTexture(GraphicsDevice, 16, 16);
            _playerPosition = new Vector2(50, 50);
            _playerDirection = new Vector2(_playerSpeed, _playerSpeed);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var elapsedSec = gameTime.GetElapsedSeconds();

            if(_playerPosition.X >= 320 - _playerSprite.Width)
            {
                _playerDirection.X = -_playerSpeed;
                _playerSprite = GenerateRandomTexture(GraphicsDevice, 16, 16);
            }
            else if (_playerPosition.X <= 0)
            {
                _playerDirection.X = _playerSpeed; _playerSprite = GenerateRandomTexture(GraphicsDevice, 16, 16);
            }

            if (_playerPosition.Y >= 180 - _playerSprite.Height)
            {
                _playerDirection.Y = -_playerSpeed; _playerSprite = GenerateRandomTexture(GraphicsDevice, 16, 16);
            }
            else if(_playerPosition.Y <= 0)
            {
                _playerDirection.Y = _playerSpeed; _playerSprite = GenerateRandomTexture(GraphicsDevice, 16, 16);
            }

            _playerPosition.X += _playerDirection.X * elapsedSec;
            _playerPosition.Y += _playerDirection.Y * elapsedSec;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            

            _spriteBatch.Draw(_playerSprite, _playerPosition, Color.White);
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
}
