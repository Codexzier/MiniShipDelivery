using MiniShipDelivery.Components.Dialog;
using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery;

public static class GlobaleGameParameters
{
    public const int ScreenWidth = 320;
    public const int ScreenHeight = 180;

    public const int ScreenWidthHalf = ScreenWidth / 2;
    public const int ScreenHeightHalf = ScreenHeight / 2;

    public const int PreferredBackBufferWidth = 1280;
    public const int PreferredBackBufferHeight = 720;

    public static HudOptionView HudView = HudOptionView.MainMenu;
    
    public static bool ShowGrid = false;
    public static bool ShowConsoleWindow = false;
    public static bool DebugMode = false;
    
    public static bool ShowDialogBox = false;
    public static bool SystemDialogBox = false;
    public static readonly DialogState DialogState = new ();
}