using System;

namespace TicTacToe
{
    public class PlayerEventArgs : EventArgs
    {
        public PlayerEventArgs(Player player)
        {
            this.Player = player;
        }

        public Player Player { get; }
    }
}
