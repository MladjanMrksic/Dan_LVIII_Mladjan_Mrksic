using System.Windows;
using TicTacToe.ViewModel;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeView.xaml
    /// </summary>
    public partial class TicTacToeView : Window
    {
        public TicTacToeView()
        {
            DataContext = new TicTacToeViewModel(this);
            InitializeComponent();
            
        }
    }
}
