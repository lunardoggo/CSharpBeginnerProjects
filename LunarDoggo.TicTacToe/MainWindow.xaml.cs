using System.Windows.Controls;
using System.Windows;
using System.Linq;
using System;

namespace TicTacToe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ButtonTileMapping[] buttonTileMappings;
        private readonly GameState gameState;

        public MainWindow()
        {
            InitializeComponent();

            this.buttonTileMappings = new ButtonTileMapping[]
            {
                new ButtonTileMapping(this.btnTopLeft,      0, 0),
                new ButtonTileMapping(this.btnTopCenter,    1, 0),
                new ButtonTileMapping(this.btnTopRight,     2, 0),
                new ButtonTileMapping(this.btnMiddleLeft,   0, 1),
                new ButtonTileMapping(this.btnMiddleCenter, 1, 1),
                new ButtonTileMapping(this.btnMiddleRight,  2, 1),
                new ButtonTileMapping(this.btnBottomLeft,   0, 2),
                new ButtonTileMapping(this.btnBottomCenter, 1, 2),
                new ButtonTileMapping(this.btnBottomRight,  2, 2),
            };

            this.gameState = new GameState(this.buttonTileMappings);
            this.gameState.TileAlreadyOccupied += this.OnTileAlreadyOccupied;
            this.gameState.GameOverPlayerWon += this.OnPlayerWon;
            this.gameState.GameOverDraw += this.OnGameOverDraw;
            this.gameState.TilesReset += this.OnTilesReset;
            this.gameState.StartGame();

        }

        private void OnTileAlreadyOccupied(object sender, EventArgs e)
        {
            MessageBox.Show("This space is already occupied, please choose another one.", "Can't occupy", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void OnGameOverDraw(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("The game ended in a draw. Do you want to play again?", "GameOver", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            this.ProcessGameOverPlayerChoice(result);
        }

        private void OnPlayerWon(object sender, PlayerEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Player {e.Player.Id} won the game. Do you want to play again?", "GameOver", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            this.ProcessGameOverPlayerChoice(result);
        }

        private void ProcessGameOverPlayerChoice(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                this.gameState.StartGame();
            }
            else
            {
                this.Close();
            }
        }

        private void OnTilesReset(object sender, EventArgs e)
        {
            foreach(ButtonTileMapping mapping in this.buttonTileMappings)
            {
                mapping.Button.Content = "";
            }
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                ButtonTileMapping mapping = this.buttonTileMappings.Single(_mapping => _mapping.Button == button);
                this.gameState.OccupyTile(mapping);
            }
        }
    }
}
