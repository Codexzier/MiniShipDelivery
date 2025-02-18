﻿using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiniShipDelivery.Components.Input;

public class InputTextController
{
    private ApplicationBus Bus => ApplicationBus.Instance;
    
    public InputTextController(Game game)
    {
        game.Window.TextInput += this.TextInputEvent;
    }

    private void TextInputEvent(object sender, TextInputEventArgs e)
    {
        if(this.Bus.TextMessage.CanLeave) return;
        if(!this.Bus.TextMessage.IsOn) return;

        if (e.Key == Keys.Enter)
        {
            this.Bus.TextMessage.HasPressedEnter = true;
            return;
        }
        
        Debug.WriteLine($"Key: {e.Key}, Character: {e.Character}");
        
        if(e.Key == Keys.Back)
        {
            if(this.Bus.TextMessage.Text.Length > 0)
                this.Bus.TextMessage.Text = this.Bus.TextMessage.Text.Remove(this.Bus.TextMessage.Text.Length - 1);
            return;
        }

        if (this.Bus.TextMessage.CanClearForNextMessage)
        {
            this.Bus.TextMessage.Text = string.Empty;
            this.Bus.TextMessage.CanClearForNextMessage = false;
        }
        
        this.Bus.TextMessage.Text += AssetOfLetters
            .Letters
            .TryGetValue(e.Key, out string value)
            ? value : e.Character.ToString();

        if (this.Bus.TextMessage.Text.ToLower().Contains("ö"))
        {
            this.Bus.TextMessage.Text = this.Bus.TextMessage.Text.Replace("ö", "OE");
        }
        
        if (this.Bus.TextMessage.Text.ToLower().Contains("ä"))
        {
            this.Bus.TextMessage.Text = this.Bus.TextMessage.Text.Replace("ä", "AE");
        }
        
        if (this.Bus.TextMessage.Text.ToLower().Contains("ü"))
        {
            this.Bus.TextMessage.Text = this.Bus.TextMessage.Text.Replace("ü", "UE");
        }
        
        if (this.Bus.TextMessage.Text.ToLower().Contains("ß"))
        {
            this.Bus.TextMessage.Text = this.Bus.TextMessage.Text.Replace("ß", "SS");
        }
        
        
        
        Debug.WriteLine($"OutputText: {this.Bus.TextMessage.Text}");
    }
}