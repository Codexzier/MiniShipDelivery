using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;

namespace MiniShipDelivery.Components.Character;

public class CharacterManager : GameComponent
{
    public readonly List<CharacterNpc> CharacterNpCs = new ();
    public readonly CharacterPlayer Player;
    
    private readonly List<BaseCharacter> _drawableCharacters = new ();
    
    public CharacterManager(
        Game game, 
        Vector2 screenPosition, 
        AssetManager assetManager, 
        InputManager input) : base(game)
    {
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
        // foreach (var npc in this._characterNpCs)
        // {
        //     //npc.Update(gameTime);
        // }
        this.Player.Update(gameTime);
        
        base.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var charToDraw in this._drawableCharacters.OrderBy(o => o.Collider.Position.Y))
        {
            charToDraw.Draw(spriteBatch);
        }
    }
}