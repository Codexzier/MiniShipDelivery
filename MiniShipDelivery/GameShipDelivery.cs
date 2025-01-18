using Microsoft.Xna.Framework;
using MiniShipDelivery.Components;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.HUD.Cursor;
using MiniShipDelivery.Components.Objects;
using MiniShipDelivery.Components.Persistence;
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
            cameraManager.UpdateOrder = 0;
            this.Components.Add(cameraManager);
            SimpleThinksHelper.CameraManagerInstance = cameraManager;

            // add all components
            var input = new InputManager(this);
            input.UpdateOrder = 1;
            this.Components.Add(input);
            HudHelper.Inputs = input.Inputs;

            // Map
            var map = new WorldManager(this);
            map.UpdateOrder = 3;
            map.DrawOrder = 1;
            this.Components.Add(map);
            
            

            // Player
            var characterManager = new CharacterManager(
                this, 
                new Vector2(152f, 82f));
            characterManager.UpdateOrder = 3;
            characterManager.DrawOrder = 2;
            this.Components.Add(characterManager);
            
            var colliderManager = new ColliderManager(
                this,
                characterManager);
            colliderManager.UpdateOrder = 4;
            this.Components.Add(colliderManager);
            
            // Persistence
            var persistenceManager = new PersistenceManager(this);
            persistenceManager.UpdateOrder = 5;
            this.Components.Add(persistenceManager);
            
            var hudManager = new HudManager(this);
            hudManager.UpdateOrder = 6;
            hudManager.DrawOrder = 6;
            this.Components.Add(hudManager);

            var cursor = new MouseManager(this);
            cursor.UpdateOrder = 7;
            cursor.DrawOrder = 100; // cursor must render at last
            this.Components.Add(cursor);
            
            
            // only for debug purpose
            var consoleManager = new ConsoleManager(this);
            consoleManager.UpdateOrder = 8;
            consoleManager.DrawOrder = 7;
            this.Components.Add(consoleManager);
            
            // TODO: sollte ich das nicht anders machen?
            //PersistenceManager.LoadMap();
            
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
