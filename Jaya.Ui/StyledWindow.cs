using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;

namespace Jaya.Ui
{
    public class StyledWindow: Window, IStyleable
    {
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
        }

        Type IStyleable.StyleKey => typeof(StyledWindow);
    }
}
