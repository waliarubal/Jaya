using Jaya.Shared.Base;
using System;
using System.Threading.Tasks;

namespace Jaya.Shared
{
    public class RelayCommand : CommandBase
    {
        readonly Action _action;

        public RelayCommand(Action action, bool isEnabled = true, bool isAsynchronous = false) : base(isEnabled, isAsynchronous)
        {
            _action = action;
        }

        public async override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = true;

            if (IsAsynchronous)
                await Task.Run(_action);
            else
                _action.Invoke();

            IsExecuting = false;
        }
    }

    public class RelayCommand<P> : CommandBase
    {
        readonly Action<P> _action;

        public RelayCommand(Action<P> action, bool isEnabled = true, bool isAsynchronous = false) : base(isEnabled, isAsynchronous)
        {
            _action = action;
        }

        public async override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = true;

            if (IsAsynchronous)
            {
                var argument = (P)parameter;
                await Task.Run(() => _action.Invoke(argument));
            }
            else
                _action.Invoke((P)parameter);

            IsExecuting = false;
        }
    }
}
