using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.GameMenuQuest;

public class GameMenuQuestManager(Game game)
{
    private readonly GameMenuQuestLog _gameMenuQuestLog = new(game);
    
    public static bool Show { get; set; }

    public void Update()
    {
        if(!Show) return;
        
        this._gameMenuQuestLog.Update();
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if(!Show) return;
        
        this._gameMenuQuestLog.Draw(spriteBatch);
    }

    
}