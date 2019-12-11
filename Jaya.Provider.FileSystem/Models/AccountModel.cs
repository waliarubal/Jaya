using Jaya.Shared.Models;
using System;

namespace Jaya.Provider.FileSystem.Models
{
    public class AccountModel: AccountModelBase
    {
        public AccountModel() : base(Environment.MachineName, Environment.MachineName)
        {
            
        }
    }
}
