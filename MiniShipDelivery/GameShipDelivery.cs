﻿using Microsoft.Xna.Framework;
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
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private AssetManager _spriteManager;
        private ColliderManager _colliderManager;
        private readonly List<CharacterNpc> _characterNpCs = new ();
        private CharacterPlayer _player;
        private EmoteManager _emote;
        private InputManager _input;
        private MapManager _map;
        private HudManager _hudManager;

        private readonly int _screenWidth = 320;
        private readonly int _screenHeight = 180;

        public GameShipDelivery()
        {
            this._graphics = new GraphicsDeviceManager(this);

            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this._graphics.PreferredBackBufferWidth = 1280;
            this._graphics.PreferredBackBufferHeight = 720;
            this._graphics.ApplyChanges();
            
            this._input = new InputManager( 
                this,
                this._screenWidth / (float)this._graphics.PreferredBackBufferWidth , 
                this._screenHeight / (float)this._graphics.PreferredBackBufferHeight);
            this._input.UpdateOrder = 1;
            this.Components.Add(this._input);
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

            this._spriteManager = new AssetManager(this.Content);
            this._emote = new EmoteManager(this._spriteManager);
            

            var characterNpc = new CharacterNpc(this._spriteManager, this._emote, new Vector2(20, 20), CharacterType.Women)
            {
                Direction = Vector2.Zero,
                Speed = 20,
                FramesPerSecond = 10                
            };
            this._characterNpCs.Add(characterNpc);

            var screenPosition = new Vector2(this._screenWidth / 2 - 8, this._screenHeight / 2);
            this._player = new CharacterPlayer(this._spriteManager, this._input, screenPosition, CharacterType.Men)
            {
                Direction = Vector2.Zero,
                Speed = 40,
                FramesPerSecond = 10
            };

            this._colliderManager = new ColliderManager();
            this._colliderManager.Add(this._player);
            foreach (var npc in this._characterNpCs)
            {
                this._colliderManager.Add(npc);
            }

            this._map = new MapManager(this._spriteManager);

            this._hudManager = new HudManager(
                this._spriteManager, 
                this._input, 
                this._player,
                this._characterNpCs,
                this._camera,
                this._screenWidth,
                this._screenHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (this._input.HasPressToClose())
            {
                this.Exit();
            }

            foreach (var npc in this._characterNpCs)
            {
                npc.Update(gameTime);
            }
            this._player.Update(gameTime);
            this._map.Update(gameTime);
            this._colliderManager.Update(gameTime);


            var delta = this._player.Collider.Position 
                         - this._camera.Position - new Vector2(158, 84);
            this._camera.Position += delta * 0.08f;
            
            this._hudManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            var transformMatrix = this._camera.GetViewMatrix();
            
            this._spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
            
            this._map.Draw(this._spriteBatch);
            foreach (var npc in this._characterNpCs)
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
