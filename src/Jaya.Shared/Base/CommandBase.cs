using System;
using System.Windows.Input;

namespace Jaya.Shared.Base
{
    public abstract class CommandBase : ModelBase, ICommand
    {
        public event EventHandler CanExecuteChanged;

        protected CommandBase(bool isEnabled, bool isAsynchronous)
        {
            IsEnabled = isEnabled;
            IsAsynchronous = isAsynchronous;
        }

        #region properties

        public bool IsEnabled
        {
            get => Get<bool>();
            set
            {
                if (Set(value))
                    RaiseCanExecuteChanged();
            }
        }

        public bool IsExecuting
        {
            get => Get<bool>();
            protected set
            {
                if (Set(value))
                    RaiseCanExecuteChanged();
            }
        }

        public bool IsAsynchronous { get; set; }

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
