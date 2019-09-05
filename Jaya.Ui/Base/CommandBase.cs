using System;
using System.Windows.Input;

namespace Jaya.Ui.Base
{
    public abstract class CommandBase : ModelBase, ICommand
    {
        public event EventHandler CanExecuteChanged;

        #region properties

        public bool IsEnabled
        {
            get => Get<bool>();
            set
            {
                Set(value);
                RaiseCanExecuteChanged();
            }
        }

        public bool IsExecuting
        {
            get => Get<bool>();
            protected set
            {
                Set(value);
                RaiseCanExecuteChanged();
            }
        }

        #endregion

        private void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled && !IsExecuting;
        }

        public abstract void Execute(object parameter);
    }
}
