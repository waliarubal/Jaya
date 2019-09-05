using Jaya.Ui.Base;
using System;

namespace Jaya.Ui
{
    public class RelayCommand : CommandBase
    {
        readonly Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public override void Execute(object parameter)
        {
            if(!CanExecute(parameter))
                return;

            IsExecuting = !IsExecuting;
            _action.Invoke();
            IsExecuting = !IsExecuting;
        }
    }

    public class RelayCommand<P> : CommandBase
    {
        readonly Action<P> _action;

        public RelayCommand(Action<P> action)
        {
            _action = action;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = !IsExecuting;
            _action.Invoke((P)parameter);
            IsExecuting = !IsExecuting;
        }
    }
}
