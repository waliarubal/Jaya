//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Jaya.Ui.Services;
using System;

namespace Jaya.Ui.ViewModels
{
    public class AboutViewModel: ViewModelBase
    {
        UpdateService _updateService;

        public AboutViewModel()
        {
            _updateService = GetService<UpdateService>();
        }

        public string Name => Constants.APP_NAME;

        public string Description => Constants.APP_DESCRIPTION;

        public string VersionString => _updateService?.VersionString;

        public byte? Bitness => _updateService?.Bitness;

        public Uri DonationUrl => Constants.URL_DONATION;

        public Uri IssuesUrl => Constants.URL_ISSUES;

        public Uri LicenseUrl => Constants.URL_LICENSE;

        public Uri ReleasesUrl => Constants.URL_LICENSE;
    }
}
