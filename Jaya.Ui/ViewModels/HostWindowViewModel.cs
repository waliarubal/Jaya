using Avalonia.Controls;
using Jaya.Ui.Base;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class HostWindowViewModel: ViewModelBase
    {
        ICommand _closeCommand;

        public string Header
        {
            get => Get<string>();
            set => Set(value);
        }

        public object Child
        {
            get => Get<object>();
            set => Set(value);
        }

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand<Window>(CloseCommandAction);

                return _closeCommand;
            }
        }

        void CloseCommandAction(Window window)
        {
            window.Close();
        }
    }
}
