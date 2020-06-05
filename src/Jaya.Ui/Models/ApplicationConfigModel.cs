//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Newtonsoft.Json;
using System;

namespace Jaya.Ui.Models
{
    public class ApplicationConfigModel : ConfigModelBase
    {

        [JsonProperty]
        public bool IsItemCheckBoxVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsFileNameExtensionVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsHiddenItemVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public double WidthPx
        {
            get => Get<double>();
            set => Set(value);
        }

        [JsonProperty]
        public double HeightPx
        {
            get => Get<double>();
            set => Set(value);
        }


        public ThemeModel Theme
        {
            get => Get<ThemeModel>();
            set
            {
                if (Set(value))
                {
                    ThemeManager.Instance.SelectedTheme = value;
                    Set(value.Name, nameof(ThemeName), false);
                }
            }
        }

        [JsonProperty]
        string ThemeName
        {
            get => Get<string>();
            set
            {
                Set(value);
                SetTheme(value);
            }
        }

        void SetTheme(string name)
        {
            foreach (var theme in ThemeManager.Instance.Themes)
            {
                if (theme.Name.Equals(name, StringComparison.InvariantCulture))
                {
                    Theme = theme;
                    break;
                }
            }
        }

        protected override ConfigModelBase Empty()
        {
            return new ApplicationConfigModel
            {
                WidthPx = 800,
                HeightPx = 600,
                ThemeName = "Dark"
            };
        }
    }
}
