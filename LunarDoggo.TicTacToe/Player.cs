namespace TicTacToe
{
    public class Player
    {
        public Player(byte id, string display)
        {
            this.Display = display;
            this.Id = id;
        }

        public string Display { get; }
        public byte Id { get; }
    }
}
