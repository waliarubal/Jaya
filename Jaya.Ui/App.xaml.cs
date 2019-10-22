using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Shared;
using Jaya.Ui.Services;
using System;

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
            ServiceLocator.Instance.GetService<SharedService>().LoadConfigurations();
        }

        protected override void OnExiting(object sender, EventArgs e)
        {
            ServiceLocator.Instance.GetService<SharedService>().SaveConfigurations();
            base.OnExiting(sender, e);
        }
    }
}
