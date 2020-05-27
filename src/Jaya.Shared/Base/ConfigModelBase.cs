//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Newtonsoft.Json;
using System;

namespace Jaya.Shared.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ConfigModelBase : ModelBase
    {
        internal static T Empty<T>() where T : ConfigModelBase
        {
            return (T)Activator.CreateInstance<T>().Empty();
        }

        protected abstract ConfigModelBase Empty();
    }
}
