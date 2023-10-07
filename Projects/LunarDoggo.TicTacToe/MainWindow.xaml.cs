using System.Windows.Controls;
using System.Windows;
using System.Linq;
using System;

namespace TicTacToe
{
    /// <summary>
    /// Interactionlogic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ButtonTileMapping[] buttonTileMappings;
        private readonly GameState gameState;

        public MainWindow()
        {
            InitializeComponent();

            this.buttonTileMappings = this.GetButtonTileMappings();

            this.gameState = new GameState(this.buttonTileMappings);
            //With the += operator you can subscribe to events, with -= you can unsubscribe
            //Make sure to unsubscribe from all events, when your object is no longer used in order to prevent "memory leaks"
            //because the garbage collector won't free the memory of the object because the event source still has a reference to it
            //In this case we don't need to unsubscribe any events because we never dispose of the GameState or GameBoard until the
            //application is terminated and therefore all resources are released
            this.gameState.TileAlreadyOccupied += this.OnTileAlreadyOccupied;
            this.gameState.GameOverPlayerWon += this.OnPlayerWon;
            this.gameState.GameOverDraw += this.OnGameOverDraw;
            this.gameState.TilesReset += this.OnTilesReset;
            this.gameState.StartGame();

        }

        /// <summary>
        /// Returns the mappings between the <see cref="Button"/>s of the UI and the tiles of the <see cref="GameBoard"/>
        /// </summary>
        private ButtonTileMapping[] GetButtonTileMappings()
        {
            return new ButtonTileMapping[]
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
        }

        //Event handlers always use the signature void <MethodName>(object sender, <EventArgs or derived class> e)
        /// <summary>
        /// Handles the <see cref="GameState.PlayerOccupiedTile"/> event and displays a message that the user can't occupy the clicked tile
        /// </summary>
        private void OnTileAlreadyOccupied(object sender, EventArgs e)
        {
            //This shows a default windows messagebox, you have to provide at least the first parameter
            //Parameter 1 (required): displayed message
            //Parameter 2 (optional): caption/title of the MessageBox
            //Parameter 3 (optional): which buttons should be added to the MessageBox (Ok, YesNo, YesNoCancel, ...)
            //Parameter 4 (optional): which icon should be displayed and which sound shoulc be played when the MessageBox is shown
            //Parameter 5 (optinoal): which button should be the predefined default button
            MessageBox.Show("This space is already occupied, please choose another one.", "Can't occupy", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// Handles the <see cref="GameState.GameOverDraw"/> event and displays that no player has won. Afterwards the players are asked, if
        /// they want to play again
        /// </summary>
        private void OnGameOverDraw(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("The game ended in a draw. Do you want to play again?", "GameOver", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            this.ProcessGameOverPlayerChoice(result);
        }

        /// <summary>
        /// Handles the <see cref="GameState.GameOverPlayerWon"/> event and displays that the player specified in
        /// <see cref="PlayerEventArgs.Player"/> has won. Afterwards the players are asked, if they want to play again
        /// </summary>
        private void OnPlayerWon(object sender, PlayerEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Player {e.Player.Id} won the game. Do you want to play again?", "GameOver", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            this.ProcessGameOverPlayerChoice(result);
        }

        /// <summary>
        /// Processes the play-again-prompt-result. If <paramref name="result"/> is <see cref="MessageBoxResult.Yes"/> the game is restarted,
        /// otherwise the window and therefore the application is closed
        /// </summary>
        private void ProcessGameOverPlayerChoice(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                this.gameState.StartGame();
            }
            else
            {
                //Closes the current window.
                //If there is no open Window and (Application.Current.ShutdownMode == ShutdownMode.OnLastWindowClose)
                //      or the main window is closed (Application.Current.ShutdownMode == ShutdownMode.OnMainWindowClose)
                //the application will be terminated as well
                //If (Application.Current.ShutdownMode == ShutdownMode.OnExplicitShutdown) you have to call Application.Current.Shutdown() yourself
                this.Close();
            }
        }

        /// <summary>
        /// Handled the <see cref="GameState.TilesReset"/> event and resets the buttons to their blank starting state
        /// </summary>
        private void OnTilesReset(object sender, EventArgs e)
        {
            foreach (ButtonTileMapping mapping in this.buttonTileMappings)
            {
                mapping.Button.Content = "";
            }
        }

        /// <summary>
        /// Handles the click event of the buttons of the window
        /// </summary>
        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            //Here we use a "Type pattern" (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/is#type-pattern)
            //(sender is Button) checks if the variable "sender" is of type Button
            //if this is the case, sender will be assigned to the variable "Button" which can be used in the scope of the if-statement
            if (sender is Button button)
            {
                //get the ButtonTileMapping for the clicked Button
                ButtonTileMapping mapping = this.buttonTileMappings.Single(_mapping => _mapping.Button == button);
                //Tells the GameState to occupy the tile of the mapping
                this.gameState.OccupyTile(mapping);
            }
        }
    }
}
