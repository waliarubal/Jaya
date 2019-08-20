using Jaya.Ui.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jaya.Ui.ViewModels
{
    public sealed class ViewModelLocator
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

        public ViewModelBase MainWindowViewModel => GetViewModel();

        public ViewModelBase MenuViewModel => GetViewModel();

        public ViewModelBase NavigationViewModel => GetViewModel();
    }
}
