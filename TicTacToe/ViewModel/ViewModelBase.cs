using System.ComponentModel;

namespace TicTacToe.ViewModel
{
    /// <summary>
    /// This class serves as base class for all classes in ViewModel folder
    /// It implements OnPropertyChanged method which we use to track changes on the UI
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
