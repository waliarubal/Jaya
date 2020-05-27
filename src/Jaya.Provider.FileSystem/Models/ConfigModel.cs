//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Newtonsoft.Json;

namespace Jaya.Provider.FileSystem.Models
{
    public class ConfigModel: ConfigModelBase
    {
        [JsonProperty]
        public bool IsProtectedFileVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        protected override ConfigModelBase Empty()
        {
            return new ConfigModel();
        }
    }
}
