using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Station;
using Utility;

namespace Station
{
    public class ImportPlugin : StationImport
	{
        ImportEvent _oEvent;

        public override void ImportInit()
        {
            _oEvent = new ImportEvent();
        }

        public override void ImportRun()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                ImportPatients(dlg.FileName);
            }
        }

        public override void ImportFinalize()
        {
        }

        public void ImportPatients(string strFilename)
        {
            if (File.Exists(strFilename))
            {
                StreamReader sr = null;
                string strLine = "";

                try
                {
                    sr = new StreamReader(strFilename, Encoding.GetEncoding(1250));

                    do
                    {
                        strLine = sr.ReadLine();
                        if (strLine != null)
                        {
                            ImportLine(strLine);
                        }
                    } while (strLine != null && !_oEvent.Abort);
                }
                catch (Exception e)
                {
                    string strErrorText = e.Message;

                    if (e.InnerException != null)
                    {
                        strErrorText += "-" + e.InnerException;
                    }

                    MessageBox.Show(strErrorText);

                    _oEvent.ClearData();
                    _oEvent.State = EVENT_STATE.STATE_ERROR;
                    _oEvent.StateText = strErrorText;

                    this.FireImportEvent(_oEvent);
                }
            }
        }

        /// <summary>
        /// Convert two strings into a DateTime
        /// </summary>
        /// <param name="strData">"dd/mm/yyyy</param>
        /// <param name="strTime">"hh:mm"</param>
        /// <returns></returns>
        private bool DateAndTime2DateTime(string strDate, string strTime, out DateTime dt)
        {
            bool fSuccess = false;
            dt = DateTime.Now;
            if (strDate.Length == 10 && strDate.IndexOf('/') == 2 && strDate.LastIndexOf('/') == 5)
            {
                int nDay = Int32.Parse(strDate.Substring(0, 2));
                int nMonth = Int32.Parse(strDate.Substring(3, 2));
                int nYear = Int32.Parse(strDate.Substring(6, 4));

                if (strTime.Length == 5 && strTime.IndexOf(':') == 2)
                {
                    int nHour = Int32.Parse(strTime.Substring(0, 2));
                    int nMinute = Int32.Parse(strTime.Substring(3, 2));

                    try
                    {
                        dt = new DateTime(nYear, nMonth, nDay, nHour, nMinute, 0);
                        fSuccess = true;
                    }
                    catch
                    {
                    }
                }
            }

            return fSuccess;
        }

        private void Name2NachnameVorname(string strName, out string strNachname, out string strVorname)
        {
            char[] separators = { ' ', ',' };
            int nIndex;

            strNachname = strName;
            strVorname = "";

            nIndex = strName.IndexOfAny(separators);
            if (nIndex != -1)
            {
                strNachname = strName.Substring(0, nIndex);
                nIndex++;
                while (nIndex < strName.Length && strName[nIndex] == ' ')
                {
                    nIndex++;
                }
                if (nIndex < strName.Length)
                {
                    strVorname = strName.Substring(nIndex);
                }
            }
        }

        public void ImportLine(string strLine)
        {
            _oEvent.ClearData();
            _oEvent.State = EVENT_STATE.STATE_DATA;
            _oEvent.StateText = "";

            // 0          1        
            // <Nachname>;<Vorname>
            String[] arLine = strLine.Split(';');
            if (arLine.Length >= 3)
            {
                _oEvent.LastName = arLine[0];
                _oEvent.FirstName = arLine[1];
                _oEvent.BirthDate = Tools.InputTextDate2DateTime(arLine[2]);

                FireImportEvent(_oEvent);
            }
            else
            {
                // Weniger als 2 Spalten
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Weniger als 2 Spalten in Datenzeile, Datenzeile wird ignoriert: " + strLine;
                FireImportEvent(_oEvent);
            }
        }

        public override string ImportDescription()
        {
            string s = "Dieses Import-Plugin importiert Patienten aus einer CSV-Datei."
                + "\r\n\r\nFormat:  <Nachname>;<Vorname>"
            ;
            return s;
        }

    }
}
