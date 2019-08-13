using Jaya.Ui.Models;
using System;
using System.Collections.Generic;

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
            ToolbarConfiguration = new ToolbarConfigModel
            {
                IsFileVisible = true,
                IsEditVisible = true,
                IsViewVisible = true,
                IsHelpVisible = true,
                IsVisible = true
            };
            PaneConfiguration = new PaneConfigModel
            {
                IsNavigationPaneVisible = true,
                IsDetailsPaneVisible = false,
                IsPreviewPaneVisible = false
            };
        }

        #endregion

        #region properties

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

        public PaneConfigModel PaneConfiguration { get; }

        #endregion

        public void SharedCommandAction(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            var parameters = parameter as List<object>;
            if (parameters == null)
                throw new ArgumentException("Failed to parse command parameters.", nameof(parameter));

            var type = (CommandType)parameters[0];
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

                case CommandType.TogglePaneNavigation:
                    PaneConfiguration.IsNavigationPaneVisible = !PaneConfiguration.IsNavigationPaneVisible;
                    break;

                case CommandType.TogglePanePreview:
                    PaneConfiguration.IsPreviewPaneVisible = !PaneConfiguration.IsPreviewPaneVisible;
                    break;

                case CommandType.TogglePaneDetails:
                    PaneConfiguration.IsDetailsPaneVisible = !PaneConfiguration.IsDetailsPaneVisible;
                    break;
            }
        }
    }
}
