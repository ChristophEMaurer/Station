using System;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

using Station.AppFramework;
using AppFramework;
using Utility;

namespace Station
{
    public class DatabaseLayerAccess : DatabaseLayer
    {
        /// <summary>
        /// <br/>count(...) liefert long
        /// </summary>
        /// <param name="bizLayer"></param>
        /// <param name="strConnectionString"></param>
        public DatabaseLayerAccess(BusinessLayerCommon businessLayer, string strConnectionString)
            : base(businessLayer, DatabaseType.MSAccess, strConnectionString)
        {
        }

    }
}
