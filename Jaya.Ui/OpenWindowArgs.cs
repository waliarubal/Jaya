using System;

namespace Jaya.Ui
{
    public class OpenWindowArgs
    {
        public string Title { get; set; }

        public Type ChildType { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
