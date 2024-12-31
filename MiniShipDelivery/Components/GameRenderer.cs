using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.World;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MiniShipDelivery.Components;

public class GameRenderer : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;

    private readonly HudManager _hudManager;
    private readonly OrthographicCamera _camera;
    private readonly MapManager _map;
    private readonly CharacterPlayer _player;
    private readonly List<CharacterNpc> _characterNpCs;
    private readonly int _screenWidth;
    private readonly int _screenHeight;
    
    public GameRenderer(Game game,
        HudManager hudManager,
        OrthographicCamera camera,
        MapManager map,
        CharacterPlayer player,
        List<CharacterNpc> characterNpCs,
        int screenWidth, int screenHeight) : base(game)
    {
        this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
        this._hudManager = hudManager;
        this._camera = camera;
        this._map = map;
        this._player = player;
        this._characterNpCs = characterNpCs;
        this._screenWidth = screenWidth;
        this._screenHeight = screenHeight;
    }

    public override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.CornflowerBlue);
        
        var transformMatrix = this._camera.GetViewMatrix();
        
        this._spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        
        this._map.Draw(this._spriteBatch);
        this._player.Draw(this._spriteBatch, gameTime);
        foreach (var npc in this._characterNpCs)
        {
            npc.Draw(this._spriteBatch, gameTime);
        }
        this._hudManager.Draw(this._spriteBatch, gameTime);
        
        this._spriteBatch.End();
    }
    
    public Vector2 GetCameraPosition() => this._camera.Position;

    public void AddPosition(Vector2 delta) => this._camera.Position += delta;
}