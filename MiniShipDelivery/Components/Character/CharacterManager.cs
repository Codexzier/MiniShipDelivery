using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.Character;

public class CharacterManager : DrawableGameComponent
{
    private readonly OrthographicCamera _camera;
    private readonly SpriteBatch _spriteBatch;
    
    public readonly List<CharacterNpc> CharacterNpCs = new ();
    public readonly CharacterPlayer Player;
    
    private readonly List<BaseCharacter> _drawableCharacters = new ();
    
    public CharacterManager(Game game,
        Vector2 screenPosition,
        AssetManager assetManager,
        InputManager input, OrthographicCamera camera) : base(game)
    {
        this._camera = camera;
        this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
        
        this.Player = new CharacterPlayer(assetManager, input, screenPosition, CharacterType.Men)
        {
            Direction = Vector2.Zero,
            Speed = 40,
            FramesPerSecond = 10
        };
        
        var characterNpc = new CharacterNpc(assetManager, new Vector2(20, 20), CharacterType.Women)
        {
            Direction = Vector2.Zero,
            Speed = 20,
            FramesPerSecond = 10                
        };
        this.CharacterNpCs.Add(characterNpc);
        
        this._drawableCharacters.Add(this.Player);
        this._drawableCharacters.Add(characterNpc);
    }

    public override void Update(GameTime gameTime)
    {
        var delta = this.Player.GetScreenPosition() - this._camera.Position;
        this._camera.Position += delta * 0.08f;
        
        // foreach (var npc in this._characterNpCs)
        // {
        //     //npc.Update(gameTime);
        // }
        this.Player.Update(gameTime);
        
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        this._spriteBatch.Begin(transformMatrix: this._camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
        foreach (var charToDraw in this._drawableCharacters.OrderBy(o => o.Collider.Position.Y))
        {
            charToDraw.Draw(this._spriteBatch);
        }
        this._spriteBatch.End();
    }
}