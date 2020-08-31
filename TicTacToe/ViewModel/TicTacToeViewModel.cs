using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TicTacToe.Command;

namespace TicTacToe.ViewModel
{
    class TicTacToeViewModel : ViewModelBase
    {
        TicTacToeView view;
        string[,] ticTacToeGrid;
        Random rnd = new Random();
        public TicTacToeViewModel(TicTacToeView tttv)
        {
            view = tttv;
            ticTacToeGrid = new string[3, 3];
        }

        private ICommand fieldClick;
        public ICommand FieldClick
        {
            get
            {
                if (fieldClick == null)
                {
                    fieldClick = new RelayCommand(param => FieldClickExecute(param), param => CanFieldClickExecute(param));
                }
                return fieldClick;
            }
        }
        private void FieldClickExecute(object param)
        {            
            Button btn = (Button)param;
            int row = Grid.GetRow(btn);
            int column = Grid.GetColumn(btn);
            ticTacToeGrid[row - 1, column - 1] = "X";
            btn.Content = "X";

            if (CheckVictory())
            {
                if (MessageBox.Show("Player won!\nWould you like to play again?","Congratulations",MessageBoxButton.YesNo,MessageBoxImage.None) == MessageBoxResult.Yes)
                {
                    TicTacToeView newGame = new TicTacToeView();
                    newGame.Show();
                    view.Close();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            int rowO, columnO;
            do
            {
                rowO = rnd.Next(0, 3);
                columnO = rnd.Next(0, 3);
            } while (CheckFreeSpot(rowO,columnO));
            Button btn1 = (Button)GetGridElement(view.gameGrid, rowO+1, columnO+1);
            ticTacToeGrid[rowO, columnO] = "O";
            btn1.Content = "O";

            if (CheckVictory())
            {
                if (MessageBox.Show("Computer won!\nWould you like to play again?", "Game Over", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
                {
                    TicTacToeView newGame = new TicTacToeView();
                    newGame.Show();
                    view.Close();
                }
                else
                {
                    view.Close();
                }
            }

        }
        private bool CanFieldClickExecute(object param)
        {
            if (param == null) return true;

            Button btn = (Button)param;
            if (btn.Content == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckVictory()
        {
            if ((ticTacToeGrid[0, 0] == "X" && ticTacToeGrid[0, 1] == "X" && ticTacToeGrid[0, 2]=="X")
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

        public bool CheckFreeSpot(int row, int column)
        {
            if (ticTacToeGrid[row, column] != null)
                return true;
            else
                return false;
        }


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
    }
}
