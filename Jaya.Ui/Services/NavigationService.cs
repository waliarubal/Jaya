using Jaya.Shared;
using Jaya.Shared.Services;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public class NavigationService
    {
        readonly CommandService _commandService;
        readonly Stack<DirectoryChangedEventArgs> _backwardStack, _forwardStack;
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        RelayCommand _navigateBack, _navigateForward;
        DirectoryChangedEventArgs _directoryChangedArgs;

        public NavigationService(CommandService commandService)
        {
            _commandService = commandService;
            _backwardStack = new Stack<DirectoryChangedEventArgs>();
            _forwardStack = new Stack<DirectoryChangedEventArgs>();
            _onDirectoryChanged = commandService.EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
        }

        ~NavigationService()
        {
            _commandService.EventAggregator.UnSubscribe(_onDirectoryChanged);
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
