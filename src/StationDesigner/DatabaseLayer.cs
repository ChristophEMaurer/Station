using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;

using AppFramework;
using Station.AppFramework;

namespace StationDesigner
{
    /// <summary>
    /// DateTime-Felder:
    /// <br>SQLServer:
    /// select Geburtsdatum from Patient
    /// ((DateTime)oDataRow["Geburtsdatum"]).ToShortDateString() -> Control.Text
    /// 
    /// control.Text -> insert into Patient (Geburtsdatum) values (convert(DateTime, 'strText', 104)
    /// control.Text -> insert into Patient (Geburtsdatum) values (null)
    /// </br>
    /// Access: control.Text -> strText --DataAdapter,Fill()--> DateTime
    /// </summary>
    public abstract class DatabaseLayer : DatabaseLayerCommon
    {
        public DatabaseLayer(BusinessLayerCommon businessLayer, DatabaseType databaseType, string connectionString) 
            : base(businessLayer, databaseType, connectionString)
        {
        }
    }
}
