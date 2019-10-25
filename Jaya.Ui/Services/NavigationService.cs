using Jaya.Shared;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using Jaya.Ui.ViewModels.Windows;
using Jaya.Ui.Views.Windows;
using System;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    [Export(nameof(NavigationService), typeof(IService))]
    [Shared]
    public sealed class NavigationService: IService
    {
        readonly CommandService _commandService;
        readonly Stack<DirectoryChangedEventArgs> _backwardStack, _forwardStack;
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        RelayCommand _navigateBack, _navigateForward;
        RelayCommand<WindowOptionsModel> _openWindow;
        DirectoryChangedEventArgs _directoryChangedArgs;

        [ImportingConstructor]
        public NavigationService([Import(nameof(CommandService))]IService commandService)
        {
            _commandService = commandService as CommandService;

            _backwardStack = new Stack<DirectoryChangedEventArgs>();
            _forwardStack = new Stack<DirectoryChangedEventArgs>();
            _onDirectoryChanged = _commandService.EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
        }

        ~NavigationService()
        {
            _commandService.EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public RelayCommand<WindowOptionsModel> OpenWindowCommand
        {
            get
            {
                if (_openWindow == null)
                    _openWindow = new RelayCommand<WindowOptionsModel>(OpenWindowCommandAction);

                return _openWindow;
            }
        }

        public RelayCommand NavigateBackCommand
        {
            get
            {
                if (_navigateBack == null)
                    _navigateBack = new RelayCommand(NavigateBack, false);

                return _navigateBack;
            }
        }

        public RelayCommand NavigateForwardCommand
        {
            get
            {
                if (_navigateForward == null)
                    _navigateForward = new RelayCommand(NavigateForward, false);

                return _navigateForward;
            }
        }

        async void OpenWindowCommandAction(WindowOptionsModel option)
        {
            var window = new HostView();

            var viewModel = window.DataContext as HostViewModel;
            viewModel.Option = option;

            window.Content = Activator.CreateInstance(option.ContentType);

            await window.ShowDialog(App.Lifetime.MainWindow);
        }

        void NavigateBack()
        {
            var item = _backwardStack.Pop();
            _forwardStack.Push(item);

            NavigateBackCommand.IsEnabled = _backwardStack.Count > 0;
            NavigateForwardCommand.IsEnabled = true;

            var args = item.Clone(NavigationDirection.Backward);
            _commandService.EventAggregator.Publish(args);
        }

        void NavigateForward()
        {
            var item = _forwardStack.Pop();
            _backwardStack.Push(item);

            NavigateBackCommand.IsEnabled = true;
            NavigateForwardCommand.IsEnabled = _forwardStack.Count > 0;

            var args = item.Clone(NavigationDirection.Forward);
            _commandService.EventAggregator.Publish(args);
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            if (args.Direction == NavigationDirection.Unknown)
            {
                _backwardStack.Push(args);
                NavigateBackCommand.IsEnabled = true;
            }

            _directoryChangedArgs = args;
        }
    }
}
