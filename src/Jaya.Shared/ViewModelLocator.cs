//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Avalonia;
using Avalonia.Controls;
using Jaya.Shared.Base;
using Jaya.Shared.Controls;
using System;
using System.Globalization;
using System.Reflection;

namespace Jaya.Shared
{
    public static class ViewModelLocator
    {
        public static readonly AvaloniaProperty AutoWireViewModelProperty;

        static ViewModelLocator()
        {
            AutoWireViewModelProperty = AvaloniaProperty.RegisterAttached<Control, bool>("AutoWireViewModel", typeof(ViewModelLocator), false);
            AutoWireViewModelProperty.Changed.Subscribe(args => AutoWireViewModelChanged(args?.Sender, args));
        }

        public static bool GetAutoWireViewModel(AvaloniaObject control)
        {
            return (bool)control.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(AvaloniaObject control, bool value)
        {
            control.SetValue(AutoWireViewModelProperty, value);
        }

        static void AutoWireViewModelChanged(AvaloniaObject control, AvaloniaPropertyChangedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            if (!(bool)e.NewValue)
                return;

            var view = control as Control;
            if (view == null)
                return;

            var viewType = control.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

            var windowType = typeof(StyledWindow);
            if (windowType.IsAssignableFrom(viewType))
                ThemeManager.Instance.EnableTheme(view as StyledWindow);

            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
            var viewModelType = Type.GetType(viewModelName);

            var viewModel = ServiceLocator.Instance.Container.GetService(viewModelType) as ViewModelBase;
            view.DataContext = viewModel;
            viewModel.IsLoaded = true;
        }
    }
}
