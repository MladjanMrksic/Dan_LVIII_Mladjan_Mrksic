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
            view.lblStatus.Content = "Computer is thinking.";
            Thread.Sleep(500);
            view.lblStatus.Content = "Computer is thinking..";
            Thread.Sleep(500);
            view.lblStatus.Content = "Computer is thinking...";
            Thread.Sleep(500);
            do
            {

            } while ();
            
        }
        private bool CanFieldClickExecute(object param)
        {
            return true;
        }

        public bool Victory()
        {
            if ((ticTacToeGrid[0, 0] == ticTacToeGrid[0, 1] && ticTacToeGrid[0, 1] == ticTacToeGrid[0, 2])
                || (ticTacToeGrid[1, 0] == ticTacToeGrid[1, 1] && ticTacToeGrid[1, 1] == ticTacToeGrid[1, 2])
                || (ticTacToeGrid[2, 0] == ticTacToeGrid[2, 1] && ticTacToeGrid[2, 1] == ticTacToeGrid[2, 2])
                || (ticTacToeGrid[0, 0] == ticTacToeGrid[1, 0] && ticTacToeGrid[1, 0] == ticTacToeGrid[2, 0])
                || (ticTacToeGrid[0, 1] == ticTacToeGrid[1, 1] && ticTacToeGrid[1, 1] == ticTacToeGrid[2, 1])
                || (ticTacToeGrid[0, 2] == ticTacToeGrid[1, 2] && ticTacToeGrid[1, 2] == ticTacToeGrid[2, 2])
                || (ticTacToeGrid[0, 0] == ticTacToeGrid[1, 1] && ticTacToeGrid[1, 1] == ticTacToeGrid[2, 2])
                || (ticTacToeGrid[0, 2] == ticTacToeGrid[1, 1] && ticTacToeGrid[1, 1] == ticTacToeGrid[2, 0]))
                return true;
            else
                return false;
        }

        public bool FreeSpot(int row, int column)
        {
            if (ticTacToeGrid[row, column] != null)
                return false;
            else
                return true;
        }
    }
}
