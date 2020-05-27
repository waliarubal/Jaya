//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
namespace Jaya.Shared.Services
{
    public sealed class CommandService: ICommandService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }
    }
}
