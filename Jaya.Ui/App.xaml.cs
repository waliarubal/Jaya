using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Ui.Services;

namespace Jaya.Ui
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void RegisterServices()
        {
            base.RegisterServices();

            ServiceLocator.Instance.GetService<ConfigurationService>().LoadConfiguration();
        }

        protected override void OnExiting(object sender, EventArgs e)
        {
            ServiceLocator.Instance.GetService<ConfigurationService>().SaveConfiguration();

            base.OnExiting(sender, e);
        }
    }
}
