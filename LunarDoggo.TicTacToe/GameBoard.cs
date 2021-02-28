using System;

namespace TicTacToe
{
    public class GameBoard
    {
        public event EventHandler<PlayerTileEventArgs> PlayerOccupiedTile;

        private readonly byte[,] tiles = new byte[3, 3];

        public bool OccupyTile(Player player, int x, int y)
        {
            if(this.IsTileOccupied(x, y))
            {
                return false;
            }
            this.tiles[x, y] = player.Id;

            this.PlayerOccupiedTile?.Invoke(this, new PlayerTileEventArgs(player, x, y));
            return true;
        }

        public bool AreAllFieldsOccupied()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if(!this.IsTileOccupied(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsTileOccupied(int x, int y)
        {
            return this.tiles[x, y] != 0;
        }

        public bool HasWon(byte player)
        {
            for (int i = 0; i < this.tiles.GetLength(0); i++)
            {
                if (this.HasWonRow(player, i) || this.HasWonColumn(player, i))
                {
                    return true;
                }
            }
            return this.HasWonDiagonals(player);
        }

        private bool HasWonRow(byte player, int rowIndex)
        {
            return player == (this.tiles[rowIndex, 0] & this.tiles[rowIndex, 1] & this.tiles[rowIndex, 2]);
        }

        private bool HasWonColumn(byte player, int columnIndex)
        {
            return player == (this.tiles[0, columnIndex] & this.tiles[1, columnIndex] & this.tiles[2, columnIndex]);
        }

        private bool HasWonDiagonals(byte player)
        {
            return player == (this.tiles[0, 0] & this.tiles[1, 1] & this.tiles[2, 2])
                || player == (this.tiles[0, 2] & this.tiles[1, 1] & this.tiles[2, 0]);
        }

        public void Reset()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    this.tiles[x, y] = 0;
                }
            }
        }
    }
}
