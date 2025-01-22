using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniShipDelivery.Components.HUD.Editor
{
    internal class MapEditorHud(Game game)
    {
        /// <summary>
        /// Top Menu bar
        /// </summary>
        private readonly MapEditorMenuCommon _mapEditorMenuCommon = new(game);

        /// <summary>
        /// right Side menu
        /// </summary>
        private readonly MapEditorMenu _mapEditorMenu = new(game);

        public void Update()
        {
            this._mapEditorMenuCommon.Update();
            this._mapEditorMenu.Update();
        }
        
        internal void Draw(SpriteBatch spriteBatch)
        {
            this._mapEditorMenuCommon.Draw(spriteBatch);
            this._mapEditorMenu.Draw(spriteBatch);
        }
    }
}