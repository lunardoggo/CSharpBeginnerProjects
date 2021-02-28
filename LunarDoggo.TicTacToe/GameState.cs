using System.Linq;
using System;

namespace TicTacToe
{
    public class GameState
    {
        public event EventHandler<PlayerEventArgs> GameOverPlayerWon;
        public event EventHandler<EventArgs> TileAlreadyOccupied;
        public event EventHandler<EventArgs> GameOverDraw;
        public event EventHandler<EventArgs> TilesReset;

        private readonly ButtonTileMapping[] buttonTileMappings;
        private readonly GameBoard gameBoard;
        private readonly Player secondPlayer;
        private readonly Player firstPlayer;

        private Player currentPlayer;

        public GameState(ButtonTileMapping[] buttonTileMappings)
        {
            this.buttonTileMappings = buttonTileMappings;
            this.firstPlayer = new Player(1, "X");
            this.secondPlayer = new Player(2, "O");
            this.gameBoard = new GameBoard();
            this.gameBoard.PlayerOccupiedTile += this.OnPlayerOccupiedTile;
        }

        private void OnPlayerOccupiedTile(object sender, PlayerTileEventArgs e)
        {
            this.buttonTileMappings.Single(_mapping => _mapping.TileX == e.TileX && _mapping.TileY == e.TileY).Button.Content = e.Player.Display;
        }

        public void StartGame()
        {
            this.currentPlayer = this.firstPlayer;
            this.gameBoard.Reset();
            this.TilesReset?.Invoke(this, EventArgs.Empty);
        }

        public void OccupyTile(ButtonTileMapping mapping)
        {
            if (this.gameBoard.OccupyTile(this.currentPlayer, mapping.TileX, mapping.TileY))
            {
                if(this.gameBoard.HasWon(this.currentPlayer.Id))
                {
                    this.GameOverPlayerWon?.Invoke(this, new PlayerEventArgs(this.currentPlayer));
                }
                else if(this.gameBoard.AreAllFieldsOccupied())
                {
                    this.GameOverDraw?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.SwitchPlayer();
                }
            }
            else
            {
                this.TileAlreadyOccupied?.Invoke(this, EventArgs.Empty);
            }
        }

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
