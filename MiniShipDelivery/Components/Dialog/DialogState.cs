using Microsoft.Xna.Framework.Input;

namespace MiniShipDelivery.Components.Dialog;

public class DialogState
{
    public string TextNpc { get; set; } = "";
    public string TextPlayer { get; set; } = "";
    public bool DialogOn { get; set; }
    public string DialogLetter { get; set; }
    public bool LetterIsPressed { get; set; }
    public Keys KeyIsPressed { get; set; }
    public bool DialogExit { get; set; }
}