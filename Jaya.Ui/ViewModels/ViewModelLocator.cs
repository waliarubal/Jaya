using Jaya.Ui.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Jaya.Ui.ViewModels
{
    public class ViewModelLocator : DynamicObject
    {
        readonly Dictionary<string, ViewModelBase> _viewModels;
        readonly string _namespaceFormat;

        public ViewModelLocator()
        {
            _namespaceFormat = "Jaya.Ui.ViewModels.{0}";
            _viewModels = new Dictionary<string, ViewModelBase>();
        }

        ~ViewModelLocator()
        {
            _viewModels.Clear();
        }

        ViewModelBase GetViewModel([CallerMemberName]string name = null)
        {
            if (_viewModels.ContainsKey(name))
                return _viewModels[name];

            var fullName = string.Format(_namespaceFormat, name);
            var type = Type.GetType(fullName);

            var viewModel = ServiceLocator.Instance.CreateInstance(type) as ViewModelBase;
            _viewModels.Add(name, viewModel);

            return viewModel;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetViewModel(binder.Name);
            return result != null;
        }
    }
}
