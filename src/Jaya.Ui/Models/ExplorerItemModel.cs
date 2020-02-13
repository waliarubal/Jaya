using Jaya.Shared.Base;
using System.Collections.ObjectModel;

namespace Jaya.Ui.Models
{
    public class ExplorerItemModel: ModelBase
    {
        public ExplorerItemModel(ItemType? type, string label, object obj, string imagePath = null)
        {
            Type = type;
            Label = label;
            Children = new ObservableCollection<ExplorerItemModel>();
            Object = obj;
            ImagePath = imagePath;
        }

        #region properties

        internal ItemType? Type { get; }

        public object Object { get; }

        public bool IsDummy => Type == ItemType.Dummy;

        public bool IsService => Type == ItemType.Service;

        public bool IsDrive => Type == ItemType.Drive;

        public bool IsDirectory => Type == ItemType.Directory;

        public bool IsAccount => Type == ItemType.Account;

        public bool IsComputer => Type == ItemType.Computer;

        public bool IsFile => Type == ItemType.File;

        public bool IsHavingMetaData => IsAccount || IsDrive || IsDirectory;

        public string Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ImagePath
        {
            get => Get<string>();
            set => Set(value);
        }

        public ObservableCollection<ExplorerItemModel> Children { get; }

        #endregion
    }
}
