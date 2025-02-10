using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using MiniShipDelivery.Components.Persistence;

namespace MiniShipDelivery.Components.Sound;

public class MusicManager 
{
    public MusicManager(Game game)
    {
        SongFlowingRocks = game.Content.Load<Song>("Music/Flowing Rocks");
        
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.2f;
        
        if(GameSettingManager.GameSetting.MusicOn)
        {
            MediaPlayer.Play(SongFlowingRocks);
        }
    }

    public static Song SongFlowingRocks { get; set; }
}