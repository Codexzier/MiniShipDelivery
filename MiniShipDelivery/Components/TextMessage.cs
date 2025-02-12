namespace MiniShipDelivery.Components;

public class TextMessage
{
    public string Text { get; set; } = string.Empty;
    public bool IsOn { get; set; }
    public bool HasPressedEnter { get; set; }
    public bool CanClearForNextMessage { get; set; }
    public bool CanLeave { get; set; }
}