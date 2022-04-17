using System;

namespace TicTacToe
{
    /// <summary>
    /// This class is used for raising events regarding a player and a specific tile on the <see cref="GameBoard"/>
    /// </summary>
    public class PlayerTileEventArgs : EventArgs
    {
        public PlayerTileEventArgs(Player player, int tileX, int tileY)
        {
            this.Player = player;
            this.TileX = tileX;
            this.TileY = tileY;
        }

        /// <summary>
        /// <see cref="Player"/> who caused the event to be raised
        /// </summary>
        public Player Player { get; }
        /// <summary>
        /// X coordinate of the events tile
        /// </summary>
        public int TileX { get; }
        /// <summary>
        /// Y coordinate of the events tile
        /// </summary>
        public int TileY { get; }
    }
}
