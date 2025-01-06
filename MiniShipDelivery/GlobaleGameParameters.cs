﻿using MiniShipDelivery.Components.HUD;

namespace MiniShipDelivery;

public static class GlobaleGameParameters
{
    public const int ScreenWidth = 320;
    public const int ScreenHeight = 180;

    public const int PreferredBackBufferWidth = 1280;
    public const int PreferredBackBufferHeight = 720;

    public static HudOptionView HudView = HudOptionView.MainMenu;
}