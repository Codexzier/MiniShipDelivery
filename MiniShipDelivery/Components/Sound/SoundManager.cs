using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MiniShipDelivery.Components.Sound;

public class SoundManager(Game game) : GameComponent(game)
{
    private readonly SoundEffect _soundEffectHover = game.Content.Load<SoundEffect>("Interface/rollover1");
    private readonly SoundEffect _soundEffectSelected = game.Content.Load<SoundEffect>("Interface/click_002");
    private readonly SoundEffect _soundEffectPressed = game.Content.Load<SoundEffect>("Interface/click3");
    private readonly SoundEffect _soundEffectSwitch = game.Content.Load<SoundEffect>("Interface/click_005");
    
    private readonly IDictionary<string, bool> _hoverCallers = new Dictionary<string, bool>();

    public void PlayHover(bool inRange, string caller = "")
    {
        if(string.IsNullOrEmpty(caller)) return;
        
        if(this._hoverCallers.ContainsKey(caller) &&
           this._hoverCallers[caller] == inRange) return;
        
        if (!inRange &&
            this._hoverCallers.ContainsKey(caller) &&
            this._hoverCallers[caller])
        {   
            this._hoverCallers[caller] = false;
            return;
        }

        if (!this._hoverCallers.ContainsKey(caller))
        {
            this._hoverCallers.Add(caller, inRange);
        }
        
        this._hoverCallers[caller] = inRange;
        
        this._soundEffectHover.Play();
    }

    public void PlayPressed()
    {
        this._soundEffectPressed.Play();
    }
    
    public void PlaySelected()
    {
        this._soundEffectSelected.Play();
    }
    
    public void PlaySwitch()
    {
        this._soundEffectSwitch.Play();
    }
}