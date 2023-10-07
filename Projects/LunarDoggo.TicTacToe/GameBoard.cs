using System;

namespace TicTacToe
{
    /// <summary>
    /// Represents a <see cref="GameBoard"/> consisting of 9 tiles separated into three rows and columns
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// Raised when a player occupied an unoccupied tile
        /// </summary>
        public event EventHandler<PlayerTileEventArgs> PlayerOccupiedTile;

        //this is a multi-dimensional-array, the amount of "," between [] specifies how many dimensions
        //the array has: (dimensioncount) n = (commacount) c + 1
        private readonly byte[,] tiles = new byte[3, 3];

        /// <summary>
        /// Tries to occupy the specified tile for the specified player and returns whether it was successful
        /// </summary>
        public bool OccupyTile(Player player, int x, int y)
        {
            if (this.IsTileOccupied(x, y))
            {
                return false;
            }
            //multidimensional arrays are accessed like 1D-arrays, just with more parameters between the []
            //you can picture the accessor in this case as this.tiles[row, column]
            this.tiles[x, y] = player.Id;

            this.PlayerOccupiedTile?.Invoke(this, new PlayerTileEventArgs(player, x, y));
            return true;
        }

        /// <summary>
        /// Returns whether all tiles are occupied by any player
        /// </summary>
        public bool AreAllFieldsOccupied()
        {
            //with tiles.GetLength(0) we get the length of the first dimension of the tiles-2D-array
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                //with tiles.GetLength(1) we get the length of the second dimension of the tiles-2D-array
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (!this.IsTileOccupied(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns whether the tile with the specified position is already occupied by any player
        /// </summary>
        private bool IsTileOccupied(int x, int y)
        {
            return this.tiles[x, y] != 0;
        }

        /// <summary>
        /// Returns whether the specified player occupies three consecutive tiles in any position and therefore has won the game
        /// </summary>
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

        /// <summary>
        /// Returns whether the specified player occupies all three tiles of a row
        /// </summary>
        private bool HasWonRow(byte player, int rowIndex)
        {
            //the & operator is the binary-and-operator
            //here we use it to make the check if the player has occupied all tiles of the row a bit shorter
            //if the tiles of the row are occupied by: 1 0 1 (0 = none, 1 = player one), and the provided player is 1,
            //this will lead to (1 == (1 & 0 & 1)), which will be (1 == 0) which is false
            //if the tiles are occupied by: 1 1 1, the result will be: (1 == (1 & 1 & 1)) and therefore (1 == 1) which is true
            return player == (this.tiles[rowIndex, 0] & this.tiles[rowIndex, 1] & this.tiles[rowIndex, 2]);
        }

        /// <summary>
        /// Returns whether the specified player occupies all three tiles of a column
        /// </summary>
        private bool HasWonColumn(byte player, int columnIndex)
        {
            return player == (this.tiles[0, columnIndex] & this.tiles[1, columnIndex] & this.tiles[2, columnIndex]);
        }

        /// <summary>
        /// Returns whether the specified player won any of the two diagonals of the <see cref="GameBoard"/>
        /// </summary>
        private bool HasWonDiagonals(byte player)
        {
            return player == (this.tiles[0, 0] & this.tiles[1, 1] & this.tiles[2, 2])
                || player == (this.tiles[0, 2] & this.tiles[1, 1] & this.tiles[2, 0]);
        }

        /// <summary>
        /// Resets the tiles of the <see cref="GameBoard"/> to be unoccupied
        /// </summary>
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
