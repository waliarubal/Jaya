using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Jaya.Shared.Base
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        readonly Dictionary<string, object> _variables;
        public event PropertyChangedEventHandler PropertyChanged;

        protected ModelBase()
        {
            _variables = new Dictionary<string, object>();
        }

        ~ModelBase()
        {
            _variables.Clear();
        }

        protected T Get<T>([CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                return default;

            if (_variables.ContainsKey(propertyName))
                return (T)_variables[propertyName];

            return default;
        }

        protected bool Set<T>(T value, [CallerMemberName]string propertyName = null, bool raiseNotification = true)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            if (value == null)
                return false;
            else if (value != null && _variables.ContainsKey(propertyName) && (object)value == _variables[propertyName])
                return false;

            if (_variables.ContainsKey(propertyName))
                _variables[propertyName] = value;
            else
                _variables.Add(propertyName, value);

            if (raiseNotification)
                RaisePropertyChanged(propertyName);

            return true;
        }

        protected bool Set<T>(ref T backingField, T value, [CallerMemberName]string propertyName = null, bool raiseNotification = true)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            if (value == null && backingField == null)
                return false;
            else if (value != null && value.Equals(backingField))
                return false;

            backingField = value;
            if (raiseNotification)
                RaisePropertyChanged(propertyName);

            return true;
        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;

            if (string.IsNullOrEmpty(propertyName))
                return;

            handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
