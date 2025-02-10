using Microsoft.Xna.Framework.Input;
using MiniShipDelivery.Components.Dialog;

namespace MiniShipDelivery.Components.Input;

public class InputTextController
{
    public readonly DialogState DialogState = new();
    
    public void Update()
    {
        if (!this.DialogState.DialogOn) return;
        
        if(!string.IsNullOrEmpty(this.DialogState.DialogLetter) &&
           this.DialogState.KeyIsPressed != Keys.Enter)
        {
            if(this.DialogState.KeyIsPressed == Keys.Back)
            {
                if(this.DialogState.TextPlayer.Length > 0)
                {
                    this.DialogState.TextPlayer = 
                        this.DialogState.TextPlayer.Remove(
                            this.DialogState.TextPlayer.Length - 1);
                    this.DialogState.DialogLetter = "";
                }
            }
            else
            {
                this.DialogState.TextPlayer += this.DialogState.DialogLetter;
                this.DialogState.DialogLetter = "";
            }
        }
        
        GlobaleGameParameters.DialogTextUser = this.DialogState.TextPlayer;
        
        if(!string.IsNullOrEmpty(this.DialogState.DialogLetter) &&
           this.DialogState.DialogLetter == "ENTER" &&
           this.DialogState.KeyIsPressed == Keys.Enter)
        {
            if(this.DialogState.TextPlayer == "EXIT")
            {
                this.DialogState.DialogExit = true;
            }

            switch (this.DialogState.TextPlayer)
            {
                case "HELLO":
                    this.DialogState.TextNpc = "Hello, how are you?";
                    break;
                case "GOOD":
                    this.DialogState.TextNpc = "I'm fine, thank you!";
                    break;
                case "BYE":
                    this.DialogState.TextNpc = "Goodbye!";
                    this.DialogState.DialogExit = true;
                    break;
            }
            
            this.DialogState.TextPlayer = string.Empty;
            this.DialogState.DialogLetter = ""; 
        }
        
        GlobaleGameParameters.DialogTextNpc = this.DialogState.TextNpc;

        if (!string.IsNullOrEmpty(this.DialogState.DialogLetter) &&
            this.DialogState.KeyIsPressed == Keys.Back &&
            this.DialogState.DialogLetter == "BACK")
        {
            this.DialogState.DialogLetter = "";
        }
    }
}