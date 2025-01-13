using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MiniShipDelivery.Components.HUD.Controls;

public class ButtonSound(Game game)
{
    private readonly SoundEffect _soundEffectHover = game.Content.Load<SoundEffect>("Interface/rollover1");
    private readonly SoundEffect _soundEffectPressed = game.Content.Load<SoundEffect>("Interface/click3");
    private bool _isInRange;

    public void PlayHover(bool inRange)
    {
        if(!inRange && !this._isInRange)
        {
            return;
        }
        
        if (!inRange && this._isInRange)
        {
            this._isInRange = false;   
            return;
        }
        
        if (this._isInRange && inRange) return;
        
        this._isInRange = true;
        this._soundEffectHover.Play();
    }

    public void PlayPressed()
    {
        this._soundEffectPressed.Play();
    }
}