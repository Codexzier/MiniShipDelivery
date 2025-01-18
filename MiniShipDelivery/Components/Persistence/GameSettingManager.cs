using System;
using System.IO;

namespace MiniShipDelivery.Components.Persistence;

public static class GameSettingManager
{
    private static readonly string GameSettingFile = $"{Environment.CurrentDirectory}/gameSetting.json";
    
    public static GameSettingData GameSetting { get; set; } = new ();

    public static void LoadGameSetting()
    {
        if(!File.Exists(GameSettingFile)) return;
        
        var json = File.ReadAllText(GameSettingFile);
        GameSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<GameSettingData>(json);
        
        // set global parameters
        GlobaleGameParameters.DebugMode = GameSetting.DebugMode;
    }

    public static void SaveGameSetting()
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(GameSetting);
        File.WriteAllText(GameSettingFile, json);
    }
}