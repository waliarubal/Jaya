using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;

namespace Jaya.Ui.ViewModels
{
    public class UpdateViewModel: ViewModelBase
    {
        RelayCommand _checkForUpdate;
        UpdateService _updateService;

        public UpdateViewModel()
        {
            _updateService = GetService<UpdateService>();

            Title = Constants.UP_TO_DATE;
            LastChecked = _updateService?.LastChecked;
        }

        public string Title
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string VersionString => _updateService?.VersionString;

        public byte? Bitness => _updateService?.Bitness;

        public ReleaseModel LatestRelease
        {
            get => Get<ReleaseModel>();
            private set => Set(value);
        }

        public DateTime? LastChecked
        {
            get => Get<DateTime?>();
            private set => Set(value);
        }

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
            Title = Constants.CHECKING_FOR_UPDATE;
            IsBusy = true;

            LatestRelease = await _updateService.CheckForUpdate();
            if (LatestRelease == null)
                Title = Constants.UP_TO_DATE;
            else
                Title = Constants.UPDATE_AVAILABLE;

            IsBusy = false;
            LastChecked = _updateService.LastChecked;
        }
    }
}
