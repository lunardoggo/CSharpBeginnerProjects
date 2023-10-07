using System.Windows.Controls;

namespace TicTacToe
{
    /// <summary>
    /// Represents a mapping between a <see cref="System.Windows.Controls.Button"/> and a tile with the specified coordinates
    /// </summary>
    public class ButtonTileMapping
    {
        public ButtonTileMapping(Button button, int tileX, int tileY)
        {
            this.Button = button;
            this.TileX = tileX;
            this.TileY = tileY;
        }

        /// <summary>
        /// <see cref="System.Windows.Controls.Button"/> of the window
        /// </summary>
        public Button Button { get; }
        /// <summary>
        /// X-coordinate of the tile
        /// </summary>
        public int TileX { get; }
        /// <summary>
        /// Y-coordinate of the tile
        /// </summary>
        public int TileY { get; }
    }
}
