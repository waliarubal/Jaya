using Jaya.Ui.Models;
using ReactiveUI;
using System.Reactive;

namespace Jaya.Ui.ViewModels
{
    public abstract class ViewModelBase : ModelBase
    {
        ReactiveCommand<CommandType, Unit> _simpleCommand;
        ReactiveCommand<object, Unit> _parameterizedCommand;

        public ReactiveCommand<CommandType, Unit> SimpleCommand
        {
            get
            {
                if (_simpleCommand == null)
                    _simpleCommand = ReactiveCommand.Create<CommandType>(Shared.Instance.SimpleCommandAction);

                return _simpleCommand;
            }
        }

        public ReactiveCommand<object, Unit> ParameterizedCommand
        {
            get
            {
                if (_parameterizedCommand == null)
                    _parameterizedCommand = ReactiveCommand.Create<object>(Shared.Instance.ParameterizedCommandAction);

                return _parameterizedCommand;
            }
        }
    }
}
