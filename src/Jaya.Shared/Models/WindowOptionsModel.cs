using Jaya.Shared.Base;
using System;

namespace Jaya.Shared.Models
{
    public class WindowOptionsModel : ModelBase
    {
        public string Title
        {
            get => Get<string>();
            set => Set(value);
        }

        public double Width
        {
            get => Get<double>();
            set => Set(value);
        }

        public double Height
        {
            get => Get<double>();
            set => Set(value);
        }

        public Type ContentType
        {
            get => Get<Type>();
            set => Set(value);
        }
    }
}
