using System;

namespace TicTacToe
{
    public class PlayerTileEventArgs : EventArgs
    {
        public PlayerTileEventArgs(Player player, int tileX, int tileY)
        {
            this.Player = player;
            this.TileX = tileX;
            this.TileY = tileY;
        }

        public Player Player { get; }
        public int TileX { get; }
        public int TileY { get; }
    }
}
