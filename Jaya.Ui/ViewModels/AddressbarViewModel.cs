namespace Jaya.Ui.ViewModels
{
    public class AddressbarViewModel : ViewModelBase
    {
        public string SearchQuery
        {
            get => Get<string>();
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(IsSearchQueryEmpty));
            }
        }

        public bool IsSearchQueryEmpty => string.IsNullOrEmpty(SearchQuery);

        public void ClearSearchQuery()
        {
            SearchQuery = null;
        }
    }
}
