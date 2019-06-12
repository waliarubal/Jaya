using ReactiveUI;

namespace Jaya.Ui.Base
{
    abstract class NotificationBase : ReactiveObject
    {
        protected bool Set<T>(string propertyName, ref T backingField, T value, bool raiseNotification = true)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            if (value == null && backingField == null)
                return false;
            else if (value != null && value.Equals(backingField))
                return false;

            backingField = value;
            if (raiseNotification)
                this.RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
