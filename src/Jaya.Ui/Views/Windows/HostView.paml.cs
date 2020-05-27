//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Shared.Controls;

namespace Jaya.Ui.Views.Windows
{
    public class HostView : StyledWindow
    {

        public HostView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            App.ThemeSelector.EnableThemes(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
