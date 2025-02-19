using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MiniShipDelivery.Components.Input;

public static class AssetOfLetters
{
    public static string ConvertUmlaute(string text)
    {
        if (text.ToLower().Contains("ö"))
        {
            text = text.Replace("ö", "OE");
            text = text.Replace("Ö", "OE");
        }
        
        if (text.ToLower().Contains("ä"))
        {
            text = text.Replace("ä", "AE");
            text = text.Replace("Ä", "AE");
        }
        
        if (text.ToLower().Contains("ü"))
        {
            text = text.Replace("ü", "UE");
            text = text.Replace("Ü", "UE");
        }
        
        if (text.ToLower().Contains("ß"))
        {
            text = text.Replace("ß", "SS");
        }
        return text;
    }
    
    public static readonly IDictionary<Keys, string> Letters = new Dictionary<Keys, string>
    {
        {  Keys.A, "A" },
        {  Keys.B, "B" },
        {  Keys.C, "C" },
        {  Keys.D, "D" },
        {  Keys.E, "E" },
        {  Keys.F, "F" },
        {  Keys.G, "G" },
        {  Keys.H, "H" },
        {  Keys.I, "I" },
        {  Keys.J, "J" },
        {  Keys.K, "K" },
        {  Keys.L, "L" },
        {  Keys.M, "M" },
        {  Keys.N, "N" },
        {  Keys.O, "O" },
        {  Keys.P, "P" },
        {  Keys.Q, "Q" },
        {  Keys.R, "R" },
        {  Keys.S, "S" },
        {  Keys.T, "T" },
        {  Keys.U, "U" },
        {  Keys.V, "V" },
        {  Keys.W, "W" },
        {  Keys.X, "X" },
        {  Keys.Y, "Y" },
        {  Keys.Z, "Z" },
        {  Keys.Space, " " },
        {  Keys.Back, "BACK" },
        {  Keys.Enter, "ENTER" },
            
        // numbers
        {  Keys.D0, "0" },
        {  Keys.D1, "1" },
        {  Keys.D2, "2" },
        {  Keys.D3, "3" },
        {  Keys.D4, "4" },
        {  Keys.D5, "5" },
        {  Keys.D6, "6" },
        {  Keys.D7, "7" },
        {  Keys.D8, "8" },
        {  Keys.D9, "9" },
        
        // { Keys.OemQuestion, "?"},
        // { Keys.None, ":"},
    };
}