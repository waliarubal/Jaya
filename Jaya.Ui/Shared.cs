using Jaya.Ui.Models;

namespace Jaya.Ui
{
    class Shared
    {
        static Shared _instance;
        static object _syncRoot;

        #region constructor

        static Shared()
        {
            _syncRoot = new object();
        }

        private Shared()
        {
            ToolbarConfiguration = new ToolbarConfigModel();
        }

        #endregion

        public static Shared Instance
        {
            get
            {
                lock(_syncRoot)
                {
                    if (_instance == null)
                        _instance = new Shared();

                    return _instance;
                }
            }
        }

        public ToolbarConfigModel ToolbarConfiguration { get; }

        public void ExecuteCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.ToggleToolbars:
                    ToolbarConfiguration.IsVisible = !ToolbarConfiguration.IsVisible;
                    break;

                case CommandType.ToggleToolbarFile:
                    ToolbarConfiguration.IsFileVisible = !ToolbarConfiguration.IsFileVisible;
                    break;

                case CommandType.ToggleToolbarEdit:
                    ToolbarConfiguration.IsEditVisible = !ToolbarConfiguration.IsEditVisible;
                    break;

                case CommandType.ToggleToolbarView:
                    ToolbarConfiguration.IsViewVisible = !ToolbarConfiguration.IsViewVisible;
                    break;

                case CommandType.ToggleToolbarHelp:
                    ToolbarConfiguration.IsHelpVisible = !ToolbarConfiguration.IsHelpVisible;
                    break;
            }
        }
    }
}
