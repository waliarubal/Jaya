//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Newtonsoft.Json;
using System;

namespace Jaya.Ui.Models
{
    public class ReleaseModel
    {
        [JsonProperty("name")]
        public Version Version { get; set; }

        [JsonProperty("prerelease")]
        public bool IsPrerelease { get; set; }

        [JsonProperty("draft")]
        public bool IsDraft { get; set; }

        [JsonProperty("published_at")]
        public DateTime Date { get; set; }

        [JsonProperty("body")]
        public string Notes { get; set; }

        [JsonProperty("assets")]
        public ReleaseAssetModel[] Downloads { get; set; }

        public override string ToString()
        {
            if (Version == null)
                return null;

            return string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);
        }
    }
}
