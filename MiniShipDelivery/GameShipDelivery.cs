using Microsoft.Xna.Framework;
using MiniShipDelivery.Components;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Dialog;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Cursor;
using MiniShipDelivery.Components.Input;
using MiniShipDelivery.Components.Objects;
using MiniShipDelivery.Components.Persistence;
using MiniShipDelivery.Components.Sound;
using MiniShipDelivery.Components.World;

namespace MiniShipDelivery
{
    public class GameShipDelivery : Game
    {
        public GameShipDelivery()
        {
            GameSettingManager.LoadGameSetting();
            
            var graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GlobaleGameParameters.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = GlobaleGameParameters.PreferredBackBufferHeight;

            graphics.ApplyChanges();
            
            this.Content.RootDirectory = "Content";
            this.Window.Title = "Mini Ship Delivery";
            this.IsMouseVisible = false;
            
            var cameraManager = new CameraManager(this);
            cameraManager.UpdateOrder = 1;
            this.Components.Add(cameraManager);
            SimpleThinksHelper.CameraManagerInstance = cameraManager;

            // add all components
            var input = new InputManager(this);
            input.UpdateOrder = 2;
            this.Components.Add(input);
            HudHelper.Inputs = input.Inputs;
            
            // Sound
            var soundManager = new SoundManager(this);
            soundManager.UpdateOrder = 3;
            this.Components.Add(soundManager);

            // Map down layer
            var map = new WorldManager(this);
            map.UpdateOrder = 4;
            map.DrawOrder = 4;
            this.Components.Add(map);
            
            
            // Player
            var characterManager = new CharacterManager(
                this, 
                new Vector2(152f, 82f));
            characterManager.UpdateOrder = 5;
            characterManager.DrawOrder = 5;
            this.Components.Add(characterManager);
            
            // Map top layer
            var mapTopLayer = new WorldManagerTopLayer(this);
            mapTopLayer.UpdateOrder = 6;
            mapTopLayer.DrawOrder = 6;
            this.Components.Add(mapTopLayer);
            
            var colliderManager = new ColliderManager(this);
            colliderManager.UpdateOrder = 3;
            colliderManager.DrawOrder = 7;
            this.Components.Add(colliderManager);
            
            
            // Persistence
            var persistenceManager = new PersistenceManager(this);
            persistenceManager.UpdateOrder = 8;
            this.Components.Add(persistenceManager);
            
            var hudManager = new HudManager(this);
            hudManager.UpdateOrder = 9;
            hudManager.DrawOrder = 9;
            this.Components.Add(hudManager);

            var dialog = new DialogManager(this);
            dialog.UpdateOrder = 10;
            dialog.DrawOrder = 10;
            this.Components.Add(dialog);
            
            var cursor = new MouseManager(this);
            cursor.UpdateOrder = 11;
            cursor.DrawOrder = 100; // cursor must render at last
            this.Components.Add(cursor);
            
            // only for debug purpose
            var consoleManager = new ConsoleManager(this);
            consoleManager.UpdateOrder = 12;
            consoleManager.DrawOrder = 12;
            this.Components.Add(consoleManager);
            
            
            
            
            var fps = new FramesPerSecondManager(this);
            fps.UpdateOrder = 99;
            fps.DrawOrder = 99;
            this.Components.Add(fps);
            
            // TODO: sollte ich das nicht anders machen?
            PersistenceManager.LoadMap();
            
            // Music
            this.MusicManagerInstance = new MusicManager(this);
        }

        public MusicManager MusicManagerInstance { get; set; }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }
        
        
    }
}
