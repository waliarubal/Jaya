using Avalonia.Threading;
using Jaya.Ui.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Jaya.Ui.Base
{
    public abstract class ViewModelBase : ModelBase
    {
        ICommand _simpleCommand, _parameterizedCommand;

        protected ViewModelBase()
        {
            EventAggregator = GetService<CommandService>().EventAggregator;
        }

        #region properties

        protected EventAggregator EventAggregator { get; }

        public ICommand SimpleCommand
        {
            get
            {
                if (_simpleCommand == null)
                    _simpleCommand = new RelayCommand<CommandType>(SimpleCommandAction);

                return _simpleCommand;
            }
        }

        public ICommand ParameterizedCommand
        {
            get
            {
                if (_parameterizedCommand == null)
                    _parameterizedCommand = new RelayCommand<object>(ParameterizedCommandAction);

                return _parameterizedCommand;
            }
        }

        #endregion

        protected T GetService<T>()
        {
            return ServiceLocator.Instance.GetService<T>();
        }

        protected void Invoke(Action action)
        {
            var dispatcher = Dispatcher.UIThread;

            if (dispatcher.CheckAccess())
                action.Invoke();
            else
                dispatcher.InvokeAsync(action, DispatcherPriority.Layout);
        }

        void SimpleCommandAction(CommandType type)
        {
            EventAggregator.Publish(type);
        }

        void ParameterizedCommandAction(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            var parameters = parameter as List<object>;
            if (parameters == null)
                throw new ArgumentException("Failed to parse command parameters.", nameof(parameter));

            var param = new KeyValuePair<CommandType, object>((CommandType)parameters[0], parameters[1]);
            EventAggregator.Publish(param);
        }
    }
}
