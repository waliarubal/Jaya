//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Models;
using Newtonsoft.Json;

namespace Jaya.Provider.Dropbox.Models
{
    public class AccountModel: AccountModelBase
    {
        public AccountModel(string id, string name): base(id, name)
        {
            
        }

        [JsonProperty]
        public string Email
        {
            get => Get<string>();
            set => Set(value);
        }

        [JsonProperty]
        public string Token
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
