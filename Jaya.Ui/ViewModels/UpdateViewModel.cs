using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;

namespace Jaya.Ui.ViewModels
{
    public class UpdateViewModel: ViewModelBase
    {
        const string UP_TO_DATE = "You're up to date";
        const string CHECKING_FOR_UPDATE = "Checking for update...";
        const string UPDATE_AVAILABLE = "Update available";

        RelayCommand _checkForUpdate;
        UpdateService _updateService;

        public UpdateViewModel()
        {
            _updateService = GetService<UpdateService>();

            if (Update == null)
                Title = UP_TO_DATE;
            else
                Title = UPDATE_AVAILABLE;
        }

        public string Title
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string VersionString => _updateService?.VersionString;

        public byte? Bitness => _updateService?.Bitness;

        public ReleaseModel Update => _updateService?.Update;

        public DateTime? Checked => _updateService?.Checked;

        public RelayCommand CheckForUpdateCommand
        {
            get
            {
                if (_checkForUpdate == null)
                    _checkForUpdate = new RelayCommand(CheckForUpdateAction);

                return _checkForUpdate;
            }
        }

        async void CheckForUpdateAction()
        {
            Title = CHECKING_FOR_UPDATE;
            IsBusy = true;

            await _updateService.CheckForUpdate();

            if (Update == null)
                Title = UP_TO_DATE;
            else
                Title = UPDATE_AVAILABLE;

            RaisePropertyChanged(nameof(Checked));
            IsBusy = false;
        }
    }
}
