using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.Helpers;
using MiniShipDelivery.Components.HUD;
using MiniShipDelivery.Components.Input;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.GameDebug;

internal class ConsoleManager(Game game) : DrawableGameComponent(game)
{
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();
    private readonly CharacterManager _character = game.GetComponent<CharacterManager>();
    private readonly SpriteBatch _spriteBatch = new(game.GraphicsDevice);
    

    private readonly Vector2 _startPosition = new(3, GlobaleGameParameters.ScreenHeight - 43);
    
    private readonly SpriteFont _font = game.Content.Load<SpriteFont>("Fonts/BaseFont");

    private static StringBuilder TextToWrite { get; } = new();
    
    private readonly ApplicationBus Bus = ApplicationBus.Instance;


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        // if mouse out of window, no need to update
        if(this.Bus.Inputs.MousePosition.X < 0 || ApplicationBus.Instance.Inputs.MousePosition.Y < 0) return;
        if(this.Bus.Inputs.MousePosition.X > GlobaleGameParameters.ScreenWidth || 
           this.Bus.Inputs.MousePosition.Y > GlobaleGameParameters.ScreenHeight) return;
        
        AddText($"Mouse Pos.: {HudHelper.Vector2ToString(this.Bus.Inputs.MousePosition)}");
        AddText($"Char. Pos.: {HudHelper.Vector2ToString(this._character.Player.Collider.Position)}");
    }

    public override void Draw(GameTime gameTime)
    {
        //if(GlobaleGameParameters.HudView != HudOptionView.MapEditor) return;
        if (!GlobaleGameParameters.ShowConsoleWindow &&
            !GlobaleGameParameters.DebugMode) return;
        
        base.Draw(gameTime);

        this._spriteBatch.BeginWithCameraViewMatrix();

        this.DrawConsoleWindow();

        this._spriteBatch.DrawString(this._font,
            TextToWrite.ToString(),
            this._startPosition + this.Bus.Camera.GetPosition() + new Vector2(3, 5),
            Color.White,
            0f,
            new Vector2(0, 0),
            0.3f,
            SpriteEffects.None, 1);

        TextToWrite.Clear();

        this._spriteBatch.End();
    }

    public static void AddText(string text)
    {
        TextToWrite.AppendLine(text);
    }

    private void DrawConsoleWindow()
    {
        var pos = this._startPosition + this.Bus.Camera.GetPosition();
        
        // black console window
        this._spriteBatch.FillRectangle(
            pos,
            new SizeF(100, 40),
            Color.Black);
        this._spriteBatch.DrawRectangle(
            pos,
            new SizeF(100, 40),
            Color.DarkGray);
        this._spriteBatch.FillRectangle(
            pos + new Vector2(1, 1),
            new SizeF(98, 2),
            Color.Blue);
        this._spriteBatch.DrawLine(
            pos + new Vector2(0, 4),
            100f, 0,
            Color.DarkGray);
    }
}