using Jaya.Ui.Base;
using System;

namespace Jaya.Ui
{
    public class RelayCommand<T> : CommandBase where T : class
    {
        readonly Action<T> _action;

        public RelayCommand(Action<T> action)
        {
            _action = action;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = !IsExecuting;
            _action.Invoke(parameter as T);
            IsExecuting = !IsExecuting;
        }
    }
}
