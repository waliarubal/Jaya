using Jaya.Ui.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Jaya.Ui.Base
{
    public abstract class ViewModelBase : ModelBase
    {
        ReactiveCommand<CommandType, Unit> _simpleCommand;
        ReactiveCommand<object, Unit> _parameterizedCommand;

        protected ViewModelBase()
        {
            EventAggregator = GetService<CommandService>().EventAggregator;
        }

        #region properties

        protected EventAggregator EventAggregator { get; }

        public ReactiveCommand<CommandType, Unit> SimpleCommand
        {
            get
            {
                if (_simpleCommand == null)
                    _simpleCommand = ReactiveCommand.Create<CommandType>(SimpleCommandAction);

                return _simpleCommand;
            }
        }

        public ReactiveCommand<object, Unit> ParameterizedCommand
        {
            get
            {
                if (_parameterizedCommand == null)
                    _parameterizedCommand = ReactiveCommand.Create<object>(ParameterizedCommandAction);

                return _parameterizedCommand;
            }
        }

        #endregion

        protected T GetService<T>()
        {
            return ServiceLocator.Instance.GetService<T>();
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
