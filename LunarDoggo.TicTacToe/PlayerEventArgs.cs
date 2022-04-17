using System;

namespace TicTacToe
{
    /// <summary>
    /// This class is used for raising events regarding a player
    /// </summary>
    public class PlayerEventArgs : EventArgs
    {
        public PlayerEventArgs(Player player)
        {
            this.Player = player;
        }

        /// <summary>
        /// <see cref="Player"/> who caused the event to be raised
        /// </summary>
        public Player Player { get; }
    }
}
