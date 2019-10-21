using Jaya.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaya.Shared.Contracts
{
    public interface ICommandService
    {
        EventAggregator EventAggregator { get; }
    }
}
