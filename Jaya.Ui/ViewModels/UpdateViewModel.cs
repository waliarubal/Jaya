using Jaya.Shared;
using Jaya.Shared.Base;
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
            _updateService = ServiceLocator.Instance.GetService<UpdateService>();
        }

        Version Version => Constants.VERSION;

        public string VersionString => string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);

        public byte Bitness => Environment.Is64BitOperatingSystem ? (byte)64 : (byte)32;

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
            var update = await _updateService.CheckForUpdate();
        }
    }
}
