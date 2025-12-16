using System;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using System.Windows.Forms;
using System.Data;

using Utility;

namespace Station
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
                    _resourceManager = new ResourceManager("Station.StationStrings", typeof(Station).Assembly);
                    _resourceManager.IgnoreCase = true;
                }
                return _resourceManager;
            }
        }


        [STAThread]
        static void Main()
        {
            bool bSuccess = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string strPath = Tools.GetAppSubDir(Application.StartupPath.ToLower(), "");

            ResourceManager resMgr = ResourceMgr;

            BusinessLayer oBusinessLayer = new BusinessLayer(resMgr);

            // Neu ab Version 11.3: Man kann in der .config die Datenbank umstellen!
            string strDatabaseType = Station.Default.DatabaseType;

            if (strDatabaseType.Length == 0)
            {
                // Diesen Eintrag gab es in den 
                strDatabaseType = "MSAccess";
            }
            if (strDatabaseType == "MSAccess")
            {
                bSuccess = oBusinessLayer.InitializeAccessDb(strPath, "station123");
            }
            else if (strDatabaseType == "SQLServer")
            {
                bSuccess = oBusinessLayer.InitializeSQLServer(strPath, null, null);
            }
            else if (strDatabaseType == "MySQL")
            {
                bSuccess = oBusinessLayer.InitializeMySql(strPath, null, null);
            }
            else
            {
                bSuccess = false;
                MessageBox.Show("Es werden die Datenbanken MS-Access und SQLServer unterstützt."
                    + "\nDer folgende Datenbanktyp ist nicht bekannt:"
                    + "\n\n'" + strDatabaseType + "'"
                    + "\n\nDie Anwendung wird daher beendet.");
            }


            //if (oBusinessLayer.Initialize(strPath))
            {
                DataView dv = oBusinessLayer.DatabaseLayer.GetDiagramme();
                if (dv.Table.Rows.Count > 1)
                {
                    Application.Run(new MainView(oBusinessLayer, Application.ExecutablePath));
                }
                else
                {
                    Application.Run(new DynamicDiagramView(oBusinessLayer, dv.Table.Rows[0]));
                }
            }
        }
    }
}