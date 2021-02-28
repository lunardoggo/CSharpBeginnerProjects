using System.Windows.Controls;

namespace TicTacToe
{
    public class ButtonTileMapping
    {
        public ButtonTileMapping(Button button, int tileX, int tileY)
        {
            this.Button = button;
            this.TileX = tileX;
            this.TileY = tileY;
        }

        public Button Button { get; }
        public int TileX { get; }
        public int TileY { get; }
    }
}
