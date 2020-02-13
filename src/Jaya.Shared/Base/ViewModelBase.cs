using Avalonia.Controls;
using Avalonia.Threading;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Jaya.Shared.Base
{
    public abstract class ViewModelBase : ModelBase
    {
        ICommand _simpleCommand, _parameterizedCommand;
        EventAggregator _eventAggregator;

        protected ViewModelBase()
        {
            
        }

        #region properties

        public bool IsBusy
        {
            get => Get<bool>();
            protected set => Set(value);
        }

        protected bool IsDesignMode => Design.IsDesignMode;

        protected EventAggregator EventAggregator
        {
            get
            {
                if (IsDesignMode)
                    return default;

                if (_eventAggregator == null)
                    _eventAggregator = GetService<CommandService>().EventAggregator;

                return _eventAggregator;
            }
        }

        public ICommand SimpleCommand
        {
            get
            {
                if (_simpleCommand == null)
                    _simpleCommand = new RelayCommand<byte>(SimpleCommandAction);

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

        protected T GetService<T>() where T : class, IService
        {
            if (IsDesignMode)
                return default;

            return ServiceLocator.Instance.GetService<T>();
        }

        protected T GetProvider<T>() where T : class, IProviderService
        {
            if (IsDesignMode)
                return default;

            return ServiceLocator.Instance.GetProviderService<T>();
        }

        protected void Invoke(Action action)
        {
            var dispatcher = Dispatcher.UIThread;

            if (dispatcher.CheckAccess())
                action.Invoke();
            else
                dispatcher.InvokeAsync(action, DispatcherPriority.Layout);
        }

        void SimpleCommandAction(byte type)
        {
            EventAggregator?.Publish(type);
        }

        void ParameterizedCommandAction(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            var parameters = parameter as List<object>;
            if (parameters == null)
                throw new ArgumentException("Failed to parse command parameters.", nameof(parameter));

            var param = new KeyValuePair<byte, object>((byte)parameters[0], parameters[1]);
            EventAggregator?.Publish(param);
        }
    }
}
