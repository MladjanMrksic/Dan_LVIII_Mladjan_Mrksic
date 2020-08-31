using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TicTacToe.Command;

namespace TicTacToe.ViewModel
{
    class TicTacToeViewModel : ViewModelBase
    {
        #region Properties
        TicTacToeView view;
        string[,] ticTacToeGrid = new string[3, 3];
        Random rnd = new Random();
        bool playerWon = false;
        #endregion

        #region Constructors
        public TicTacToeViewModel(TicTacToeView tttv)
        {
            view = tttv;
            ticTacToeGrid = new string[3, 3];
        }
        #endregion

        #region Commands
        private ICommand fieldClick;
        public ICommand FieldClick
        {
            get
            {
                if (fieldClick == null) fieldClick = new RelayCommand(param => FieldClickExecute(param), param => CanFieldClickExecute(param));
                return fieldClick;
            }
        }
        /// <summary>
        /// When the any button is clicked, this part of the code gets executed
        /// </summary>
        /// <param name="param">Button which was clicked</param>
        private void FieldClickExecute(object param)
        {
            //Casting object as Button for easier manipulation
            Button btn = (Button)param;
            //Getting row and column of the clicked button
            int row = Grid.GetRow(btn);
            int column = Grid.GetColumn(btn);
            //Corresponding array item is marked as "X"
            ticTacToeGrid[row - 1, column - 1] = "X";
            //Button content is updated to "X"
            btn.Content = "X";
            //Calling method CheckVictory to check if the victory condition is met
            if (CheckVictory())
            {
                //If the victory condition is met, player get's to chose if they want to play again
                if (MessageBox.Show("Player won!\nWould you like to play again?", "Congratulations", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
                {
                    //If player wants to play again, a new view is opened while the current one is closed
                    playerWon = true;
                    TicTacToeView newGame = new TicTacToeView();
                    newGame.Show();
                    view.Close();
                }
                else
                {
                    //If the player doesn't want to play again, the program is terminated
                    playerWon = true;
                    view.Close();
                }
            }
            //Declaring variables for computer's turn
            int rowO, columnO;
            //Chosing row and column untill computer finds a pair which isn't already taken
            do
            {
                rowO = rnd.Next(0, 3);
                columnO = rnd.Next(0, 3);
            } while (CheckFreeSpot(rowO, columnO));
            //Getting information about the button at chosen row and column
            Button btn1 = (Button)GetGridElement(view.gameGrid, rowO + 1, columnO + 1);
            //Array item as well as button content are set to "O"
            ticTacToeGrid[rowO, columnO] = "O";
            btn1.Content = "O";
            //If the victory condition is met, player get's to chose if they want to play again
            if (CheckVictory() && playerWon == false)
            {     
                //If player wants to play again, a new view is opened while the current one is closed
                if (MessageBox.Show("Computer won!\nWould you like to play again?", "Game Over", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
                {
                    TicTacToeView newGame = new TicTacToeView();
                    newGame.Show();
                    view.Close();
                }
                //If the player doesn't want to play again, the program is terminate
                else view.Close();
            }

        }
        //Button press is only valid if the button wasn't already chosen
        private bool CanFieldClickExecute(object param)
        {
            if (param == null) return true;
            Button btn = (Button)param;
            if (btn.Content == null) return true;
            else return false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks if victory condition has been met
        /// </summary>
        /// <returns>True if victory is achieved, or false if it isn't</returns>
        public bool CheckVictory()
        {
            if ((ticTacToeGrid[0, 0] == "X" && ticTacToeGrid[0, 1] == "X" && ticTacToeGrid[0, 2] == "X")
                || (ticTacToeGrid[1, 0] == "X" && ticTacToeGrid[1, 1] == "X" && ticTacToeGrid[1, 2] == "X")
                || (ticTacToeGrid[2, 0] == "X" && ticTacToeGrid[2, 1] == "X" && ticTacToeGrid[2, 2] == "X")
                || (ticTacToeGrid[0, 0] == "O" && ticTacToeGrid[0, 1] == "O" && ticTacToeGrid[0, 2] == "O")
                || (ticTacToeGrid[1, 0] == "O" && ticTacToeGrid[1, 1] == "O" && ticTacToeGrid[1, 2] == "O")
                || (ticTacToeGrid[2, 0] == "O" && ticTacToeGrid[2, 1] == "O" && ticTacToeGrid[2, 2] == "O")
                || (ticTacToeGrid[0, 0] == "X" && ticTacToeGrid[1, 0] == "X" && ticTacToeGrid[2, 0] == "X")
                || (ticTacToeGrid[0, 1] == "X" && ticTacToeGrid[1, 1] == "X" && ticTacToeGrid[2, 1] == "X")
                || (ticTacToeGrid[0, 2] == "X" && ticTacToeGrid[1, 2] == "X" && ticTacToeGrid[2, 2] == "X")
                || (ticTacToeGrid[0, 0] == "O" && ticTacToeGrid[1, 0] == "O" && ticTacToeGrid[2, 0] == "O")
                || (ticTacToeGrid[0, 1] == "O" && ticTacToeGrid[1, 1] == "O" && ticTacToeGrid[2, 1] == "O")
                || (ticTacToeGrid[0, 2] == "O" && ticTacToeGrid[1, 2] == "O" && ticTacToeGrid[2, 2] == "O")
                || (ticTacToeGrid[0, 0] == "X" && ticTacToeGrid[1, 1] == "X" && ticTacToeGrid[2, 2] == "X")
                || (ticTacToeGrid[0, 2] == "X" && ticTacToeGrid[1, 1] == "X" && ticTacToeGrid[2, 0] == "X")
                || (ticTacToeGrid[0, 0] == "O" && ticTacToeGrid[1, 1] == "O" && ticTacToeGrid[2, 2] == "O")
                || (ticTacToeGrid[0, 2] == "O" && ticTacToeGrid[1, 1] == "O" && ticTacToeGrid[2, 0] == "O"))
                return true;
            else
                return false;
        }
        /// <summary>
        /// This method checks if a desired spot is taken
        /// </summary>
        /// <param name="row">Row in the array</param>
        /// <param name="column">Column in the array</param>
        /// <returns>True if the spot is taken, or false if it isn't</returns>
        public bool CheckFreeSpot(int row, int column)
        {
            if (ticTacToeGrid[row, column] != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Gets a specific element from the selected grid
        /// </summary>
        /// <param name="g">Grid from which we are retrieving elements</param>
        /// <param name="r">Row of the element we desire</param>
        /// <param name="c">Column of the element we desire</param>
        /// <returns>Returns element from the specified column and row</returns>
        public UIElement GetGridElement(Grid g, int r, int c)
        {
            for (int i = 0; i < g.Children.Count; i++)
            {
                UIElement e = g.Children[i];
                if (Grid.GetRow(e) == r && Grid.GetColumn(e) == c)
                    return e;
            }
            return null;
        }
        #endregion

    }
}
