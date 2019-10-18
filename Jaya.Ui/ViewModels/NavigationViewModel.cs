using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        readonly ConfigurationService _configService;
        ICommand _populateCommand;

        public NavigationViewModel()
        {
            _configService = GetService<ConfigurationService>();

            Node = new TreeNodeModel(null, null);
            PopulateCommand.Execute(Node);
        }

        #region properties

        public ICommand PopulateCommand
        {
            get
            {
                if (_populateCommand == null)
                    _populateCommand = new RelayCommand<TreeNodeModel>(Populate);

                return _populateCommand;
            }
        }

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => _configService.ApplicationConfiguration;

        public TreeNodeModel Node { get; }

        public TreeNodeModel SelectedNode
        {
            get => Get<TreeNodeModel>();
            set
            {
                Set(value);

                if (value == null)
                    return;

                if (value.FileSystemObject != null)
                {
                    var args = new DirectoryChangedEventArgs(value.Service, value.Provider, value.FileSystemObject as DirectoryModel);
                    EventAggregator.Publish(args);
                }
            }
        }

        #endregion

        void OnNodeExpanded(TreeNodeModel node, bool isExpaded)
        {
            if (!isExpaded)
                return;

            if (node.IsHavingDummyChild)
                PopulateCommand.Execute(node);
        }

        void AddChildNode(TreeNodeModel node, TreeNodeModel childNode)
        {
            Invoke(() => node.Children.Add(childNode));
        }

        async void Populate(TreeNodeModel node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node.IsExpanded && !node.IsHavingDummyChild)
                return;

            node.RemoveDummyChild();

            if (node.Service == null)
            {
                foreach (var service in GetService<ProviderService>().Services)
                {
                    var child = new TreeNodeModel(service, null);
                    child.Label = service.Name;
                    child.ImagePath = service.ImagePath;
                    child.NodeExpanded += OnNodeExpanded;
                    child.AddDummyChild();
                    AddChildNode(node, child);
                }
            }
            else if (node.Provider == null)
            {
                var provider = await node.Service.GetDefaultProviderAsync();

                var child = new TreeNodeModel(node.Service, provider);
                child.Label = provider.Name;
                child.FileSystemObject = new DirectoryModel();
                child.ImagePath = provider.ImagePath;
                child.NodeExpanded += OnNodeExpanded;
                child.AddDummyChild();
                AddChildNode(node, child);
            }
            else
            {
                var currentDirectory = await node.Service.GetDirectoryAsync(node.Provider, node.FileSystemObject?.Path);
                foreach (var directory in currentDirectory.Directories)
                {
                    var child = new TreeNodeModel(node.Service, node.Provider);
                    child.Label = directory.Name;
                    child.FileSystemObject = directory;
                    child.NodeExpanded += OnNodeExpanded;
                    if (directory.Type == FileSystemObjectType.Drive)
                        child.ImagePath = "avares://Jaya.Ui/Assets/Images/Hdd-16.png";
                    child.AddDummyChild();
                    AddChildNode(node, child);
                }
            }
        }
    }
}
