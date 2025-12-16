using System;
using System.Data;
using System.Resources;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using AppFramework;
//using Utility;
//using Security;
//using Security.Cryptography;

namespace Station.AppFramework
{
    public abstract class BusinessLayerCommon : BusinessLayerBase, ISecurityManager
    {
        public const int VERSION_MAJOR = 1;
        public const int VERSION_MINOR = 4;
        public const string VERSION_DATE = "(17.05.2010)";

        protected string _strPassword;

        protected const string INSERT = "insert";
        protected const string UPDATE = "update";
        protected const string DELETE = "delete";

        protected string _strUser = "";

        DataView _oDiagnosen;
        DataView _oInfektionen;

        public const int AbortLoopCount = 50;

        protected DatabaseLayerCommon _databaseLayerCommon;

        /// <summary>
        /// VersionMajor/VersionMinor müssen mit der Datenbankversion übereinstimmen
        /// ReleaseMajor ändert sich, wenn sich nur das Programm, nicht aber 
        /// die Datenbankstruktur ändert.
        /// </summary>
        /// 1.7.7

        /// <summary>
        /// Wird verwendet, um die Werte symmetrisch zu verschlüsseln,
        /// die man nicht direkt sehen soll
        /// </summary>
        private const string Salt = "q$r%ju&*l()1#$1ef!C!ECW!DC$@RGvqdcqef';1lf!EC!ec";

        //
        // operationen123
        //
        protected const string _passwordCypher = "gEdMGrmfaOBd96L4ypdFBA==";
        public string _password;

        //
        // FOSS Elegant UI 3.3.0.0: '9F41-B0B5-2756-D3D1-5CB5-B3DB-EBC5-2EB1'
        // _elegantUiKeyCypher = "Snzjo+zAeqrr7GJSBTWm47I/Ruz3DnXq2OubjlCzkvJ09l8OAGKZgP+pOktjK6Hm";
        //
        // FOSS Elegant UI 3.4.0.0: '790D-75BF-E4E8-BECF-3A91-DE38-36AF-4F2A'
        // _elegantUiKeyCypher = "6qaSvM0ZgnjdQfRe7LA3xs4N2rbiVxqym64gnfLJ7zpeTyjFUFz3IUrd7cNlsQTW";
        //
        protected const string _elegantUiKeyCypher = "Snzjo+zAeqrr7GJSBTWm47I/Ruz3DnXq2OubjlCzkvJ09l8OAGKZgP+pOktjK6Hm";
        protected string _elegantUiKey;

        public const string ProgramTitel = "OP-LOG";

        public const int OperationQuelleIntern = 0;
        public const int OperationQuelleExtern = 1;
        public const int OperationQuelleAlle = 2;

        public const int ID_Alle = -100;

        protected DataRow _currentUser;
        protected ArrayList _currentUserRights;

        protected DataView _chirurgenFunktionen;

        private ProgressEventArgs _progressEventArgs = new ProgressEventArgs();

        public event ProgressCallback Progress;


        public BusinessLayerCommon(ResourceManager resourceManager)
            : base(resourceManager, VERSION_MAJOR, VERSION_MINOR, VERSION_DATE, ProgramTitel)
        {
        }

        //abstract public string GetDatabasePath();

        protected void FireProgressEvent(ProgressEventArgs e)
        {
            if (Progress != null)
            {
                Progress(e);
            }
        }

        public string Password
        {
            get { return _password; }
        }

        public ArrayList CurrentUserRights
        {
            get { return _currentUserRights; }
        }

        public DataRow CurrentUser
        {
            get { return _currentUser; }
        }
        public int CurrentUser_ID_Chirurgen
        {
            get { return ConvertToInt32(_currentUser["ID_Chirurgen"]); }
        }
        public string CurrentUser_Nachname
        {
            get { return (string)_currentUser["Nachname"]; }
        }
        public string CurrentUser_UserID
        {
            get { return (string)_currentUser["UserID"]; }
        }
        public bool CurrentUser_IstWeiterbilder
        {
            get { return 1 == ConvertToInt32(_currentUser["IstWeiterbilder"]); }
        }

        public DatabaseLayerCommon DatabaseLayerCommon
        {
            get { return _databaseLayerCommon; }
        }

