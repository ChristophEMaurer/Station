using System;
using System.Data;
using System.Text;
using System.Collections;

using AppFramework;
using Utility;

namespace Station.AppFramework
{
    public class DatabaseLayerCommon : DatabaseLayerBase
    {
        public event ProgressCallback Progress;

        protected string TextAlle;

        private BusinessLayerCommon _businessLayerCommon;


        public DatabaseLayerCommon(BusinessLayerCommon businessLayer, DatabaseType databaseType, string connectionString)
            : base(businessLayer, databaseType, connectionString)
        {
            _businessLayerCommon = businessLayer;

            TextAlle = _businessLayerCommon.GetText("DatabaseLayerCommon", "alle");
        }

        protected void FireProgressEvent(ProgressEventArgs e)
        {
            if (Progress != null)
            {
                Progress(e);
            }
        }

        public void HandleDatum(DateTime? from, DateTime? to, StringBuilder sb)
        {
            HandleDatum(from, to, sb, null);
        }

        public void HandleDatum(DateTime? from, DateTime? to, StringBuilder sb, string prefix)
        {
            string s = "";

            if (from.HasValue)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    s = string.Format("AND {0} <= Datum ", DateTime2DBDateTimeString(from.Value));
                }
                else
                {
                    s = string.Format("AND {0} <= {1}.Datum ", DateTime2DBDateTimeString(from.Value), prefix);
                }
            }

            sb.Replace("$datefrom$", s);

