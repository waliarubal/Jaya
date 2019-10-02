using Jaya.Ui.Base;
using System;

namespace Jaya.Ui.Models
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
            set
            {
                if (Set(value) && Content != null)
                    InitializeContent();
            }
        }

        public object Content
        {
            get => Get<object>();
            private set => Set(value);
        }

        internal void InitializeContent()
        {
            if (ContentType == null)
                return;

            Content = Activator.CreateInstance(ContentType);
        }
    }
}
