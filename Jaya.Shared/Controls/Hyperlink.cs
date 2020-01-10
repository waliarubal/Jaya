using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using Jaya.Shared.Services;
using System;

namespace Jaya.Shared.Controls
{
    public class Hyperlink : Button, IStyleable
    {
        public static readonly DirectProperty<Hyperlink, Uri> UrlProperty;

        Uri _url;
        ContentPresenter _container;

        static Hyperlink()
        {
            UrlProperty = AvaloniaProperty.RegisterDirect<Hyperlink, Uri>(nameof(Url), o => o.Url, (o, v) => o.Url = v, null);
        }

        public Uri Url
        {
            get => _url;
            set => SetAndRaise(UrlProperty, ref _url, value);
        }

        Type IStyleable.StyleKey => typeof(Hyperlink);

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            _container = GetControl<ContentPresenter>(e, "PART_ContentPresenter");
            _container.Tapped += delegate
            {
                if (Url == null || Design.IsDesignMode)
                    return;

                ServiceLocator.Instance.GetService<PlatformService>().OpenBrowser(Url.ToString());
            };
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
