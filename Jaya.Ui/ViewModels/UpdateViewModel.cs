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

            Update = _updateService?.Update;
            Checked = _updateService?.Checked;
        }

        public string Title
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string VersionString => _updateService?.VersionString;

        public byte? Bitness => _updateService?.Bitness;

        public bool IsUpdateAvailable
        {
            get => Get<bool>();
            private set => Set(value);
        }

        public ReleaseModel Update
        {
            get => Get<ReleaseModel>();
            private set
            {
                Set(value);

                IsUpdateAvailable = value != null;
                if (IsUpdateAvailable)
                    Title = UPDATE_AVAILABLE;
                else
                    Title = UP_TO_DATE;
            }
        }

        public DateTime? Checked
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
            Title = CHECKING_FOR_UPDATE;
            IsBusy = true;

            await _updateService.CheckForUpdate();
            Update = _updateService.Update;
            Checked = _updateService.Checked;

            IsBusy = false;
        }
    }
}
