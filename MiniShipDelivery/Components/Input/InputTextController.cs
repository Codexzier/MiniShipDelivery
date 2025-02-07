using Microsoft.Xna.Framework.Input;

namespace MiniShipDelivery.Components.Input;

public class InputTextController
{
    public void Update()
    {
        if (!GlobaleGameParameters.DialogState.DialogOn) return;
        
        if(!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter) &&
           GlobaleGameParameters.DialogState.KeyIsPressed != Keys.Enter)
        {
            if(GlobaleGameParameters.DialogState.KeyIsPressed == Keys.Back)
            {
                if(GlobaleGameParameters.DialogState.TextPlayer.Length > 0)
                {
                    GlobaleGameParameters.DialogState.TextPlayer = 
                        GlobaleGameParameters.DialogState.TextPlayer.Remove(
                            GlobaleGameParameters.DialogState.TextPlayer.Length - 1);
                    GlobaleGameParameters.DialogState.DialogLetter = "";
                }
            }
            else
            {
                GlobaleGameParameters.DialogState.TextPlayer += GlobaleGameParameters.DialogState.DialogLetter;
                GlobaleGameParameters.DialogState.DialogLetter = "";
            }
        }
        
        if(!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter) &&
           GlobaleGameParameters.DialogState.DialogLetter == "ENTER" &&
           GlobaleGameParameters.DialogState.KeyIsPressed == Keys.Enter)
        {
            if(GlobaleGameParameters.DialogState.TextPlayer == "EXIT")
            {
                GlobaleGameParameters.DialogState.DialogExit = true;
            }

            switch (GlobaleGameParameters.DialogState.TextPlayer)
            {
                case "HELLO":
                    GlobaleGameParameters.DialogState.TextNpc = "Hello, how are you?";
                    break;
                case "GOOD":
                    GlobaleGameParameters.DialogState.TextNpc = "I'm fine, thank you!";
                    break;
                case "BYE":
                    GlobaleGameParameters.DialogState.TextNpc = "Goodbye!";
                    GlobaleGameParameters.DialogState.DialogExit = true;
                    break;
            }
            
            GlobaleGameParameters.DialogState.TextPlayer = string.Empty;
            GlobaleGameParameters.DialogState.DialogLetter = ""; 
        }

        if (!string.IsNullOrEmpty(GlobaleGameParameters.DialogState.DialogLetter) &&
            GlobaleGameParameters.DialogState.KeyIsPressed == Keys.Back &&
            GlobaleGameParameters.DialogState.DialogLetter == "BACK")
        {
            GlobaleGameParameters.DialogState.DialogLetter = "";
        }
    }
}