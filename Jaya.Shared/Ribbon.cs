using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace Jaya.Shared
{
    /// <summary>
    /// Refer https://www.powerworld.com/WebHelp/Content/MainDocumentation_HTML/Ribbons.htm for details.
    /// </summary>
    public class Ribbon: TabControl, IStyleable
    {
        static Ribbon()
        {

        }

        Type IStyleable.StyleKey => typeof(Ribbon);
    }
}