        public override void SetDatabaseLayer(DatabaseLayerBase databaseLayer)
        {
            base.SetDatabaseLayer(databaseLayer);
            _databaseLayerCommon = (DatabaseLayerCommon)databaseLayer;
        }


        public bool UserHasRight(string right)
        {
            return true;
        }

        public Label FindNearestLabel(Control control)
        {
            return null;
        }

        public string PathDatabase
        {
            get { return DatabasePath; }
        }

        private void ErrorMessage(string s)
        {
            MessageBox.Show(s);
        }

        abstract protected DatabaseLayerCommon CreateDatabaseAccess(string s);

        public bool InitializeAccessDb(string databasePath, string password)
        {
            bool bSuccess = false;

            try
            {
                _strUser = Environment.UserName;
                AppPath = databasePath;
                ServerDir = databasePath;

                _strPassword = password;

                DatabasePath = System.IO.Path.GetFullPath(ServerDir + "\\station.mdb");
                ServerDir = System.IO.Path.GetFullPath(ServerDir);

                if (!File.Exists(DatabasePath))
                {
                    ErrorMessage("Die Datenbankdatei \n\n'" + DatabasePath + "'\n\nfehlt, das Programm kann daher nicht gestartet werden.\nWenden Sie sich mit dieser Meldung an Ihren Systemadministrator.");
                }
                else
                {
                    //string strDataSource = "Provider=Microsoft.Jet.OLEDB.4.0; Jet OLEDB:Database Password=" + _strPassword + "; Data Source=" + DatabasePath;
                    string strDataSource = "Provider=Microsoft.ACE.OLEDB.12.0; Jet OLEDB:Database Password=" + _strPassword + "; Data Source=" + DatabasePath;
                    // Microsoft.ACE.OLEDB.12.0

                    SetDatabaseLayer(CreateDatabaseAccess(strDataSource));

                    int nDatabaseMajor = Int32.Parse((string)_databaseLayerCommon.GetConfig("MajorVersion")["Value"]);
                    int nDatabaseMinor = Int32.Parse((string)_databaseLayerCommon.GetConfig("MinorVersion")["Value"]);

                    DatabaseVersion = nDatabaseMajor + "." + nDatabaseMinor;
                    if (nDatabaseMajor == VERSION_MAJOR && nDatabaseMinor == VERSION_MINOR)
                    {
                        bSuccess = true;
                    }
                    else
                    {
                        ErrorMessage("Datenbankversion und Programmversion stimmen nicht überein, das Programm kann daher nicht gestartet werden."
                            + "\n\nDatenbankversion: " + nDatabaseMajor + "." + nDatabaseMinor
                            + "\nDatenbankname: " + DatabasePath
                            + "\nProgrammversion: " + VERSION_MAJOR + "." + VERSION_MINOR + " " + VERSION_DATE
                            + "\n\nNotieren Sie diese Werte und wenden Sie sich an den Systemadministrator.");
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage("\n"
                    + "Eventuell kann die Datenbankdatei 'Station.mdb' nicht gefunden werden.\n\n"
                    + "Wenden Sie sich mit dieser Fehlermeldung an Ihren Systemadministrator\n\n"
                    + e.ToString());
            }

            return bSuccess;
        }

        public bool InitializeSQLServer(string strAppPath, string strServerPath, string connectionString)
        {
            bool bSuccess = false;
            return bSuccess;
        }

        public bool InitializeMySql(string strAppPath, string strServerPath, string connectionString)
        {
            bool bSuccess = false;
            return bSuccess;
        }

        public string PathLogfiles
        {
            get { return ServerDir + Path.DirectorySeparatorChar + "logfiles"; }
        }

        public string PathPlugins
        {
            get { return AppPath + Path.DirectorySeparatorChar + "plugins"; }
        }


        public override string AppTitle()
        {
            return "Stationsplaner";
        }
        public override string AppTitle(string text)
        {
            return text + " - Stationsplaner";
        }


        #region Common

        public void Write2Log(string strAction, string strMessage)
        {
            _databaseLayerCommon.Write2Log(_strUser, strAction, strMessage);
        }


        #endregion

        public string AppAndVersionString
        {
            get
            {
                return AppTitle() + " " + VERSION_MAJOR + "." + VERSION_MINOR + " " + VERSION_DATE;
            }
        }
        public string VersionString
        {
            get
            {
                return VERSION_MAJOR + "." + VERSION_MINOR + " " + VERSION_DATE;
            }
        }

        #region Patienten
        public DataRow CreateDataRowPatient()
        {
            return _databaseLayerCommon.CreateDataRowPatient();
        }
        public int GetID_Patienten(string strNachname, string strVorname, DateTime dtGebDatum)
        {
            return _databaseLayerCommon.GetID_Patienten(strNachname, strVorname, dtGebDatum);
        }

        public string GetPatientDiagnose(DataRow oPatient)
        {
            string strDiagnose;

            if (oPatient["ID_Diagnosen"] == DBNull.Value)
            {
                strDiagnose = oPatient["Diagnose"].ToString();
            }
            else
            {
                strDiagnose = oPatient["DiagnoseDiagnose"].ToString();
            }
            return strDiagnose;
        }

        public DataRow GetPatient(int iID)
        {
            return _databaseLayerCommon.GetPatient(iID);
        }

        public DataRow GetBettPatient(int nID_Betten)
        {
            return _databaseLayerCommon.GetBettPatient(nID_Betten);
        }


        public DataRow GetPatientNachnameAnzahlBettenByBett(int nID_Betten)
        {
            return _databaseLayerCommon.GetPatientNachnameAnzahlBettenByBett(nID_Betten);
        }
        public DataRow GetPatientByBett(int nID_Betten)
        {
            return _databaseLayerCommon.GetPatientByBett(nID_Betten);
        }

#if false
        public DataView GetZimmerPatienten(int nStation, int nZimmerNummer)
        {
            return _databaseLayerCommon.GetZimmerPatienten(nStation, nZimmerNummer);
        }
#endif
        public DataView GetPatienten()
        {
            return _databaseLayerCommon.GetPatienten();
        }
        public DataView GetPatientenOrderBy(string strOrderBy)
        {
            return _databaseLayerCommon.GetPatientenOrderBy(strOrderBy);
        }
        public DataView GetPatientenByNachname(string strNachname)
        {
            return _databaseLayerCommon.GetPatientenByNachname(strNachname);
        }

        public void PatientEntlassen(int nID_Patienten)
        {
            DataRow oDataRow = _databaseLayerCommon.GetPatient(nID_Patienten);
            this.Write2Log(DELETE, "Patient: " + (string)oDataRow["Nachname"]);

            _databaseLayerCommon.DeletePatientenInfektionenForPatient(nID_Patienten);
            _databaseLayerCommon.DeletePatient(nID_Patienten);
        }

        public int InsertPatient(DataRow oDataRow)
        {
            this.Write2Log(INSERT, "Patient: " + (string)oDataRow["Nachname"]);

            return _databaseLayerCommon.InsertPatient(oDataRow);
        }
        public bool UpdatePatient(DataRow oDataRow)
        {
            this.Write2Log(UPDATE, "Patient: " + (string)oDataRow["Nachname"]);

            return _databaseLayerCommon.UpdatePatient(oDataRow);
        }

        #endregion

        #region Betten
        public DataRow GetBett(int nID_Zimmer, int nBettenNummer)
        {
            return _databaseLayerCommon.GetBett(nID_Zimmer, nBettenNummer);
        }
        public DataView GetBetten(int nID_Zimmer)
        {
            return _databaseLayerCommon.GetBetten(nID_Zimmer);
        }
        public bool UpdateBett(int nID_Betten, int nID_Patienten)
        {
            DataRow row = GetPatient(nID_Patienten);
            this.Write2Log(UPDATE, "Bett: " + nID_Betten + " - " + (string)row["Nachname"]);

            return _databaseLayerCommon.UpdateBett(nID_Betten, nID_Patienten);
        }

        public bool UpdateBettByZimmerBettNummer(int nID_Zimmer, int nBettenNummer, int nID_Patienten)
        {
            DataRow row = GetPatient(nID_Patienten);
            this.Write2Log(UPDATE, "Bett: " + nBettenNummer + " - " + (string)row["Nachname"]);

            return _databaseLayerCommon.UpdateBettByZimmerBettNummer(nID_Zimmer, nBettenNummer, nID_Patienten);
        }
        public bool UpdateBettByZimmerBettNummer(int nID_Zimmer, int nBettenNummer)
        {
            return _databaseLayerCommon.UpdateBettByZimmerBettNummer(nID_Zimmer, nBettenNummer);
        }
        public DataView GetFreieBetten(int nID_Diagramme)
        {
            return _databaseLayerCommon.GetFreieBetten(nID_Diagramme);
        }

        #endregion
        #region Zimmer
        public bool IstZimmerPrivat(int nID_Zimmer)
        {
            return _databaseLayerCommon.IstZimmerPrivat(nID_Zimmer);
        }
        public int HoleZimmerGeschlecht(int nID_Zimmer)
        {
            return _databaseLayerCommon.HoleZimmerGeschlecht(nID_Zimmer);
        }

        public DataRow GetZimmer(int nID_Zimmer)
        {
            return _databaseLayerCommon.GetZimmer(nID_Zimmer);
        }
        public DataRow GetTextfeld(int nID_Texte)
        {
            return _databaseLayerCommon.GetTextfeld(nID_Texte);
        }

        public bool GetZimmerMinMaxAnzahlBetten(int nID_Zimmer, out bool bBelegt, out int nMin, out int nMax)
        {
            return _databaseLayerCommon.GetZimmerMinMaxAnzahlBetten(nID_Zimmer, out bBelegt, out nMin, out nMax);
        }

        public DataRow GetZimmerByBett(int nID_Betten)
        {
            return _databaseLayerCommon.GetZimmerByBett(nID_Betten);
        }

        #endregion

        #region  Diagnosen
        public DataRow CreateDataRowDiagnose()
        {
            return _databaseLayerCommon.CreateDataRowDiagnose();
        }

        public DataRow GetDiagnose(int nID_Diagnosen)
        {
            return _databaseLayerCommon.GetDiagnose(nID_Diagnosen);
        }
        public DataView GetDiagnosen()
        {
            if (this._oDiagnosen == null)
            {
                _oDiagnosen = _databaseLayerCommon.GetDiagnosen();
            }

            return _oDiagnosen;
        }
        public bool DeleteDiagnose(int nID_Diagnosen)
        {
            DataRow row = GetDiagnose(nID_Diagnosen);
            Write2Log(DELETE, "Diagnose: " + (string)row["DRG_Name"]);

            _oDiagnosen = null;
            return _databaseLayerCommon.DeleteDiagnose(nID_Diagnosen);
        }
        public bool UpdateDiagnose(DataRow dataRow)
        {
            Write2Log(UPDATE, "Diagnose: " + (string)dataRow["DRG_Name"]);

            _oDiagnosen = null;
            return _databaseLayerCommon.UpdateDiagnose(dataRow);
        }
        public int InsertDiagnose(DataRow dataRow)
        {
            Write2Log(INSERT, "Diagnose: " + (string)dataRow["DRG_Name"]);

            _oDiagnosen = null;
            return _databaseLayerCommon.InsertDiagnose(dataRow);
        }

        #endregion

        #region Infektionen
        public DataView GetInfektionen()
        {
            if (_oInfektionen == null)
            {
                _oInfektionen = _databaseLayerCommon.GetInfektionen();
            }

            return _oInfektionen;
        }
        public bool DeleteInfektion(int nID_Infektionen)
        {
            DataRow row = _databaseLayerCommon.GetInfektion(nID_Infektionen);
            Write2Log(DELETE, "Infektionen: " + (string)row["Name"]);

            _oInfektionen = null;
            return _databaseLayerCommon.DeleteInfektion(nID_Infektionen);
        }
        public bool UpdateInfektion(DataRow dataRow)
        {
            Write2Log(UPDATE, "Infektionen: " + (string)dataRow["Name"]);

            _oInfektionen = null;
            return _databaseLayerCommon.UpdateInfektion(dataRow);
        }
        public int InsertInfektion(DataRow dataRow)
        {
            Write2Log(INSERT, "Infektionen: " + (string)dataRow["Name"]);

            _oInfektionen = null;
            return _databaseLayerCommon.InsertInfektion(dataRow);
        }
        public DataRow CreateDataRowInfektion()
        {
            return _databaseLayerCommon.CreateDataRowInfektion();
        }

        #endregion

        #region PatientenInfektionen
        public DataView GetPatientenInfektionen(int nID_Diagramme)
        {
            return _databaseLayerCommon.GetPatientenInfektionen(nID_Diagramme);
        }
        public DataView GetPatientenInfektionen(int nID_Patienten, string strOrderBy)
        {
            return _databaseLayerCommon.GetPatientenInfektionen(nID_Patienten, strOrderBy);
        }
        public bool DeletePatientenInfektionen(int nID_PatientenInfektionen)
        {
            return _databaseLayerCommon.DeletePatientenInfektionen(nID_PatientenInfektionen);
        }
        public int GetPatientenAnzahlMitInfektion(string strID)
        {
            return _databaseLayerCommon.GetPatientenAnzahlMitInfektion(strID);
        }
        public int InsertPatientenInfektionen(DataRow dataRow)
        {
            return _databaseLayerCommon.InsertPatientenInfektionen(dataRow);
        }

        public DataRow CreateDataRowPatientenInfektionen()
        {
            return _databaseLayerCommon.CreateDataRowPatientenInfektionen();
        }

        #endregion
        #region  Various
        public void FreieBetten(int nID_Diagramme, out long nFreieMaennerbetten, out long nFreieFrauenbetten)
        {
            _databaseLayerCommon.FreieBetten(nID_Diagramme, out nFreieMaennerbetten, out nFreieFrauenbetten);
        }
        public long FreieZimmer(int nID_Diagramme)
        {
            return _databaseLayerCommon.FreieZimmer(nID_Diagramme);
        }
        public void Belegung(int nID_Diagramme, out long nVerfuegbareBetten, out long nBelegteBetten)
        {
            _databaseLayerCommon.Belegung(nID_Diagramme, out nVerfuegbareBetten, out nBelegteBetten);
        }

        /// <summary>
        /// Returns true if Patient was moved and Form must be redrawn
        /// </summary>
        /// <param name="nID_BettenFrom"></param>
        /// <param name="nID_BettenTo"></param>
        /// <returns></returns>
        public bool MovePatient(int nID_BettenFrom, int nID_BettenTo)
        {
            bool bSuccess = true;

            if (nID_BettenFrom == nID_BettenTo)
            {
                bSuccess = false;
            }
            else
            {
                DataRow oZimmerDst = GetZimmerByBett(nID_BettenTo);

                DataRow oPatientSrc = GetPatientNachnameAnzahlBettenByBett(nID_BettenFrom);
                DataRow oPatientDst = GetPatientNachnameAnzahlBettenByBett(nID_BettenTo);

                if (oPatientDst != null)
                {
                    MessageBox.Show("Bett ist schon belegt!", AppTitle());
                    bSuccess = false;
                }
                else
                {
                    if ((int)oZimmerDst["Belegt"] > 0 && ((int)oPatientSrc["AnzahlBetten"] != (int)oZimmerDst["AnzahlBetten"]))
                    {
                        MessageBox.Show("Patient '" + (string)oPatientSrc["Nachname"]
                            + "' kann nur in einem " + oPatientSrc["AnzahlBetten"].ToString() + "-Betten-Zimmer liegen.", AppTitle());
                        bSuccess = false;
                    }
                }
                if (bSuccess)
                {
                    bSuccess = _databaseLayerCommon.MovePatient(nID_BettenFrom, nID_BettenTo);
                }
            }

            return bSuccess;
        }
        #endregion
        #region LogTable
        public DataView GetLogTable(string strNumRecords, string strUser, string strVon, string strBis, string strAktion, string strMessage)
        {
            return _databaseLayerCommon.GetLogTable(strNumRecords, strUser, strVon, strBis, strAktion, strMessage);
        }
        #endregion

        public void OpenDatabaseForImport()
        {
            _databaseLayerCommon.OpenForImport();
        }
        public void CloseDatabaseForImport()
        {
            _databaseLayerCommon.CloseForImport();
        }

    }
}