            s = "";
            if (to.HasValue)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    s = string.Format("AND Datum <= {0} ", DateTime2DBDateTimeString(to));
                }
                else
                {
                    s = string.Format("AND {0}.Datum <= {1} ", prefix, DateTime2DBDateTimeString(to));
                }
            }
            sb.Replace("$dateto$", s);
        }
        #region LogTable
        public int Write2Log(string sUser, string sAction, string sMessage)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO LogTable 
                    (
                    [User],
                    [Action],
                    [Message],
                    [Timestamp]
                    )
                VALUES
                    (
                    @User,
                    @Action,
                    @Message,
                    @Timestamp
                    )
                ");

            sb.Replace("@Timestamp", TimestampNowSyntax());

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@User", sUser));
            aSQLParameters.Add(this.SqlParameter("@Action", sAction));
            aSQLParameters.Add(this.SqlParameter("@Message", sMessage));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "LogTable");
        }
        public DataView GetLogTable(string strNumRecords, string strUser, string strVon, string strBis, string strAktion, string strMessage)
        {
            string sSQL =
                @"
                SELECT @TOP
                    ID_LogTable,
                    [TimeStamp],
                    [User],
                    Action,
                    Message
                FROM
                    LogTable
                @WHERE
                ORDER BY
                    [TimeStamp] DESC
                ";

            ArrayList arSqlParameter = new ArrayList();

            string strWhere = "WHERE 1=1";
            if (strNumRecords.Length > 0)
            {
                sSQL = sSQL.Replace("@TOP", "TOP " + strNumRecords.ToString());
            }
            else
            {
                sSQL = sSQL.Replace("@TOP", "");
            }
            if (strVon.Length > 0)
            {
                strWhere += " AND [TimeStamp] >= " + DateString2DBDateString(strVon);
            }
            if (strBis.Length > 0)
            {
                strWhere += " AND [TimeStamp] <= " + DateString2DBDateStringEnd(strBis);
            }
            if (strUser.Length > 0)
            {
                string s = base.CreateLikeExpression(strUser);
                strWhere += " AND [User] like @User";
                arSqlParameter.Add(this.SqlParameter("@User", "%" + s + "%"));
            }
            if (strMessage.Length > 0)
            {
                string s = base.CreateLikeExpression(strMessage);
                strWhere += " AND Message like @Message";
                arSqlParameter.Add(this.SqlParameter("@Message", "%" + s + "%"));
            }
            if (strAktion.Length > 0)
            {
                string s = base.CreateLikeExpression(strAktion);
                strWhere += " AND Action like @Action";
                arSqlParameter.Add(this.SqlParameter("@Action", "%" + s + "%"));
            }

            sSQL = sSQL.Replace("@WHERE", strWhere);

            return this.GetDataView(sSQL, arSqlParameter, "LogTable");
        }

        #endregion



        #region  Patienten
        public bool MovePatient(int nID_BettenFrom, int ID_BettenTo)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE
                    Patienten 
                SET
                    ID_Betten=@ID_BettenTo
                WHERE
                    ID_Betten=@nID_BettenFrom
                ");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_BettenTo", ID_BettenTo));
            sqlParameters.Add(this.SqlParameter("@nID_BettenFrom", nID_BettenFrom));

            int effectedRecords = this.ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (effectedRecords == 1);
        }

        public int InsertPatient(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Patienten
                    (
                    Nachname,
                    Vorname,
                    Geburtsdatum,
                    Aufnahmedatum,
                    Entlassungsdatum,
                    Privat,
                    Geschlecht,
                    ID_Diagnosen,
                    Diagnose,
                    ID_Betten,
                    [Isolation],
                    AnzahlBetten
                    )
                    VALUES
                    (
                     @Nachname,
                     @Vorname,
                     @Geburtsdatum,
                     @Aufnahmedatum,
                     @Entlassungsdatum,
                     @Privat,
                     @Geschlecht,
                     @ID_Diagnosen,
                     @Diagnose,
                     @ID_Betten,
                     @Isolation,
                     @AnzahlBetten
                    )
                ");

            sb.Replace("@Geburtsdatum", DateTime2DBDateTimeString(oDataRow["Geburtsdatum"]));
            sb.Replace("@Aufnahmedatum", DateTime2DBDateTimeString(oDataRow["Aufnahmedatum"]));
            sb.Replace("@Entlassungsdatum", DateTime2DBDateTimeString(oDataRow["Entlassungsdatum"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Nachname", (string)oDataRow["Nachname"]));
            aSQLParameters.Add(this.SqlParameter("@Vorname", (string)oDataRow["Vorname"]));
            aSQLParameters.Add(this.SqlParameter("@Privat", (int)oDataRow["Privat"]));
            aSQLParameters.Add(this.SqlParameter("@Geschlecht", (int)oDataRow["Geschlecht"]));
            aSQLParameters.Add(this.SqlParameterInt("@ID_Diagnosen", oDataRow["ID_Diagnosen"]));
            aSQLParameters.Add(this.SqlParameter("@Diagnose", (string)oDataRow["Diagnose"]));
            aSQLParameters.Add(this.SqlParameterInt("@ID_Betten", oDataRow["ID_Betten"]));
            aSQLParameters.Add(this.SqlParameter("@Isolation", (int)oDataRow["Isolation"]));
            aSQLParameters.Add(this.SqlParameter("@AnzahlBetten", (int)oDataRow["AnzahlBetten"]));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "Patienten");
        }

        public bool UpdatePatient(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE Patienten
                SET
                    Nachname=@Nachname,
                    Vorname=@Vorname,
                    Geburtsdatum=@Geburtsdatum,
                    Aufnahmedatum=@Aufnahmedatum,
                    Entlassungsdatum=@Entlassungsdatum,
                    Geschlecht=@Geschlecht,
                    Privat=@Privat,
                    ID_Diagnosen=@ID_Diagnosen,
                    Diagnose=@Diagnose,
                    ID_Betten=@ID_Betten,
                    [Isolation]=@Isolation,
                    IsolationText=@IsolationText,
                    Hervorheben=@Hervorheben,
                    HervorhebenGrund=@HervorhebenGrund,
                    AnzahlBetten=@AnzahlBetten
                WHERE
                    ID_Patienten=@ID_Patienten
                ");

            sb.Replace("@Geburtsdatum", DateTime2DBDateTimeString(oDataRow["Geburtsdatum"]));
            sb.Replace("@Aufnahmedatum", DateTime2DBDateTimeString(oDataRow["Aufnahmedatum"]));
            sb.Replace("@Entlassungsdatum", DateTime2DBDateTimeString(oDataRow["Entlassungsdatum"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Nachname", (string)oDataRow["Nachname"]));
            aSQLParameters.Add(this.SqlParameter("@Vorname", (string)oDataRow["Vorname"]));
            aSQLParameters.Add(this.SqlParameter("@Geschlecht", (int)oDataRow["Geschlecht"]));
            aSQLParameters.Add(this.SqlParameter("@Privat", (int)oDataRow["Privat"]));
            aSQLParameters.Add(this.SqlParameterInt("@ID_Diagnosen", oDataRow["ID_Diagnosen"]));
            aSQLParameters.Add(this.SqlParameter("@Diagnose", (string)oDataRow["Diagnose"]));
            aSQLParameters.Add(this.SqlParameterInt("@ID_Betten", oDataRow["ID_Betten"]));
            aSQLParameters.Add(this.SqlParameter("@Isolation", (int)oDataRow["Isolation"]));
            aSQLParameters.Add(this.SqlParameter("@IsolationText", (string)oDataRow["IsolationText"]));
            aSQLParameters.Add(this.SqlParameter("@Hervorheben", (int)oDataRow["Hervorheben"]));
            aSQLParameters.Add(this.SqlParameter("@HervorhebenGrund", (string)oDataRow["HervorhebenGrund"]));
            aSQLParameters.Add(this.SqlParameter("@AnzahlBetten", (int)oDataRow["AnzahlBetten"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", (int)oDataRow["ID_Patienten"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public DataRow GetBettPatient(int nID_Betten)
        {
            string s =
                @"
                SELECT 
                    ID_Patienten,
                    Nachname,
                    Vorname,
                    Geburtsdatum, 
                    Aufnahmedatum,
                    Entlassungsdatum,
                    Geschlecht,
                    Privat,
                    Patienten.[Isolation],
                    Patienten.IsolationText,
                    Patienten.Hervorheben,
                    Patienten.HervorhebenGrund,
                    Patienten.AnzahlBetten,
                    Patienten.Diagnose,
                    Patienten.ID_Diagnosen,
                    Diagnosen.DRG_Name as DiagnoseDiagnose,
                    Patienten.ID_Betten as PatientenID_Betten,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer,
                    Betten.BettenNummer,
                    Betten.ID_Betten as BettenID_Betten
                FROM 
                    (Patienten LEFT JOIN Diagnosen ON Patienten.ID_Diagnosen = Diagnosen.ID_Diagnosen)
                    RIGHT JOIN (Betten right join Zimmer on Zimmer.ID_Zimmer= Betten.ID_Zimmer)ON Patienten.ID_Betten = Betten.ID_Betten
                WHERE
                    Betten.ID_Betten=@ID_Betten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Betten", nID_Betten));

            return this.GetRecord(s, aSQLParameters, "Patienten");
        }

        public DataRow GetPatient(int ID_Patienten)
        {
            string s =
                @"
                SELECT 
                    ID_Patienten,
                    Nachname,
                    Vorname,
                    Geburtsdatum, 
                    Aufnahmedatum,
                    Entlassungsdatum,
                    Geschlecht,
                    Privat,
                    Patienten.[Isolation],
                    Patienten.IsolationText,
                    Patienten.Hervorheben,
                    Patienten.HervorhebenGrund,
                    Patienten.AnzahlBetten,
                    Patienten.Diagnose,
                    Patienten.ID_Diagnosen,
                    Diagnosen.DRG_Name as DiagnoseDiagnose,
                    Patienten.ID_Betten,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer,
                    Betten.BettenNummer 
                FROM 
                    (Patienten LEFT JOIN Diagnosen ON Patienten.ID_Diagnosen = Diagnosen.ID_Diagnosen)
                    LEFT JOIN (Betten left join Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer) ON Patienten.ID_Betten = Betten.ID_Betten
                WHERE
                    ID_Patienten=@ID_Patienten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", ID_Patienten));

            return this.GetRecord(s, aSQLParameters, "Patienten");
        }

        public int GetID_Patienten(string strNachname, string strVorname, DateTime dtGebDatum)
        {
            int nID_Patienten = -1;

            string s =
                @"
                SELECT 
                    ID_Patienten
                FROM 
                    Patienten
                WHERE
                    Nachname=@Nachname
                    AND Vorname=@Vorname
                    AND Geburtsdatum=@Geburtsdatum
                ";

            s = s.Replace("@Geburtsdatum", DateTime2DBDateTimeString(dtGebDatum));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Nachname", strNachname));
            aSQLParameters.Add(this.SqlParameter("@Vorname", strVorname));

            DataRow oRow = GetRecord(s, aSQLParameters, "Patienten");

            if (oRow != null)
            {
                nID_Patienten = (int)oRow["ID_Patienten"];
            }

            return nID_Patienten;
        }

        /// <summary>
        /// Die Reihenfolge der Spalten darf NICHT fehlerverursachend sein können!!!
        /// </summary>
        /// <returns></returns>
        public DataRow CreateDataRowPatient()
        {
            DataTable dt = new DataTable("Patient");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Patienten", typeof(int)));
            dt.Columns.Add(new DataColumn("Nachname", typeof(string)));
            dt.Columns.Add(new DataColumn("Vorname", typeof(string)));
            dt.Columns.Add(new DataColumn("Geburtsdatum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Aufnahmedatum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Entlassungsdatum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Privat", typeof(int)));
            dt.Columns.Add(new DataColumn("Geschlecht", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Diagnosen", typeof(int)));
            dt.Columns.Add(new DataColumn("Diagnose", typeof(string)));
            dt.Columns.Add(new DataColumn("ID_Betten", typeof(int)));
            dt.Columns.Add(new DataColumn("Isolation", typeof(int)));
            dt.Columns.Add(new DataColumn("IsolationText", typeof(string)));
            dt.Columns.Add(new DataColumn("Hervorheben", typeof(int)));
            dt.Columns.Add(new DataColumn("HervorhebenGrund", typeof(string)));
            dt.Columns.Add(new DataColumn("AnzahlBetten", typeof(int)));

            dataRow = dt.NewRow();

            dataRow["ID_Patienten"] = DBNull.Value;
            dataRow["Nachname"] = "";
            dataRow["Vorname"] = "";
            dataRow["Geburtsdatum"] = new DateTime(1900, 1, 1);
            dataRow["Aufnahmedatum"] = DateTime.Today;
            dataRow["Entlassungsdatum"] = DBNull.Value;
            dataRow["Privat"] = 0;
            dataRow["Geschlecht"] = 1;
            dataRow["ID_Diagnosen"] = DBNull.Value;
            dataRow["Diagnose"] = "";
            dataRow["ID_Betten"] = DBNull.Value;
            dataRow["Isolation"] = 0;
            dataRow["IsolationText"] = "";
            dataRow["Hervorheben"] = 0;
            dataRow["HervorhebenGrund"] = "";
            dataRow["AnzahlBetten"] = 3;

            return dataRow;
        }

        public int GetPatientenAnzahlMitInfektion(string strID)
        {
            string sSQL =
                @"
                SELECT 
                    count(ID_PatientenInfektionen) as Anzahl
                FROM    
                    PatientenInfektionen
                    LEFT JOIN Infektionen on Infektionen.ID_Infektionen = PatientenInfektionen.ID_Infektionen
                WHERE 
                    Infektionen.ID = @ID
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID", strID));

            DataRow oDataRow = this.GetRecord(sSQL, aSQLParameters, "PatientenInfektionen");
            int nCount = (int)oDataRow["Anzahl"];

            sSQL =
                @"
                SELECT 
                    count(ID_Patienten) as Anzahl
                FROM    
                    Patienten
                    LEFT JOIN Infektionen on Infektionen.ID_Infektionen = Patienten.ID_Infektionen
                WHERE 
                    Infektionen.ID = @ID
                ";

            oDataRow = this.GetRecord(sSQL, aSQLParameters, "Patienten");
            nCount += (int)oDataRow["Anzahl"];

            return nCount;
        }

        public DataRow GetPatientNachnameAnzahlBettenByBett(int nID_Betten)
        {
            string s =
                @"
                SELECT 
                    Nachname,
                    AnzahlBetten
                FROM 
                    Patienten 
                WHERE
                    ID_Betten=@ID_Betten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Betten", nID_Betten));

            return GetRecord(s, aSQLParameters, "Patienten");
        }

        public DataRow GetPatientByBett(int nID_Betten)
        {
            string s =
                @"
                SELECT 
                    ID_Patienten,
                    Nachname,
                    Vorname,
                    Geburtsdatum, 
                    Aufnahmedatum,
                    Entlassungsdatum,
                    Geschlecht,
                    Privat,
                    Patienten.[Isolation],
                    Patienten.IsolationText,
                    Patienten.Hervorheben,
                    Patienten.HervorhebenGrund,
                    Patienten.AnzahlBetten,
                    Patienten.Diagnose,
                    Patienten.ID_Diagnosen,
                    Diagnosen.DRG_Name as DiagnoseDiagnose,
                    Patienten.ID_Betten,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer,
                    Betten.BettenNummer
                FROM 
                    (Patienten LEFT JOIN Diagnosen ON Patienten.ID_Diagnosen = Diagnosen.ID_Diagnosen)
                    LEFT JOIN (Betten LEFT JOIN Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer)
                    ON Patienten.ID_Betten = Betten.ID_Betten
                WHERE
                    Patienten.ID_Betten=@ID_Betten;
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Betten", nID_Betten));

            return GetRecord(s, aSQLParameters, "Patienten");
        }

        public DataView GetPatienten()
        {
            return GetPatientenOrderBy("Nachname, Vorname");
        }

        public DataView GetPatientenOrderBy(string strOrderBy)
        {
            string sSQL =
                @"
                SELECT 
                    ID_Patienten,
                    Nachname,
                    Vorname,
                    Geburtsdatum, 
                    Aufnahmedatum,
                    Entlassungsdatum,
                    Privat,
                    Geschlecht,
                    Patienten.[Isolation],
                    Patienten.IsolationText,
                    Patienten.Hervorheben,
                    Patienten.HervorhebenGrund,
                    Patienten.AnzahlBetten,
                    Patienten.Diagnose,
                    Patienten.ID_Diagnosen,
                    Patienten.ID_Betten,
                    Diagnosen.DRG_Name as DiagnoseDiagnose,
                    Betten.BettenNummer,
                    Zimmer.ZimmerNummer,
                    Zimmer.Station
                FROM
                    (Patienten LEFT JOIN Diagnosen ON Patienten.ID_Diagnosen = Diagnosen.ID_Diagnosen) 
                    LEFT JOIN (Betten left join Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer)
                    ON Patienten.ID_Betten = Betten.ID_Betten
                ORDER BY
                    @OrderBy
                ";

            sSQL = sSQL.Replace("@OrderBy", strOrderBy);

            return GetDataView(sSQL, null, "Patienten");
        }

        public DataView GetPatientenByNachname(string strNachname)
        {
            string sSQL =
                @"
                SELECT 
                    ID_Patienten,
                    Nachname,
                    Vorname,
                    Geburtsdatum, 
                    Aufnahmedatum,
                    Entlassungsdatum,
                    Privat,
                    Geschlecht,
                    Patienten.[Isolation],
                    Patienten.IsolationText,
                    Patienten.Hervorheben,
                    Patienten.HervorhebenGrund,
                    Patienten.AnzahlBetten,
                    Patienten.Diagnose,
                    Patienten.ID_Diagnosen,
                    Patienten.ID_Betten,
                    Diagnosen.DRG_Name as DiagnoseDiagnose,
                    Betten.BettenNummer,
                    Zimmer.ZimmerNummer,
                    Zimmer.Station
                FROM
                    (Patienten LEFT JOIN Diagnosen ON Patienten.ID_Diagnosen = Diagnosen.ID_Diagnosen) 
                    LEFT JOIN (Betten left join Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer)
                    ON Patienten.ID_Betten = Betten.ID_Betten
                WHERE
                    Nachname = @Nachname
                ORDER BY
                    Vorname;
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Nachname", strNachname));

            return GetDataView(sSQL, aSQLParameters, "Patienten");
        }

        public bool DeletePatient(int nID_Patienten)
        {
            string sb = @"
                DELETE FROM
                    Patienten
                WHERE
                    ID_Patienten = @ID_Patienten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", nID_Patienten));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        #endregion

        #region  Zimmer
        /// <summary>
        /// Holt das Minimum und das Maximum des Wertes von AnzahlBetten der Patienten, die
        /// demselben Zimmer zugeordnet sind, diese sollten allerdings alle gleich sein, 
        /// aber man weiss ja nie...
        /// </summary>
        /// <param name="nID_Zimmer"></param>
        /// <param name="bBelegt"></param>
        /// <param name="nMin"></param>
        /// <param name="nMax"></param>
        /// <returns></returns>
        public bool GetZimmerMinMaxAnzahlBetten(int nID_Zimmer, out bool bBelegt, out int nMin, out int nMax)
        {
            string s =
                @"
                SELECT 
                    min(AnzahlBetten) as MinAnzahl,
                    max(AnzahlBetten) as MaxAnzahl
                FROM
                    Betten
                    inner join Patienten on Patienten.ID_Betten = Betten.ID_Betten
                WHERE
                    Betten.ID_Zimmer = @ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            DataRow oRow = GetRecord(s, aSQLParameters, "Betten");

            if (oRow["MinAnzahl"] != DBNull.Value)
            {
                bBelegt = true;
                nMin = (int)oRow["MinAnzahl"];
                nMax = (int)oRow["MaxAnzahl"];
            }
            else
            {
                bBelegt = false;
                nMin = -1;
                nMax = -1;
            }

            return true;
        }

        /// <summary>
        /// Holt alle Felder aus Tabelle Zimmer und zusätzlich:
        /// AnzahlBetten: alle Patienten, die diesem Zimmer zugewiesen sind, müssen denselben Wert in AnzahlBetten haben.
        /// Belegt: Wenn ein Patient diesem Zimmer zugewiesen ist, ist Belegt = true sonst false.
        /// Geschlecht: 
        /// </summary>
        /// <param name="nStation"></param>
        /// <param name="nNummer"></param>
        /// <returns></returns>
        public DataRow GetZimmer(int nID_Zimmer)
        {
            // 0 as Belegt muss vorkommen, damit diese Spalte in der DataRow vorkommt.
            string sb = @"
                SELECT 
                    Zimmer.ID_Zimmer,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer,
                    0 as Belegt,
                    min(Patienten.AnzahlBetten) as AnzahlBetten,
                    max(Patienten.Geschlecht) as Geschlecht
                FROM
                    Zimmer left join (Betten LEFT JOIN Patienten ON Patienten.ID_Betten = Betten.ID_Betten)
                    on Betten.ID_Zimmer = Zimmer.ID_Zimmer
                WHERE
                    Zimmer.ID_Zimmer=@ID_Zimmer
                group by 
                    Zimmer.ID_Zimmer,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));
            DataRow oDataRow = this.GetRecord(sb, aSQLParameters, "Zimmer");

            // Durch den left join kann da nix rauskommen, also DBNULL, dann ist das Zimmer gar nicht belegt und hat 3 freie Betten
            if (oDataRow["AnzahlBetten"] == DBNull.Value)
            {
                oDataRow["Belegt"] = 0;
                oDataRow["AnzahlBetten"] = 3;
            }
            else
            {
                oDataRow["Belegt"] = 1;
            }
            if (oDataRow["Geschlecht"] == DBNull.Value)
            {
                oDataRow["Geschlecht"] = -1;
            }

            return oDataRow;
        }

        public DataRow GetTextfeld(int nID_Texte)
        {
            string sb = @"
                SELECT 
                    ID_Texte,
                    [Text]
                FROM
                    Texte
                WHERE
                    ID_Texte=@ID_Texte
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Texte", nID_Texte));
            return GetRecord(sb, aSQLParameters, "Texte");
        }

        public DataRow GetZimmerByBett(int nID_Betten)
        {
            DataRow info = GetRecord("select ID_Zimmer from Betten where ID_Betten = " + nID_Betten, null, "Betten");

            string s =
                @"
                SELECT 
                    0 as Belegt,
                    min(Patienten.AnzahlBetten) as AnzahlBetten,
                    max(Patienten.Geschlecht) as Geschlecht
                FROM
                    Betten LEFT JOIN Patienten ON Patienten.ID_Betten = Betten.ID_Betten
                WHERE
                    Betten.ID_Zimmer = @ID_Zimmer
                group by 
                    Betten.ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", (int)info["ID_Zimmer"]));

            DataRow oDataRow = GetRecord(s, aSQLParameters, "Betten");

            // Durch den left join kann da nix rauskommen, also DBNULL, dann ist das Zimmer gar nicht belegt und hat 3 freie Betten
            if (oDataRow["AnzahlBetten"] == DBNull.Value)
            {
                oDataRow["Belegt"] = 0;
                oDataRow["AnzahlBetten"] = 3;
            }
            else
            {
                oDataRow["Belegt"] = 1;
            }
            if (oDataRow["Geschlecht"] == DBNull.Value)
            {
                oDataRow["Geschlecht"] = -1;
            }

            return oDataRow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nStation"></param>
        /// <param name="nZimmerNummer"></param>
        /// <returns>-1 nicht belegt
        /// 1 mann
        /// 0 Frau</returns>
        public int HoleZimmerGeschlecht(int nID_Zimmer)
        {
            int nGeschlecht;

            string sb = @"
                    select TOP 1 Geschlecht
                    FROM 
                        Betten inner join Patienten on Betten.ID_Betten = Patienten.ID_Betten
                    where 
                        ID_Zimmer=@ID_Zimmer
                 ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            DataRow dataRow = this.GetRecord(sb, aSQLParameters, "Betten");

            nGeschlecht = (dataRow == null) ? -1 : ((int)dataRow["Geschlecht"]);

            return nGeschlecht;
        }

        /// <summary>
        /// Liefert true, wenn mindestens ein Privatpatient im Zimmer liegt,
        /// und false, wenn keiner im ZImmer ist oder kein Privatpatient
        /// </summary>
        /// <param name="nStation"></param>
        /// <param name="nZimmerNummer"></param>
        /// <returns></returns>
        public bool IstZimmerPrivat(int nID_Zimmer)
        {
            bool bPrivat = false;

            string sb = @"
                select COUNT(Patienten.ID_Betten)
                from
                    Patienten 
                    inner join Betten on Betten.ID_Betten = Patienten.ID_Betten
                    inner join Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer
                where 
                    ID_Zimmer = @ID_Zimmer
                    and Privat = 1
                ";

            sb = sb.Replace("@ID_Zimmer", nID_Zimmer.ToString());

            long nCount = this.ExecuteScalar(sb.ToString());

            if (nCount > 0)
            {
                bPrivat = true;
            }

            return bPrivat;
        }

        /// <summary>
        /// Alle Betten holen, die noch nicht belegt sind: Alle ID_Betten holen, die nicht einem Patienten zugewiesen sind.
        /// </summary>
        /// <returns></returns>
        public DataView GetFreieBetten(int nID_Diagramme)
        {
            string sSQL =
                @"
                select 
                    ID_Betten,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer,
                    Betten.BettenNummer
                from 
                    Betten
                    inner join Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer
                where 
                    Zimmer.ID_Diagramme = @ID_Diagramme and 
                    ID_Betten not in (select distinct ID_Betten from Patienten where ID_Betten is not null)
                order by 
                    Zimmer.Station,
                    Zimmer.ZimmerNummer, 
                    Betten.BettenNummer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return GetDataView(sSQL, aSQLParameters, "Betten");
        }
        #endregion

        #region Betten
        public DataRow GetBett(int nID_Zimmer, int nBettenNummer)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    Betten.ID_Betten,
                    BettenNummer,
                    ID_Patienten
                FROM
                    Betten
                    LEFT JOIN Patienten on Betten.ID_Betten = Patienten.ID_Betten
                WHERE
                    ID_Zimmer = @ID_Zimmer
                    AND BettenNummer = @BettenNummer
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));
            aSQLParameters.Add(this.SqlParameter("@BettenNummer", nBettenNummer));

            return GetRecord(sb.ToString(), aSQLParameters, "Betten");
        }
        public DataView GetBetten(int nID_Zimmer)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    Betten.ID_Betten,
                    BettenNummer,
                    ID_Patienten
                FROM
                    Betten
                    LEFT JOIN Patienten on Betten.ID_Betten = Patienten.ID_Betten
                WHERE
                    ID_Zimmer = @ID_Zimmer
                ORDER BY
                    BettenNummer
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            return GetDataView(sb.ToString(), aSQLParameters, "Betten");
        }
        public bool UpdateBett(int nID_Betten, int nID_Patienten)
        {
            string sb = @"
                UPDATE 
                    Patienten
                SET 
                    ID_Betten = @ID_Betten
                WHERE 
                    ID_Patienten = @ID_Patienten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Betten", nID_Betten));
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", nID_Patienten));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public void DesignerDeleteBett(int _nID_Betten)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// EInem Patient ein Bett zuweisen.
        /// </summary>
        /// <param name="nStation"></param>
        /// <param name="nZimmerNummer"></param>
        /// <param name="nBettNummer"></param>
        /// <param name="nID_Patienten"></param>
        /// <returns></returns>
        public bool UpdateBettByZimmerBettNummer(int nID_Zimmer, int nBettenNummer, int nID_Patienten)
        {
            int iEffectedRecords = 0;

            string s = @"
                select 
                    ID_Betten 
                from 
                    Betten 
                where 
                    BettenNummer = @BettenNummer
                    AND ID_Zimmer = @ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@BettenNummer", nBettenNummer));
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            DataRow oBett = this.GetRecord(s, aSQLParameters, "Betten");

            if (oBett != null)
            {
                int nID_Betten = (int)oBett["ID_Betten"];

                s = @"
                UPDATE 
                    Patienten
                SET 
                    ID_Betten = @ID_Betten
                where 
                    ID_Patienten=@ID_Patienten
                ";

                aSQLParameters.Clear();
                aSQLParameters.Add(this.SqlParameter("@ID_Betten", nID_Betten));
                aSQLParameters.Add(this.SqlParameter("@ID_Patienten", nID_Patienten));

                iEffectedRecords = this.ExecuteNonQuery(s, aSQLParameters);
            }

            return (iEffectedRecords == 1);
        }

        /// <summary>
        /// Bett leeren, also dem Patient, der drin liegt, das Bett loeschen
        /// </summary>
        /// <param name="nStation"></param>
        /// <param name="nZimmerNummer"></param>
        /// <param name="nBettNummer"></param>
        /// <returns></returns>
        public bool UpdateBettByZimmerBettNummer(int nID_Zimmer, int nBettenNummer)
        {
            string s = @"
                UPDATE 
                    Patienten
                SET 
                    ID_Betten = null
                where 
                    ID_Betten = (select ID_Betten from Betten where BettenNummer = @BettenNummer and ID_Zimmer=@ID_Zimmer)
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@BettenNummer", nBettenNummer));
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            int iEffectedRecords = this.ExecuteNonQuery(s, aSQLParameters);

            return (iEffectedRecords == 1);
        }
        #endregion

        #region Diagnosen
        public DataRow GetDiagnose(int nID_Diagnosen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_Diagnosen,
                    DRG,
                    DRG_Name,
                    U_GVD,
                    M_GVD,
                    O_GVD
                FROM
                    Diagnosen
                WHERE 
                    ID_Diagnosen=@ID_Diagnosen
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagnosen", nID_Diagnosen));

            return GetRecord(sb.ToString(), aSQLParameters, "Diagnosen");
        }

        public DataView GetDiagnosen()
        {
            string sSQL =
                @"
                SELECT 
                    ID_Diagnosen,
                    DRG,
                    DRG_Name,
                    U_GVD,
                    M_GVD,
                    O_GVD,
                    ""["" + DRG + ""] "" + DRG_Name as DisplayMember
                FROM
                    Diagnosen
                ORDER BY
                    DRG_Name
                ";

            return GetDataView(sSQL, null, "Diagnosen");
        }
        public int InsertDiagnose(DataRow dataRow)
        {
            string sb =
                @"
                INSERT INTO Diagnosen
                    (
                    DRG,
                    DRG_Name,
                    U_GVD,
                    M_GVD,
                    O_GVD
                    )
                    VALUES
                    (
                    @DRG,
                    @DRG_Name,
                    @U_GVD,
                    @M_GVD,
                    @O_GVD
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@DRG", (string)dataRow["DRG"]));
            aSQLParameters.Add(this.SqlParameter("@DRG_Name", (string)dataRow["DRG_Name"]));
            aSQLParameters.Add(this.SqlParameter("@U_GVD", (int)dataRow["U_GVD"]));
            aSQLParameters.Add(this.SqlParameter("@M_GVD", (int)dataRow["M_GVD"]));
            aSQLParameters.Add(this.SqlParameter("@O_GVD", (int)dataRow["O_GVD"]));

            return InsertRecord(sb, aSQLParameters, "Diagnosen");
        }
        public bool UpdateDiagnose(DataRow dataRow)
        {
            string s = @"
                UPDATE
                    Diagnosen
                SET
                    DRG = @DRG,
                    DRG_Name = @DRG_Name,
                    U_GVD = @U_GVD,
                    M_GVD = @M_GVD,
                    O_GVD = @O_GVD
                WHERE
                    ID_Diagnosen = @ID_Diagnosen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@DRG", (string)dataRow["DRG"]));
            aSQLParameters.Add(this.SqlParameter("@DRG_Name", (string)dataRow["DRG_Name"]));
            aSQLParameters.Add(this.SqlParameter("@U_GVD", (int)dataRow["U_GVD"]));
            aSQLParameters.Add(this.SqlParameter("@M_GVD", (int)dataRow["M_GVD"]));
            aSQLParameters.Add(this.SqlParameter("@O_GVD", (int)dataRow["O_GVD"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Diagnosen", (int)dataRow["ID_Diagnosen"]));

            int iEffectedRecords = this.ExecuteNonQuery(s, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public bool DeleteDiagnose(int nID_Diagnosen)
        {
            string s = @"
                DELETE FROM
                    Diagnosen
                WHERE
                    ID_Diagnosen = @ID_Diagnosen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagnosen", nID_Diagnosen));

            int iEffectedRecords = this.ExecuteNonQuery(s, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public DataRow CreateDataRowDiagnose()
        {
            DataTable dt = new DataTable("Diagnosen");

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Diagnosen", typeof(int)));
            dt.Columns.Add(new DataColumn("DRG", typeof(string)));
            dt.Columns.Add(new DataColumn("DRG_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("U_GVD", typeof(float)));
            dt.Columns.Add(new DataColumn("M_GVD", typeof(float)));
            dt.Columns.Add(new DataColumn("O_GVD", typeof(float)));

            return dt.NewRow();
        }

        #endregion

        #region Berechnungen
        /// <summary>
        /// Zaehlt die Anzahl der freien Frauen und Maennerzimmer. Es werden nur Zimmer berücksichtigt,
        /// in denen jemand liegt.
        /// Es wird ueber alle Patienten aus einem Zimmer gruppiert.
        /// ACHTUNG: Das Datenbankfeld Geschlecht wird benutzt um sofort herauszufinden, 
        /// ob ein Zimmer ein Frauen/Maennerzimmer ist. 
        /// 0 - Frau
        /// 1 - Mann
        /// Wenn die Summe 0 ist, ist es ein Frauenzimmer,
        /// Wenn die Summe > 0 ist, ist es ein Maennerzimmer
        /// 
        /// Die Anzahl der Betten des Zimmers wird mit min(AnzahlBetten) geholt, weil AnzahlBetten
        /// immer gleich ist und min() über alle gleiche eben diese Zahl liefert.
        /// </summary>
        /// <param name="nFreieMaennerbetten"></param>
        /// <param name="nFreieFrauenbetten"></param>
        /// <returns></returns>
        public void FreieBetten(int nID_Diagramme, out long nFreieMaennerbetten, out long nFreieFrauenbetten)
        {
            nFreieMaennerbetten = 0;
            nFreieFrauenbetten = 0;

            string sSQL =
                @"
                SELECT 
                    Zimmer.Station, 
                    Zimmer.ZimmerNummer, 
                    count(Betten.ID_Betten) AS BelegteBetten, 
                    sum(Patienten.Geschlecht) AS SummeGeschlecht, 
                    min(Patienten.AnzahlBetten) AS AnzahlBetten
                FROM 
                    Patienten
                        INNER JOIN (Betten INNER JOIN Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer)
                        ON Betten.ID_Betten = Patienten.ID_Betten
                WHERE
                    Zimmer.ID_Diagramme=@ID_Diagramme
                GROUP BY 
                    Zimmer.Station,
                    Zimmer.ZimmerNummer
                ORDER BY 
                    Zimmer.Station,
                    Zimmer.ZimmerNummer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            DataView oDataView = this.GetDataView(sSQL, aSQLParameters, "Patienten");

            foreach (DataRow dataRow in oDataView.Table.Rows)
            {
                int nSum;

                // Sum gibt bei Access double zurück
                nSum = Convert.ToInt32((double)dataRow["SummeGeschlecht"]);
                if (nSum > 0)
                {
                    nFreieMaennerbetten += (int)dataRow["AnzahlBetten"] - (int)dataRow["BelegteBetten"];
                }
                else
                {
                    nFreieFrauenbetten += (int)dataRow["AnzahlBetten"] - (int)dataRow["BelegteBetten"];
                }
            }
        }

        public long FreieZimmer(int nID_Diagramme)
        {
            long nTotal;

            // Gesamtanzahl Zimmer
            string sSQL =
                @"
                    SELECT 
                        COUNT(ID_Zimmer)
                    FROM 
                        Zimmer
                    where
                        ID_Diagramme = @ID_Diagramme
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            nTotal = ExecuteScalar(sSQL, aSQLParameters);

            // Belegte Zimmer
            sSQL =
                @"
                    SELECT DISTINCT
                        Zimmer.ID_Zimmer
                    FROM
                        Zimmer
                        INNER JOIN (Betten INNER JOIN Patienten ON Betten.ID_Betten = Patienten.ID_Betten)
                        ON Betten.ID_Zimmer = Zimmer.ID_Zimmer
                    where
                        Zimmer.ID_Diagramme = @ID_Diagramme
                ";

            aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            DataView dv = this.GetDataView(sSQL, aSQLParameters, "Zimmer");
            long nBelegteZimmer = dv.Table.Rows.Count;

            return nTotal - nBelegteZimmer;
        }

        /// <summary>
        /// Verfuegbare Betten, das sind 2 in einem 2er Zimmer, 1 in einem Isolationszimmer
        /// oder 3 in einem Dreier-Zimmer
        /// Alle Patienten, die demselben Zimmer zugeordnet sind, sollten denselben Eintrag
        /// bei AnzahlBetten haben, also holt min()ueber diese maximal n Eintraege die
        /// Anzahl der Betten des Zimmers.
        /// </summary>
        /// <param name="nVerfuegbareBetten"></param>
        /// <param name="nBelegteBetten"></param>
        public void Belegung(int nID_Diagramme, out long nVerfuegbareBetten, out long nBelegteBetten)
        {
            string sSQL =
                @"
                    SELECT 
                        Zimmer.ID_Zimmer,
                        min(AnzahlBetten) as AnzahlBetten
                    FROM
                        Zimmer left join (Betten left JOIN Patienten ON Betten.ID_Betten = Patienten.ID_Betten)
                        on Betten.ID_Zimmer = Zimmer.ID_Zimmer
                    WHERE
                        Zimmer.ID_Diagramme=@ID_Diagramme
                    group by 
                        Zimmer.ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            DataView dv = GetDataView(sSQL, aSQLParameters, "Betten");

            nVerfuegbareBetten = 0;
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                if (dataRow["AnzahlBetten"] == DBNull.Value)
                {
                    dataRow["AnzahlBetten"] = 3;
                }
                nVerfuegbareBetten += Convert.ToInt32((int)dataRow["AnzahlBetten"]);
            }

            sSQL =
                @"
                    SELECT count(ID_Patienten) as Anzahl
                    FROM 
                        Patienten
                        inner join (Betten inner join Zimmer on Zimmer.ID_Zimmer = Betten.ID_Zimmer)
                        on Betten.ID_Betten=Patienten.ID_Betten
                    WHERE
                        Zimmer.ID_Diagramme=@ID_Diagramme
                ";

            aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            nBelegteBetten = this.ExecuteScalar(sSQL, aSQLParameters);
        }

        #endregion

        #region Infektionen
        public DataRow GetInfektion(int nID_Infektionen)
        {
            string sSQL =
                @"
                SELECT 
                    ID_Infektionen,
                    Name,
                    Reihenfolge,
                    Sonstiges
                FROM
                    Infektionen
                WHERE
                    ID_Infektionen=@ID_Infektionen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Infektionen", nID_Infektionen));

            return GetRecord(sSQL, aSQLParameters, "Infektionen");
        }

        public DataView GetInfektionen()
        {
            string sSQL =
                @"
                SELECT 
                    ID_Infektionen,
                    Name,
                    Reihenfolge,
                    Sonstiges
                FROM
                    Infektionen
                ORDER BY
                    Sonstiges,
                    Reihenfolge
                ";

            return this.GetDataView(sSQL, null, "Infektionen");
        }

        public DataRow CreateDataRowInfektion()
        {
            DataTable dt = new DataTable("Infektionen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Infektionen", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Sonstiges", typeof(int)));
            dt.Columns.Add(new DataColumn("Reihenfolge", typeof(int)));

            dataRow = dt.NewRow();

            return dataRow;
        }

        public bool DeleteInfektion(int nID_Infektionen)
        {
            string sb = @"
                DELETE FROM
                    Infektionen
                WHERE
                    ID_Infektionen = @ID_Infektionen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Infektionen", nID_Infektionen));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }
        public bool UpdateInfektion(DataRow dataRow)
        {
            string sb = @"
                UPDATE
                    Infektionen
                SET
                    Name = @Name,
                    Reihenfolge = @Reihenfolge,
                    Sonstiges = @Sonstiges
                WHERE
                    ID_Infektionen = @ID_Infektionen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Name", (string)dataRow["Name"]));
            aSQLParameters.Add(this.SqlParameter("@Reihenfolge", (int)dataRow["Reihenfolge"]));
            aSQLParameters.Add(this.SqlParameter("@Sonstiges", (int)dataRow["Sonstiges"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Infektionen", (int)dataRow["ID_Infektionen"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public int InsertInfektion(DataRow dataRow)
        {
            string sb =
                @"
                INSERT INTO Infektionen
                    (
                    Name,
                    Reihenfolge,
                    Sonstiges
                    )
                    VALUES
                    (
                     @Name,
                     @Reihenfolge,
                     @Sonstiges
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Name", (string)dataRow["Name"]));
            aSQLParameters.Add(this.SqlParameter("@Reihenfolge", (int)dataRow["Reihenfolge"]));
            aSQLParameters.Add(this.SqlParameter("@Sonstiges", (int)dataRow["Sonstiges"]));

            return InsertRecord(sb, aSQLParameters, "Infektionen");
        }
        #endregion

        #region PatientenInfektionen
        public DataView GetPatientenInfektionen(int nID_Patienten, string strOrderBy)
        {
            string sSQL =
                @"
                SELECT 
                    ID_PatientenInfektionen,
                    Name,
                    PatientenInfektionen.Datum,
                    Sonstiges
                FROM
                    PatientenInfektionen 
                    inner join Infektionen on Infektionen.ID_Infektionen = PatientenInfektionen.ID_Infektionen
                WHERE
                    ID_Patienten = @ID_Patienten
                ORDER BY
                    @OrderBy
                ";

            sSQL = sSQL.Replace("@OrderBy", strOrderBy);

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", nID_Patienten));

            return this.GetDataView(sSQL, aSQLParameters, "PatientenInfektionen");
        }

        public DataView GetPatientenInfektionen(int nID_Diagramme)
        {
            string sSQL =
                @"
                SELECT 
                    PatientenInfektionen.Datum,
                    Infektionen.Name as Infektionsname,
                    Patienten.Nachname,
                    Patienten.Hervorheben,
                    Zimmer.Station,
                    Zimmer.ZimmerNummer
                FROM 
                    (PatientenInfektionen 
                    INNER JOIN Infektionen ON Infektionen.ID_Infektionen = PatientenInfektionen.ID_Infektionen) 
                    INNER JOIN (Patienten INNER JOIN (Betten INNER join Zimmer on Zimmer.ID_Zimmer=Betten.ID_Zimmer)ON Patienten.ID_Betten = Betten.ID_Betten) 
                    ON Patienten.ID_Patienten = PatientenInfektionen.ID_Patienten
                WHERE
                    Zimmer.ID_Diagramme=@ID_Diagramme
                ORDER BY 
                    Infektionen.Sonstiges, 
                    Infektionen.Name
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return this.GetDataView(sSQL, aSQLParameters, "PatientenInfektionen");
        }

        public bool DeletePatientenInfektionen(int nID_PatientenInfektionen)
        {
            string sb = @"
                DELETE FROM
                    PatientenInfektionen
                WHERE
                    ID_PatientenInfektionen = @ID_PatientenInfektionen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_PatientenInfektionen", nID_PatientenInfektionen));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);

        }
        public bool DeletePatientenInfektionenForPatient(int nID_Patienten)
        {
            string sb = @"
                DELETE FROM
                    PatientenInfektionen
                WHERE
                    ID_Patienten = @ID_Patienten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", nID_Patienten));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return true;

        }
        public int InsertPatientenInfektionen(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO PatientenInfektionen
                    (
                    ID_Patienten,
                    ID_Infektionen,
                    Datum
                    )
                    VALUES
                    (
                     @ID_Patienten,
                     @ID_Infektionen,
                     @Datum
                    )
                ");


            sb.Replace("@Datum", DateTime2DBDateTimeString(oDataRow["Datum"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Patienten", (int)oDataRow["ID_Patienten"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Infektionen", (int)oDataRow["ID_Infektionen"]));

            return InsertRecord(sb.ToString(), aSQLParameters, "PatientenInfektionen");
        }

        public DataRow CreateDataRowPatientenInfektionen()
        {
            DataTable dt = new DataTable("PatientenInfektionen");

            dt.Columns.Add(new DataColumn("ID_PatientenInfektionen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Patienten", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Infektionen", typeof(int)));
            dt.Columns.Add(new DataColumn("Datum", typeof(DateTime)));

            return dt.NewRow();
        }

        #endregion

        #region Diagramme
        public DataView GetDiagramme()
        {
            string strSQL = @"
                select
                    ID_Diagramme,
                    Name,
                    Beschreibung
                from
                    Diagramme
                order by
                    ID_Diagramme
                ";

            return GetDataView(strSQL, null, "Diagramme");
        }
        public void DesignerUpdateDiagramm(DataRow row)
        {
        }

        public DataRow CreateDataRowDiagramm()
        {
            return null;
        }

        #endregion

        #region Designer
        public DataView DesignerGetBetten(int nID_Zimmer)
        {
            string strSQL = @"
                select 
                    ID_Betten,
                    ID_Zimmer,
                    BettenNummer,
                    LocationX,
                    LocationY
                from
                    Betten
                where
                    ID_Zimmer=@ID_Zimmer
                order by
                    BettenNummer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            return GetDataView(strSQL, aSQLParameters, "Betten");
        }
        public DataView DesignerGetTexte(int nID_Diagramme)
        {
            string strSQL = @"
                select
                    ID_Texte,
                    [Text],
                    LocationX,
                    LocationY,
                    Height,
                    Width
                from
                    Texte
                where
                    ID_Diagramme=@ID_Diagramme
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return GetDataView(strSQL, aSQLParameters, "Texte");
        }
        public DataView DesignerGetZimmers(int nID_Diagramme)
        {
            string strSQL = @"
                select
                    ID_Zimmer,
                    Station,
                    ZimmerNummer,
                    LocationX,
                    LocationY,
                    Height,
                    Width,
                    NummerLocationX,
                    NummerLocationY,
                    InfoLocationX,
                    InfoLocationY,
                    IsolationLocationX,
                    IsolationLocationY
                from
                    Zimmer
                where
                    ID_Diagramme=@ID_Diagramme
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return GetDataView(strSQL, aSQLParameters, "Zimmer");
        }
        public DataRow DesignerGetDiagramm(int nID_Diagramme)
        {
            string strSQL = @"
                select 
                    ID_Diagramme,
                    Name,
                    Beschreibung,
                    InfoLocationX,
                    InfoLocationY
                from
                    Diagramme
                where
                    ID_Diagramme=@ID_Diagramme
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return GetRecord(strSQL, aSQLParameters, "Diagramme");
        }
        public long GetDiagrammeCount()
        {
            string sb = @"
                select 
                    COUNT(ID_Diagramme)
                from
                    Diagramme 
                ";

            long nCount = this.ExecuteScalar(sb);

            return nCount;
        }

        #endregion

        #region Designer
        public DataView DesignerGetZimmerForDiagramm(int nID_Diagramme)
        {
            string strSQL = @"
                select
                    ID_Zimmer,
                    Station,
                    ZimmerNummer,
                    LocationX,
                    LocationY,
                    Height,
                    Width,
                    NummerLocationX,
                    NummerLocationY,
                    InfoLocationX,
                    InfoLocationY,
                    IsolationLocationX,
                    IsolationLocationY
                from
                    Zimmer
                where
                    ID_Diagramme=@ID_Diagramme
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return GetDataView(strSQL, aSQLParameters, "Zimmer");
        }

        #endregion


        #region Designer
        public DataView DesignerGetTextfelderForDiagramm(int nID_Diagramme)
        {
            string strSQL = @"
                select
                    ID_Texte,
                    Text,
                    LocationX,
                    LocationY,
                    Height,
                    Width
                from
                    Texte
                where
                    ID_Diagramme=@ID_Diagramme
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));

            return GetDataView(strSQL, aSQLParameters, "Texte");
        }

        public DataRow DesignerGetTextfeld(int nID_Texte)
        {
            string strSQL = @"
                select
                    ID_Texte,
                    Text,
                    LocationX,
                    LocationY,
                    Height,
                    Width
                from
                    Texte
                where
                    ID_Texte=@ID_Texte
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Texte", nID_Texte));

            return GetRecord(strSQL, aSQLParameters, "Texte");
        }
        public DataRow DesignerGetZimmer(int nID_Zimmer)
        {
            string strSQL = @"
                select
                    ID_Zimmer,
                    Station,
                    ZimmerNummer,
                    LocationX,
                    LocationY,
                    Height,
                    Width,
                    NummerLocationX,
                    NummerLocationY,
                    InfoLocationX,
                    InfoLocationY,
                    IsolationLocationX,
                    IsolationLocationY
                from
                    Zimmer
                where
                    ID_Zimmer=@ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            return GetRecord(strSQL, aSQLParameters, "Zimmer");
        }

        public DataRow DesignerGetBett(int nID_Betten)
        {
            string strSQL = @"
                select 
                    ID_Betten,
                    ID_Zimmer,
                    BettenNummer,
                    LocationX,
                    LocationY
                from
                    Betten
                where
                    ID_Betten=@ID_Betten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Betten", nID_Betten));

            return GetRecord(strSQL, aSQLParameters, "Betten");
        }

        public DataView DesignerGetDiagramme()
        {
            string strSQL = @"
                select 
                    ID_Diagramme,
                    Name,
                    Beschreibung,
                    InfoLocationX,
                    InfoLocationY
                from
                    Diagramme
                order by
                    ID_Diagramme
                ";

            return GetDataView(strSQL, null, "Diagramme");
        }
        public int DesignerInsertTextfeld(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Texte
                    (
                    ID_Diagramme,
                    [Text],
                    LocationX,
                    LocationY,
                    Width,
                    Height
                    )
                    VALUES
                    (
                    @ID_Diagramme,
                    @Text,
                    @LocationX,
                    @LocationY,
                    @Width,
                    @Height
                    )
                ");


            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", (int)oDataRow["ID_Diagramme"]));
            aSQLParameters.Add(this.SqlParameter("@Text", (string)oDataRow["Text"]));
            aSQLParameters.Add(this.SqlParameter("@LocationX", (int)oDataRow["LocationX"]));
            aSQLParameters.Add(this.SqlParameter("@LocationY", (int)oDataRow["LocationY"]));
            aSQLParameters.Add(this.SqlParameter("@Width", (int)oDataRow["Width"]));
            aSQLParameters.Add(this.SqlParameter("@Height", (int)oDataRow["Height"]));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "Texte");
        }
        public int DesignerInsertZimmer(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Zimmer
                    (
                    ID_Diagramme,
                    Station,
                    ZimmerNummer,
                    LocationX,
                    LocationY,
                    Width,
                    Height,
                    NummerLocationX,
                    NummerLocationY,
                    InfoLocationX,
                    InfoLocationY,
                    IsolationLocationX,
                    IsolationLocationY
                    )
                    VALUES
                    (
                    @ID_Diagramme,
                    @Station,
                    @ZimmerNummer,
                    @LocationX,
                    @LocationY,
                    @Width,
                    @Height,
                    @NummerLocationX,
                    @NummerLocationY,
                    @InfoLocationX,
                    @InfoLocationY,
                    @IsolationLocationX,
                    @IsolationLocationY
                    )
                ");


            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", (int)oDataRow["ID_Diagramme"]));
            aSQLParameters.Add(this.SqlParameter("@Station", (int)oDataRow["Station"]));
            aSQLParameters.Add(this.SqlParameter("@ZimmerNummer", (int)oDataRow["ZimmerNummer"]));
            aSQLParameters.Add(this.SqlParameter("@LocationX", (int)oDataRow["LocationX"]));
            aSQLParameters.Add(this.SqlParameter("@LocationY", (int)oDataRow["LocationY"]));
            aSQLParameters.Add(this.SqlParameter("@Width", (int)oDataRow["Width"]));
            aSQLParameters.Add(this.SqlParameter("@Height", (int)oDataRow["Height"]));
            aSQLParameters.Add(this.SqlParameter("@NummerLocationX", (int)oDataRow["NummerLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@NummerLocationY", (int)oDataRow["NummerLocationY"]));
            aSQLParameters.Add(this.SqlParameter("@InfoLocationX", (int)oDataRow["InfoLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@InfoLocationY", (int)oDataRow["InfoLocationY"]));
            aSQLParameters.Add(this.SqlParameter("@IsolationLocationX", (int)oDataRow["IsolationLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@IsolationLocationY", (int)oDataRow["IsolationLocationY"]));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "Zimmer");
        }
        public int DesignerUpdateTextfeld(DataRow oDataRow)
        {
            string s = @"
                UPDATE Texte SET
                    [Text]=@Text,
                    LocationX=@LocationX,
                    LocationY=@LocationY,
                    Width=@Width,
                    Height=@Height
                WHERE
                    ID_Texte=@ID_Texte
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Text", (string)oDataRow["Text"]));
            aSQLParameters.Add(this.SqlParameter("@LocationX", (int)oDataRow["LocationX"]));
            aSQLParameters.Add(this.SqlParameter("@LocationY", (int)oDataRow["LocationY"]));
            aSQLParameters.Add(this.SqlParameter("@Width", (int)oDataRow["Width"]));
            aSQLParameters.Add(this.SqlParameter("@Height", (int)oDataRow["Height"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Texte", (int)oDataRow["ID_Texte"]));

            return this.ExecuteNonQuery(s, aSQLParameters);
        }
        public int DesignerUpdateZimmer(DataRow oDataRow)
        {
            string s = @"
                UPDATE Zimmer SET
                    Station=@Station,
                    ZimmerNummer=@ZimmerNummer,
                    LocationX=@LocationX,
                    LocationY=@LocationY,
                    Width=@Width,
                    Height=@Height,
                    NummerLocationX=@NummerLocationX,
                    NummerLocationY=@NummerLocationY,
                    InfoLocationX=@InfoLocationX,
                    InfoLocationY=@InfoLocationY,
                    IsolationLocationX=@IsolationLocationX,
                    IsolationLocationY=@IsolationLocationY
                WHERE
                    ID_Zimmer=@ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Station", (int)oDataRow["Station"]));
            aSQLParameters.Add(this.SqlParameter("@ZimmerNummer", (int)oDataRow["ZimmerNummer"]));
            aSQLParameters.Add(this.SqlParameter("@LocationX", (int)oDataRow["LocationX"]));
            aSQLParameters.Add(this.SqlParameter("@LocationY", (int)oDataRow["LocationY"]));
            aSQLParameters.Add(this.SqlParameter("@Width", (int)oDataRow["Width"]));
            aSQLParameters.Add(this.SqlParameter("@Height", (int)oDataRow["Height"]));
            aSQLParameters.Add(this.SqlParameter("@NummerLocationX", (int)oDataRow["NummerLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@NummerLocationY", (int)oDataRow["NummerLocationY"]));
            aSQLParameters.Add(this.SqlParameter("@InfoLocationX", (int)oDataRow["InfoLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@InfoLocationY", (int)oDataRow["InfoLocationY"]));
            aSQLParameters.Add(this.SqlParameter("@IsolationLocationX", (int)oDataRow["IsolationLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@IsolationLocationY", (int)oDataRow["IsolationLocationY"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", (int)oDataRow["ID_Zimmer"]));

            return this.ExecuteNonQuery(s, aSQLParameters);
        }
        public int DesignerInsertBett(DataRow oDataRow)
        {
            string s = @"
                INSERT INTO Betten
                    (
                    ID_Zimmer,
                    BettenNummer,
                    LocationX,
                    LocationY
                    )
                    VALUES
                    (
                    @ID_Zimmer,
                    @BettenNummer,
                    @LocationX,
                    @LocationY
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", (int)oDataRow["ID_Zimmer"]));
            aSQLParameters.Add(this.SqlParameter("@BettenNummer", (int)oDataRow["BettenNummer"]));
            aSQLParameters.Add(this.SqlParameter("@LocationX", (int)oDataRow["LocationX"]));
            aSQLParameters.Add(this.SqlParameter("@LocationY", (int)oDataRow["LocationY"]));

            return this.InsertRecord(s, aSQLParameters, "Betten");
        }
        public int DesignerUpdateBett(DataRow oDataRow)
        {
            string s = @"
                UPDATE Betten SET
                    BettenNummer=@BettenNummer,
                    LocationX=@LocationX,
                    LocationY=@LocationY
                WHERE
                    ID_Betten=@ID_Betten
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@BettenNummer", (int)oDataRow["BettenNummer"]));
            aSQLParameters.Add(this.SqlParameter("@LocationX", (int)oDataRow["LocationX"]));
            aSQLParameters.Add(this.SqlParameter("@LocationY", (int)oDataRow["LocationY"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Betten", (int)oDataRow["ID_Betten"]));

            return this.ExecuteNonQuery(s, aSQLParameters);
        }
        public bool DesignerDeleteBetten(int nID_Zimmer)
        {
            string sb = @"
                DELETE FROM
                    Betten
                WHERE
                    ID_Zimmer = @ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return true;
        }
        public bool DesignerDeleteTextfeld(int nID_Texte)
        {
            string sb = @"
                DELETE FROM
                    Texte
                WHERE
                    ID_Texte = @ID_Texte
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Texte", nID_Texte));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }
        public bool DesignerDeleteZimmer(int nID_Zimmer)
        {
            string sb = @"
                DELETE FROM
                    Zimmer
                WHERE
                    ID_Zimmer = @ID_Zimmer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Zimmer", nID_Zimmer));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        /// <summary>
        ///  Loescht alle Betten, Zimmer und Diagramme.
        /// <br>Vorher werden alle Patienten aus den Betten entfernt.
        /// </br>
        /// </summary>
        /// <param name="nID_Diagramme"></param>
        /// <returns></returns>
        public bool DesignerDeleteDiagrammComplete(int nID_Diagramme)
        {
            // Alle Patienten aus den Betten entfernen
            string s = "UPDATE Patienten set ID_Betten = null";
            ExecuteNonQuery(s, null);

            // Alle Betten loeschen
            s = @"
                DELETE FROM 
                    Betten 
                WHERE 
                    ID_Zimmer in (select ID_Zimmer from Zimmer where ID_Diagramme=@ID_Diagramme)
                ";
            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            ExecuteNonQuery(s, aSQLParameters);

            // Alle Texte loeschen
            s = @"
                DELETE FROM 
                    Texte 
                WHERE 
                    ID_Diagramme=@ID_Diagramme
                ";
            aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            ExecuteNonQuery(s, aSQLParameters);

            // Alle Zimmer loeschen
            s = @"
                DELETE FROM 
                    Zimmer 
                WHERE 
                    ID_Diagramme=@ID_Diagramme
                ";
            aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            ExecuteNonQuery(s, aSQLParameters);

            // Diagramm loeschen
            s = @"
                DELETE FROM
                    Diagramme
                WHERE
                    ID_Diagramme = @ID_Diagramme
                ";
            aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Diagramme", nID_Diagramme));
            int iEffectedRecords = this.ExecuteNonQuery(s, aSQLParameters);

            return (iEffectedRecords == 1);
        }


        public DataRow DesignerGetDesignerSettings()
        {
            string strSQL = @"
                select
                    SnapToGrid,
                    ShowGrid,
                    GridSize
                from
                    DesignerSettings
                ";

            return GetRecord(strSQL, null, "DesignerSettings");
        }

        public int UpdateDesignerSettings(DataRow oDataRow)
        {
            // Delete key
            string s = "delete from DesignerSettings";
            ExecuteNonQuery(s, null);

            // insert key
            s = @"
                insert into DesignerSettings (
                    SnapToGrid,
                    ShowGrid,
                    GridSize
                    )
                values (
                    @SnapToGrid,
                    @ShowGrid,
                    @GridSize
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@SnapToGrid", (int)oDataRow["SnapToGrid"]));
            aSQLParameters.Add(this.SqlParameter("@ShowGrid", (int)oDataRow["ShowGrid"]));
            aSQLParameters.Add(this.SqlParameter("@GridSize", (int)oDataRow["GridSize"]));
            return InsertRecord(s, aSQLParameters, "DesignerSettings");
        }

        public int DesignerInsertDiagramm(DataRow oDataRow)
        {
            string s = @"
                INSERT INTO Diagramme
                    (
                    Name,
                    Beschreibung,
                    InfoLocationX,
                    InfoLocationY
                    )
                    VALUES
                    (
                    @Name,
                    @Beschreibung,
                    @InfoLocationX,
                    @InfoLocationY
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Name", (string)oDataRow["Name"]));
            aSQLParameters.Add(this.SqlParameter("@Beschreibung", (string)oDataRow["Beschreibung"]));
            aSQLParameters.Add(this.SqlParameter("@InfoLocationX", (int)oDataRow["InfoLocationX"]));
            aSQLParameters.Add(this.SqlParameter("@InfoLocationY", (int)oDataRow["InfoLocationY"]));

            return this.InsertRecord(s, aSQLParameters, "Diagramme");
        }
        public DataRow CreateDataRowTextfeld()
        {
            DataTable dt = new DataTable("Texte");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Texte", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Diagramme", typeof(int)));
            dt.Columns.Add(new DataColumn("Text", typeof(string)));
            dt.Columns.Add(new DataColumn("LocationX", typeof(int)));
            dt.Columns.Add(new DataColumn("LocationY", typeof(int)));
            dt.Columns.Add(new DataColumn("Width", typeof(int)));
            dt.Columns.Add(new DataColumn("Height", typeof(int)));

            dataRow = dt.NewRow();

            dataRow["ID_Texte"] = -1;
            dataRow["ID_Diagramme"] = -1;
            dataRow["Text"] = "Text";
            dataRow["LocationX"] = 50;
            dataRow["LocationY"] = 50;
            dataRow["Width"] = 80;
            dataRow["Height"] = 16;

            return dataRow;
        }
        public DataRow CreateDataRowZimmer()
        {
            DataTable dt = new DataTable("Zimmer");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Zimmer", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Diagramme", typeof(int)));
            dt.Columns.Add(new DataColumn("Station", typeof(int)));
            dt.Columns.Add(new DataColumn("ZimmerNummer", typeof(int)));
            dt.Columns.Add(new DataColumn("LocationX", typeof(int)));
            dt.Columns.Add(new DataColumn("LocationY", typeof(int)));
            dt.Columns.Add(new DataColumn("Width", typeof(int)));
            dt.Columns.Add(new DataColumn("Height", typeof(int)));
            dt.Columns.Add(new DataColumn("NummerLocationX", typeof(int)));
            dt.Columns.Add(new DataColumn("NummerLocationY", typeof(int)));
            dt.Columns.Add(new DataColumn("InfoLocationX", typeof(int)));
            dt.Columns.Add(new DataColumn("InfoLocationY", typeof(int)));
            dt.Columns.Add(new DataColumn("IsolationLocationX", typeof(int)));
            dt.Columns.Add(new DataColumn("IsolationLocationY", typeof(int)));

            dataRow = dt.NewRow();

            dataRow["ID_Zimmer"] = -1;
            dataRow["ID_Diagramme"] = -1;
            dataRow["Station"] = 0;
            dataRow["ZimmerNummer"] = 0;
            dataRow["LocationX"] = 50;
            dataRow["LocationY"] = 50;
            dataRow["Width"] = 123;
            dataRow["Height"] = 88;
            dataRow["NummerLocationX"] = 190;
            dataRow["NummerLocationY"] = 88;
            dataRow["InfoLocationX"] = 3;
            dataRow["InfoLocationY"] = 36;
            dataRow["IsolationLocationX"] = 40;
            dataRow["IsolationLocationY"] = 3;

            return dataRow;
        }
        public DataRow CreateDataRowBett()
        {
            DataTable dt = new DataTable("Betten");

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Betten", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Zimmer", typeof(int)));
            dt.Columns.Add(new DataColumn("BettenNummer", typeof(int)));
            dt.Columns.Add(new DataColumn("LocationX", typeof(int)));
            dt.Columns.Add(new DataColumn("LocationY", typeof(int)));

            DataRow dataRow = dt.NewRow();

            return dataRow;
        }

        #endregion
    }
}

