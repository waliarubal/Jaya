using Jaya.Ui.Models;
using ReactiveUI;
using System.Reactive;

namespace Jaya.Ui.ViewModels
{
    public abstract class ViewModelBase : ModelBase
    {
        ReactiveCommand<object, Unit> _sharedCommand;

        public ReactiveCommand<object, Unit> SharedCommand
        {
            get
            {
                if (_sharedCommand == null)
                    _sharedCommand = ReactiveCommand.Create<object>(Shared.Instance.SharedCommandAction);

                return _sharedCommand;
            }
        }
    }
}
