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
        private ComponentInput _input;
        private Vector2 _playerDirection;

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
                Speed = 40
            };

            _input = new ComponentInput(_player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var elapsedSec = gameTime.GetElapsedSeconds();

            

            _player.Position += _input.GetMovement() * elapsedSec;

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

    public class ComponentInput
    {
        private CharacterPlayer _player;

        public ComponentInput(CharacterPlayer player)
        {
            this._player = player;
        }

        public Vector2 GetMovement()
        {
            var movement = new Vector2(0, 0);

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                movement.Y = -1;
            }

            if(keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                movement.Y = 1;
            }

            if(keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                movement.X = -1;
            }

            if(keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                movement.X = 1;
            }

            return movement * _player.Speed;
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
            int y_charArt = 3;
            spriteBatch.Draw(Sprite, Position, new Rectangle((16 * 0) + 0, (16 * y_charArt) + y_charArt, 16, 16), Color.White);

            // pant
            int x_pant = 3;
            spriteBatch.Draw(Sprite, Position, new Rectangle((16 * x_pant) + x_pant, (16 * y_charArt) + y_charArt, 16, 16), Color.White);

            // shoe
            int x_shoe = 4;
            spriteBatch.Draw(Sprite, Position, new Rectangle((16 * x_shoe) + x_shoe, (16 * y_charArt) + y_charArt, 16, 16), Color.White);

            // shirt
            int x_shirt = 6;
            spriteBatch.Draw(Sprite, Position, new Rectangle((16 * x_shirt) + x_shirt, (16 * y_charArt) + y_charArt, 16, 16), Color.White);
        }
    }
}
