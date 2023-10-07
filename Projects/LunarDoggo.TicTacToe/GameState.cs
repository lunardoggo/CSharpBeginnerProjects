using System.Linq;
using System;

namespace TicTacToe
{
    /// <summary>
    /// Represents the current state of the game
    /// </summary>
    public class GameState
    {
        //EventHandlers always use the syntyx event EventHandler<EventArgs or derived type> <Event name>
        //the event operator makes sure, that a class can only handle its own subscriptions to the event and
        //can't interfere with the subscriptions of other classes

        /// <summary>
        /// Raised when a player won the game
        /// </summary>
        public event EventHandler<PlayerEventArgs> GameOverPlayerWon;
        /// <summary>
        /// Raised when a player tried to occupy an already occupied tile
        /// </summary>
        public event EventHandler<EventArgs> TileAlreadyOccupied;
        /// <summary>
        /// Raised when the game ended in a draw
        /// </summary>
        public event EventHandler<EventArgs> GameOverDraw;
        /// <summary>
        /// Raised when all tiles were reset
        /// </summary>
        public event EventHandler<EventArgs> TilesReset;

        private readonly ButtonTileMapping[] buttonTileMappings;
        private readonly GameBoard gameBoard;
        private readonly Player secondPlayer;
        private readonly Player firstPlayer;

        //Varialbe to switch between the two players
        private Player currentPlayer;

        public GameState(ButtonTileMapping[] buttonTileMappings)
        {
            this.buttonTileMappings = buttonTileMappings;
            //The first player will be displayed as "X" on the tiles, the second as "O"
            this.firstPlayer = new Player(1, "X");
            this.secondPlayer = new Player(2, "O");
            this.gameBoard = new GameBoard();
            this.gameBoard.PlayerOccupiedTile += this.OnPlayerOccupiedTile;
        }

        /// <summary>
        /// Handles <see cref="GameBoard.PlayerOccupiedTile"/>
        /// </summary>
        private void OnPlayerOccupiedTile(object sender, PlayerTileEventArgs e)
        {
            //We just set the Content-display of the button of the tile to the current players displayname
            this.buttonTileMappings.Single(_mapping => _mapping.TileX == e.TileX && _mapping.TileY == e.TileY).Button.Content = e.Player.Display;
        }

        /// <summary>
        /// Resets all tiles and sets the current player to the first one
        /// </summary>
        public void StartGame()
        {
            this.currentPlayer = this.firstPlayer;
            this.gameBoard.Reset();
            this.TilesReset?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Tells the <see cref="GameBoard"/> to occupy a specific tile for the <see cref="GameState.currentPlayer"/>
        /// </summary>
        /// <param name="mapping"></param>
        public void OccupyTile(ButtonTileMapping mapping)
        {
            if (this.gameBoard.OccupyTile(this.currentPlayer, mapping.TileX, mapping.TileY))
            {
                //If the tile of the gameboard can be occupied, check if the game is either won
                //or ended in a draw. If neither is the case, switch the player
                if (this.gameBoard.HasWon(this.currentPlayer.Id))
                {
                    //Events could be raised by (in this case) calling this.GameOverPlayerWon(this, new PlayerEventArgs(this.currentPlayer))
                    //But if there aren't any handlers subscribed to it, this will lead to a NullReferenceException
                    //To prevent this, use the null-conditional-operator with a call to the Invoke-Method for the event
                    //this is equivalent to: 
                    // if(this.GameOverPlayerWon != null) { this.GameOverPlayerWon(this, new PlayerEventArgs(this.currentPlayer)); }
                    this.GameOverPlayerWon?.Invoke(this, new PlayerEventArgs(this.currentPlayer));
                }
                else if (this.gameBoard.AreAllFieldsOccupied())
                {
                    //If you want to raise an event, that uses the regular EventArgs as an eventtype, 
                    //you can use EventArgs.Empty instead of new EventArgs()
                    this.GameOverDraw?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.SwitchPlayer();
                }
            }
            else
            {
                //If the tile can't be occupied, it must already be occupied by either player
                this.TileAlreadyOccupied?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Switches between player one and two
        /// </summary>
        private void SwitchPlayer()
        {
            if (this.currentPlayer == this.firstPlayer)
            {
                this.currentPlayer = this.secondPlayer;
            }
            else
            {
                this.currentPlayer = this.firstPlayer;
            }
        }
    }
}
