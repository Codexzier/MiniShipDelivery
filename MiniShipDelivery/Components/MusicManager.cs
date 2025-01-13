using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace MiniShipDelivery.Components;

public class MusicManager 
{
    public MusicManager(Game game)
    {
        SongFlowingRocks = game.Content.Load<Song>("Music/Flowing Rocks");
        
        MediaPlayer.Play(SongFlowingRocks);
        MediaPlayer.IsRepeating = true;
        
        MediaPlayer.Volume = 0.2f;
    }

    public static Song SongFlowingRocks { get; set; }
}