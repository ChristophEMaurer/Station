using System;
using System.Text;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


using AppFramework;
using Station.AppFramework;

namespace StationDesigner
{
    public class BusinessLayer : BusinessLayerCommon
	{
		protected DatabaseLayer _oDatabaseLayer;

        public BusinessLayer(System.Resources.ResourceManager resourceManager)
            : base(resourceManager)
        {
        }

        public DatabaseLayer DatabaseLayer
        {
            get { return _oDatabaseLayer; }
        }

        override protected DatabaseLayerCommon CreateDatabaseAccess(string strDataSource)
        {
            return new DatabaseLayerAccess(this, strDataSource);
        }

        public override void SetDatabaseLayer(DatabaseLayerBase databaseLayer)
        {
            base.SetDatabaseLayer(databaseLayer);
            _oDatabaseLayer = (DatabaseLayer)databaseLayer;
        }

        override public void DebugPrint(long flag, string msg)
        {
        }
    }
}

