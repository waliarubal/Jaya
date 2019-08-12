using Jaya.Ui.Models;

namespace Jaya.Ui
{
    class Shared
    {
        static Shared _instance;
        static object _syncLock;

        static Shared()
        {
            _syncLock = new object();
        }

        private Shared()
        {
            ToolbarsConfiguration = new ToolbarsConfigurationModel();
        }

        public static Shared Instance
        {
            get
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                        _instance = new Shared();

                    return _instance;
                }
            }
        }

        public ToolbarsConfigurationModel ToolbarsConfiguration { get; }

    }
}
