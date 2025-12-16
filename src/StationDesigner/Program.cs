using System;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using System.Windows.Forms;

using Station;
using Utility;

namespace StationDesigner
{
    static class Program
    {
        static ResourceManager _resourceManager = null;

        static private ResourceManager ResourceMgr
        {
            get
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new ResourceManager("StationDesigner.StationDesignerStrings", typeof(StationDesigner).Assembly);
                    _resourceManager.IgnoreCase = true;
                }
                return _resourceManager;
            }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string strPath = Tools.GetAppSubDir(Application.StartupPath.ToLower(), "");

            ResourceManager resMgr = ResourceMgr;

            BusinessLayer oBusinessLayer = new BusinessLayer(resMgr);

            string databasePath = StationDesigner.Default.DatabasePath;

            if (oBusinessLayer.InitializeAccessDb(databasePath, "station123"))
            {
                Application.Run(new DesignerView(oBusinessLayer));
            }
        }
    }
}