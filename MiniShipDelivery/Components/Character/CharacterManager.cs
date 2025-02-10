using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Dialog;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.Input;

namespace MiniShipDelivery.Components.Character;

public class CharacterManager : DrawableGameComponent
{
    private readonly CameraManager _camera;
    private readonly SpriteBatch _spriteBatch;
    
    public readonly List<CharacterNpc> CharacterNpCs = new ();
    public readonly CharacterPlayer Player;
    
    private readonly List<BaseCharacter> _drawableCharacters = new ();
    
    private ApplicationBus Bus => ApplicationBus.Instance;
    
    public CharacterManager(Game game,
        Vector2 screenPosition) : base(game)
    {
        this._camera = game.GetComponent<CameraManager>();
        this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
        
        var texturesCharacter = new SpriteCharacter(game);
        var texturesCharacterShadow = new SpriteCharacterShadow(game);
        var texturesEmote = new SpriteEmote(game);
        
        this.Player = new CharacterPlayer(
            texturesCharacter,
            texturesCharacterShadow,
            texturesEmote,
            screenPosition, 
            CharacterType.Men)
        {
            Direction = Vector2.Zero,
            Speed = 20,
            FramesPerSecond = 5
        };
        
        var characterNpc = new CharacterNpc(
            texturesCharacter, 
            texturesCharacterShadow,
            texturesEmote,
            new Vector2(40, 40), 
            CharacterType.Women)
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
        var delta = this.Player.GetScreenPosition() - this.Bus.Camera.GetPosition();
        //this._camera.Camera.Position += delta * 0.08f;
        this.Bus.Camera.AddPosition(delta * 0.08f);
        
        foreach (var npc in this._drawableCharacters)
        {
            npc.Update(gameTime);
        }
        this.Player.Update(gameTime);
        
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        this._spriteBatch.BeginWithCameraViewMatrix();
        foreach (var charToDraw in this._drawableCharacters.OrderBy(o => o.Collider.Position.Y))
        {
            charToDraw.Draw(this._spriteBatch);
            
            if(charToDraw.IsColliding)
            {
                charToDraw.DrawEmote(
                    this._spriteBatch, 
                    charToDraw.Collider.Position - new Vector2(0, 16));
            }
            
            GlobaleGameParameters.ShowDialogBox = charToDraw.IsColliding;
        }
        this._spriteBatch.End();
    }
}