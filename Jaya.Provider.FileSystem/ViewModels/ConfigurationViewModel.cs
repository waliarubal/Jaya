﻿using Jaya.Provider.FileSystem.Models;
using Jaya.Provider.FileSystem.Services;
using Jaya.Shared.Base;

namespace Jaya.Provider.FileSystem.ViewModels
{
    public class ConfigurationViewModel: ViewModelBase
    {
        readonly FileSystemService _config;

        public ConfigurationViewModel()
        {
            _config = GetProvider<FileSystemService>();
        }

        public ConfigModel Configuration => _config.GetConfigurtion<ConfigModel>();
    }
}
