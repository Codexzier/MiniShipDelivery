using Microsoft.Xna.Framework.Input;
using MiniShipDelivery.Components.Dialog;

namespace MiniShipDelivery.Components.Input;

public class InputTextController
{
    public DialogState dialogState = new();
    
    public void Update()
    {
        if (!this.dialogState.DialogOn) return;
        
        if(!string.IsNullOrEmpty(this.dialogState.DialogLetter) &&
           this.dialogState.KeyIsPressed != Keys.Enter)
        {
            if(this.dialogState.KeyIsPressed == Keys.Back)
            {
                if(this.dialogState.TextPlayer.Length > 0)
                {
                    this.dialogState.TextPlayer = 
                        this.dialogState.TextPlayer.Remove(
                            this.dialogState.TextPlayer.Length - 1);
                    this.dialogState.DialogLetter = "";
                }
            }
            else
            {
                this.dialogState.TextPlayer += this.dialogState.DialogLetter;
                this.dialogState.DialogLetter = "";
            }
        }
        
        GlobaleGameParameters.DialogTextUser = this.dialogState.TextPlayer;
        
        if(!string.IsNullOrEmpty(this.dialogState.DialogLetter) &&
           this.dialogState.DialogLetter == "ENTER" &&
           this.dialogState.KeyIsPressed == Keys.Enter)
        {
            if(this.dialogState.TextPlayer == "EXIT")
            {
                this.dialogState.DialogExit = true;
            }

            switch (this.dialogState.TextPlayer)
            {
                case "HELLO":
                    this.dialogState.TextNpc = "Hello, how are you?";
                    break;
                case "GOOD":
                    this.dialogState.TextNpc = "I'm fine, thank you!";
                    break;
                case "BYE":
                    this.dialogState.TextNpc = "Goodbye!";
                    this.dialogState.DialogExit = true;
                    break;
            }
            
            this.dialogState.TextPlayer = string.Empty;
            this.dialogState.DialogLetter = ""; 
        }
        
        GlobaleGameParameters.DialogTextNpc = this.dialogState.TextNpc;

        if (!string.IsNullOrEmpty(this.dialogState.DialogLetter) &&
            this.dialogState.KeyIsPressed == Keys.Back &&
            this.dialogState.DialogLetter == "BACK")
        {
            this.dialogState.DialogLetter = "";
        }
    }
}