using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.GameMenuQuest;

public class GameMenuQuestManager
{
    private readonly GameMenuQuestLog _gameMenuQuestLog;
    
    public static bool Show { get; set; }
    
    public GameMenuQuestManager(Game game)
    {
        this._gameMenuQuestLog = new GameMenuQuestLog(game);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!Show) return;
        
        this._gameMenuQuestLog.Draw(spriteBatch);
    }
}