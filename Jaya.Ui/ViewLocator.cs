using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Jaya.Ui.Base;
using System;

namespace Jaya.Ui
{
    class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
                return (Control)Activator.CreateInstance(type);
            else
                return new TextBlock { Text = string.Format("Not Found: {0}", name) };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}