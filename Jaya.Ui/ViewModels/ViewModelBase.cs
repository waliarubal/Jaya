using Jaya.Ui.Models;
using Jaya.Ui.Services;
using ReactiveUI;
using System.Reactive;

namespace Jaya.Ui.ViewModels
{
    public abstract class ViewModelBase : ModelBase
    {
        ReactiveCommand<CommandType, Unit> _simpleCommand;
        ReactiveCommand<object, Unit> _parameterizedCommand;

        protected T GetService<T>() where T : ServiceBase
        {
            return ServiceManager.Instance.Get<T>();
        }

        public ReactiveCommand<CommandType, Unit> SimpleCommand
        {
            get
            {
                if (_simpleCommand == null)
                    _simpleCommand = ReactiveCommand.Create<CommandType>(GetService<CommandService>().SimpleCommandAction);

                return _simpleCommand;
            }
        }

        public ReactiveCommand<object, Unit> ParameterizedCommand
        {
            get
            {
                if (_parameterizedCommand == null)
                    _parameterizedCommand = ReactiveCommand.Create<object>(GetService<CommandService>().ParameterizedCommandAction);

                return _parameterizedCommand;
            }
        }
    }
}
