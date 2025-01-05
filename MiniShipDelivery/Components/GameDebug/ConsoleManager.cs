using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniShipDelivery.Components.Character;
using MiniShipDelivery.Components.GameDebug;
using MiniShipDelivery.Components.Helpers;
using MonoGame.Extended;

namespace MiniShipDelivery.Components.HUD.Helpers;

internal class ConsoleManager(
    Game game,
    int screenHeight) : DrawableGameComponent(game)
{
    private readonly CameraManager _camera = game.GetComponent<CameraManager>();
    private readonly InputManager _input = game.GetComponent<InputManager>();
    private readonly CharacterManager _character = game.GetComponent<CharacterManager>();
    private readonly SpriteBatch _spriteBatch = new(game.GraphicsDevice);
    

    private readonly Vector2 _startPosition = new(3, screenHeight - 43);
    private readonly StringBuilder _stringBuilder = new();
    private readonly SpriteFont _font = game.Content.Load<SpriteFont>("Fonts/BaseFont");

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        this.AddText($"{DateTime.Now:f}");
        this.AddText($"Mouse Pos.: {HudHelper.Vector2ToString(this._input.Inputs.MousePosition)}");
        this.AddText($"Char. Pos.: {HudHelper.Vector2ToString(this._character.Player.Collider.Position)}");

        foreach (var charNpc in this._character.CharacterNpCs)
        {
            this.AddText($"NPC: {charNpc.Collider.Position}");
        }
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
       
        
        this._spriteBatch.Begin(
            transformMatrix: this._camera.Camera.GetViewMatrix(), 
            samplerState: SamplerState.PointClamp);
        
        this.DrawConsoleWindow();
        
        this._spriteBatch.DrawString(this._font,
            this._stringBuilder.ToString(),
            this._startPosition + this._camera.Camera.Position + new Vector2(3, 5),
            Color.White,
            0f,
            new Vector2(0, 0),
            0.3f,
            SpriteEffects.None, 1);

        this._stringBuilder.Clear();
        
        this._spriteBatch.End();
    }

    private void AddText(string text)
    {
        this._stringBuilder.AppendLine(text);
    }

    private void DrawConsoleWindow()
    {
        // black console window
        this._spriteBatch.FillRectangle(
            this._startPosition + this._camera.Camera.Position,
            new SizeF(100, 40),
            Color.Black);
        this._spriteBatch.DrawRectangle(
            this._startPosition + this._camera.Camera.Position,
            new SizeF(100, 40),
            Color.DarkGray);
        this._spriteBatch.FillRectangle(
            this._startPosition + this._camera.Camera.Position + new Vector2(1, 1),
            new SizeF(98, 2),
            Color.Blue);
        this._spriteBatch.DrawLine(
            this._startPosition + this._camera.Camera.Position + new Vector2(0, 4),
            100f, 0,
            Color.DarkGray);
    }
}