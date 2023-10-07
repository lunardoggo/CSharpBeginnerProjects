namespace TicTacToe
{
    /// <summary>
    /// Representation of a player
    /// </summary>
    public class Player
    {
        public Player(byte id, string display)
        {
            this.Display = display;
            this.Id = id;
        }

        //For tic tac toe this is usually set to either "X" or "O" 
        /// <summary>
        /// Display name of the player
        /// </summary>
        public string Display { get; }
        /// <summary>
        /// Id-number of the player
        /// </summary>
        public byte Id { get; }
    }
}
