using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Assets;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD;

internal class ConsoleManager
{
    private readonly AssetManager _assetManager;
    private readonly InputManager _input;
    private readonly OrthographicCamera _camera;
    private readonly int _screenWidth;
    private readonly int _screenHeight;

    private readonly Vector2 _startPosition;
    private StringBuilder _stringBuilder = new ();

    public ConsoleManager(AssetManager assetManager, 
        InputManager input, 
        OrthographicCamera camera, 
        int screenWidth,
        int screenHeight)
    {
        this._assetManager = assetManager;
        this._input = input;
        this._camera = camera;
        this._screenWidth = screenWidth;
        this._screenHeight = screenHeight;
            
        this._startPosition = new Vector2(3, this._screenHeight - 43);
    }
        
    public void AddText(string text)
    {
        this._stringBuilder.AppendLine(text);
    }
        
    public void DrawText(SpriteBatch spriteBatch)
    {
        // black console window
        spriteBatch.FillRectangle(
            this._startPosition + this._camera.Position,
            new SizeF(100, 40),
            Color.Black);
        spriteBatch.DrawRectangle(
            this._startPosition + this._camera.Position,
            new SizeF(100, 40), 
            Color.DarkGray);
        spriteBatch.FillRectangle(
            this._startPosition + this._camera.Position + new Vector2(1, 1),
            new SizeF(98, 2),
            Color.Blue);
        spriteBatch.DrawLine(
            this._startPosition + this._camera.Position + new Vector2(0, 4), 
            100f, 0,
            Color.DarkGray
            );
        
        spriteBatch.DrawString(this._assetManager.Font,
            this._stringBuilder.ToString(),
            this._startPosition + this._camera.Position + new Vector2(3, 5),
            Color.White,
            0f,
            new Vector2(0, 0),
            0.3f,
            SpriteEffects.None, 1);

        this._stringBuilder.Clear();
    }

    
}