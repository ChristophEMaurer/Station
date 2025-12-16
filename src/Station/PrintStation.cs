using System;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Data;

namespace Station
{
	public class PrintStation
	{
		#region Class Variables

        private enum PAGE_TYPE {
            GRAFIK,
            PATIENTEN,
            INFO
        };

        private const int X_PATIENT_ZIMMER = 250;
        private const int X_PATIENT_DIAGNOSE = 350;

        private const int PATIENTEN_PER_PAGE = 20;

        private bool _bPrintGrafik;
        private bool _bPrintPatienten;
        private bool _bPrintInfo;

        private int _nCurrentPage;

        private DataView _dvPatienten;
        private int _nDvPatientenIndex;

        private PAGE_TYPE _currentPageType;

        private int _nNumPagesPatienten;
        private int _nCurrentPagePatienten;

        private Font _printFont;
        private Font _printFontBold;
        private Font _printFontPrivat;
        private BusinessLayer _oBusinessLayer;
        private PrintDocument _pd;
        
        private const int ZIMMER_Y_OFFSET = 110;
        private const int BETT_HEIGHT = 32;
        private const int BETT_WIDTH = 32;

        private DynamicDiagramView _stationView;
        private DataRow _oDiagramm;
		#endregion

		#region Constructors

		/// <summary>
		/// Creates an empty PrintForm object.
		/// </summary>

		/// <summary>
		/// Creates a PrintForm object initialized with the specified Form.
		/// </summary>
        public PrintStation(BusinessLayer b, DynamicDiagramView frm, DataRow oDiagramm)
		{
            _oBusinessLayer = b;
            _stationView = frm;
            _oDiagramm = oDiagramm;

            _printFont = new Font("Courier New", 8);
            _printFontBold = new Font("Courier New", 8, FontStyle.Bold);
            _printFontPrivat = new Font("Courier New", 20);

            // Setup a PrintDocument
            _pd = new PrintDocument();
            _pd.BeginPrint += new PrintEventHandler(this.PrintDocument_BeginPrint);
            _pd.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            _pd.QueryPageSettings += new QueryPageSettingsEventHandler(_pd_QueryPageSettings);
            _pd.EndPrint += new PrintEventHandler(_pd_EndPrint);
        }

        private void _pd_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Color = false;
        }

		#endregion

		#region Public Methods
        public BusinessLayer BusinessLayer
        {
            get { return _oBusinessLayer; }
        }

        public void Print(bool bPrintGrafik, bool bPrintPatienten, bool bPrintInfo)
        {
            _bPrintGrafik = bPrintGrafik;
            _bPrintPatienten = bPrintPatienten;
            _bPrintInfo = bPrintInfo;

            PrintDialog dlg = new PrintDialog();
            dlg.Document = _pd;
            dlg.ShowDialog();
        }

		/// <summary>
		/// Prints the entire form to a PrintPreview object.
		/// </summary>
        public void PrintPreview(int nWidth, int nHeight, bool bPrintGrafik, bool bPrintPatienten, bool bPrintInfo)
        {
            PrintPreview(_stationView, nWidth, nHeight, bPrintGrafik, bPrintPatienten, bPrintInfo);
        }

        public void PrintPreview(Form frm, int nWidth, int nHeight, bool bPrintGrafik, bool bPrintPatienten, bool bPrintInfo)
		{
			if (frm == null)
				return;

            _bPrintGrafik = bPrintGrafik;
            _bPrintPatienten = bPrintPatienten;
            _bPrintInfo = bPrintInfo;

			// Setup & show the PrintPreviewDialog
			PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Width = nWidth;
            ppd.Height = nHeight;
			ppd.Document = _pd;
			ppd.ShowDialog();
		}

        void _pd_EndPrint(object sender, PrintEventArgs e)
        {
        }
        public void PageSetup()
        {
            PageSetupDialog dlg = new PageSetupDialog();
            dlg.Document = _pd;

            dlg.ShowDialog();
        }

		#endregion

		#region Event Handlers

		private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
		{
            _nCurrentPage = 0;

            // if sind rückwärts, letzter ist wichtigster, damit fängt man an.
            if (_bPrintInfo)
            {
                _currentPageType = PAGE_TYPE.INFO;
            }

            if (_bPrintPatienten)
            {
                _currentPageType = PAGE_TYPE.PATIENTEN;

                _dvPatienten = BusinessLayer.GetPatientenOrderBy("Zimmer.Station, Zimmer.ZimmerNummer");
                _nDvPatientenIndex = 0;
                _nNumPagesPatienten = _dvPatienten.Table.Rows.Count / PATIENTEN_PER_PAGE;
                _nCurrentPagePatienten = 0;
            }

            if (_bPrintGrafik)
            {
                _currentPageType = PAGE_TYPE.GRAFIK;
            }
		}

		private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			// Set the page margins
			Rectangle rPageMargins = new Rectangle(e.MarginBounds.Location, e.MarginBounds.Size);

			// Make sure nothing gets printed in the margins
			e.Graphics.SetClip(rPageMargins);

            Point ptOffset = new Point(rPageMargins.X, rPageMargins.Y);
            ptOffset.Offset(1, 0);

            if (_currentPageType == PAGE_TYPE.GRAFIK)
            {
                PrintPageStation(e, ptOffset);

                if (_bPrintPatienten)
                {
                    _currentPageType = PAGE_TYPE.PATIENTEN;
                    e.HasMorePages = true;
                }
                else if (_bPrintInfo)
                {
                    _currentPageType = PAGE_TYPE.INFO;
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
                
                _currentPageType = PAGE_TYPE.PATIENTEN;

            }
            else if (_currentPageType == PAGE_TYPE.PATIENTEN)
            {
                PrintPatienten(e, ref ptOffset);
                _nCurrentPagePatienten++;
                if (_nCurrentPagePatienten >= _nNumPagesPatienten)
                {
                    if (_bPrintInfo)
                    {
                        _currentPageType = PAGE_TYPE.INFO;
                        e.HasMorePages = true;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                else
                {
                    e.HasMorePages = true;
                }
            }
            else if (_currentPageType == PAGE_TYPE.INFO)
            {
                PrintPageStationInfo(e, ref ptOffset);
                e.HasMorePages = false;
            }

            _nCurrentPage++;
		}

		#endregion

        void PrintHeader(PrintPageEventArgs ev, ref Point ptOffset)
        {
            string line;

            line = "Station " + BusinessLayer.VersionString + ", Druckdatum: " + DateTime.Now.ToString() + "      Seite " + (_nCurrentPage + 1).ToString();
            PrintLine(ev, ref ptOffset, line);
            line = (string)_oDiagramm["Name"] + " (" + (string)_oDiagramm["Beschreibung"] + ")";
            PrintLine(ev, ref ptOffset, line);
            ptOffset.Y += 20;
        }

        void PrintLine(PrintPageEventArgs ev, ref Point ptOffset, string strLine)
        {
            PrintLine(ev, ref ptOffset, strLine, false, true);
        }

        void PrintLine(PrintPageEventArgs ev, ref Point ptOffset, string strLine, bool bBold)
        {
            PrintLine(ev, ref ptOffset, strLine, bBold, true);
        }

        void PrintLine(PrintPageEventArgs ev, ref Point ptOffset, string strLine, bool bBold, bool nIncY)
        {
            Font font;

            if (bBold)
            {
                font = _printFontBold;
            }
            else
            {
                font = _printFont;
            }

            ev.Graphics.DrawString(strLine, font, Brushes.Black,
                   ptOffset.X, ptOffset.Y, new StringFormat());

            if (nIncY)
            {
                if (bBold)
                {
                    ptOffset.Offset(0, (int)_printFontBold.GetHeight(ev.Graphics));
                }
                else
                {
                    ptOffset.Offset(0, (int)_printFont.GetHeight(ev.Graphics));
                }
            }
        }

        void PrintPatienten(PrintPageEventArgs ev, ref Point ptOffset)
        {
            string line = null;
            int nXNull = ptOffset.X;

            PrintHeader(ev, ref ptOffset);

            PrintLine(ev, ref ptOffset, "Patienten");
            PrintLine(ev, ref ptOffset, "");

            PrintLine(ev, ref ptOffset, "Patient", false, false);
            ptOffset.X = X_PATIENT_ZIMMER;
            PrintLine(ev, ref ptOffset, "Zimmer", false, false);
            ptOffset.X = X_PATIENT_DIAGNOSE;
            PrintLine(ev, ref ptOffset, "Diagnose");
            ptOffset.X = nXNull;
            ev.Graphics.DrawLine(Pens.Black, ev.MarginBounds.X, ptOffset.Y, ev.MarginBounds.Width, ptOffset.Y);
            ptOffset.Offset(0, 2);

            for (int i = 0; i < PATIENTEN_PER_PAGE; i++)
            {
                if (_nDvPatientenIndex >= _dvPatienten.Table.Rows.Count)
                {
                    break;
                }

                DataRow dataRow = _dvPatienten.Table.Rows[_nDvPatientenIndex++];

                line = "";
                if ((int)dataRow["Privat"] > 0)
                {
                    line = "[P] ";
                }
                line += (string)dataRow["Nachname"]; // +", " + dataRow["Vorname"].ToString();
                line = Utility.Tools.CutString(line, 20, true);
                PrintLine(ev, ref ptOffset, line, false, false);
                ptOffset.X = X_PATIENT_ZIMMER;

                line = dataRow["Station"].ToString() + "." + dataRow["ZimmerNummer"].ToString();
                PrintLine(ev, ref ptOffset, line, false, false);
                ptOffset.X = X_PATIENT_DIAGNOSE;

                line = BusinessLayer.GetPatientDiagnose(dataRow);
                line = Utility.Tools.CutString(line, 50, true);
                PrintLine(ev, ref ptOffset, line);
                ptOffset.X = nXNull;
            }
        }

        private void PrintBett(Graphics g, Point ptOffset, DataRow oZimmer, DataRow oBett, DataRow oPatient)
        {
            Point ptZimmer = ptOffset;
            ptZimmer.Offset((int)oZimmer["LocationX"], (int)oZimmer["LocationY"]);

            Point ptBett = ptZimmer;
            ptBett.Offset((int)oBett["LocationX"], (int)oBett["LocationY"]);

            g.DrawRectangle(Pens.Black,
                ptBett.X,
                ptBett.Y,
                BETT_HEIGHT,
                BETT_WIDTH);

            int nPrivate = 0;

            if (oPatient != null)
            {
                nPrivate = (int)oPatient["Privat"];

                if (nPrivate > 0)
                {
                    g.DrawString("P", _printFontPrivat, Brushes.Black, 
                        ptBett.X + 5,
                        ptBett.Y + 5
                        );
                }
                else
                {
                    g.DrawLine(Pens.Black, ptBett.X, ptBett.Y, ptBett.X + BETT_WIDTH, ptBett.Y + BETT_HEIGHT);
                    g.DrawLine(Pens.Black, ptBett.X + BETT_WIDTH, ptBett.Y, ptBett.X, ptBett.Y + BETT_HEIGHT);
                }
                if (oPatient["Aufnahmedatum"] != DBNull.Value && DateTime.Today < (DateTime)oPatient["Aufnahmedatum"])
                {
                    g.DrawString("Aufn.", _printFont, Brushes.Black, ptBett.X, ptBett.Y + 12);
                }

                if (oPatient["Entlassungsdatum"] != DBNull.Value && DateTime.Today > (DateTime)oPatient["Entlassungsdatum"])
                {
                    g.DrawString("Entl.", _printFont, Brushes.Black, ptBett.X, ptBett.Y);
                }
            }
        }

        private void PrintPatientenName(Graphics g, Point ptOffset, DataRow oZimmer, DataRow oBett, DataRow oPatient)
        {
            Point ptZimmer = ptOffset;
            ptZimmer.Offset((int)oZimmer["LocationX"], (int)oZimmer["LocationY"]);

            Point ptBett = ptZimmer;
            ptBett.Offset((int)oBett["LocationX"], (int)oBett["LocationY"]);

            if (oPatient != null)
            {
                string s = Utility.Tools.CutString((string)oPatient["Nachname"] + "-" + BusinessLayer.GetPatientDiagnose(oPatient), 20);
                g.DrawString(s, _printFont, Brushes.Black, 
                    ptZimmer.X,
                    ptBett.Y + BETT_HEIGHT + (int)oBett["BettenNummer"] * 12);
            }
        }

        private void PrintZimmer(Graphics g, Point ptOffset, DataRow rowZimmer)
        {
            //Zimmer
            g.DrawRectangle(Pens.Black, 
                ptOffset.X + (int)rowZimmer["LocationX"], 
                ptOffset.Y + (int)rowZimmer["LocationY"],
                (int)rowZimmer["Width"],
                (int)rowZimmer["Height"]);

            // Betten
            DataView dvBetten = BusinessLayer.DatabaseLayer.DesignerGetBetten((int)rowZimmer["ID_Zimmer"]);
            foreach (DataRow rowBetten in dvBetten.Table.Rows)
            {
                DataRow oPatient = BusinessLayer.GetPatientByBett((int)rowBetten["ID_Betten"]);
                PrintBett(g, ptOffset, rowZimmer, rowBetten, oPatient);
                PrintPatientenName(g, ptOffset, rowZimmer, rowBetten, oPatient);
            }
        }

        private void PrintPageStationGrafikInfo(PrintPageEventArgs ev, Point ptOffset)
        {
            Graphics g = ev.Graphics;

            g.DrawString("Freie Männerbetten: " + _stationView.InfoFreieMaennerbetten, _printFont, Brushes.Black, ptOffset.X, ptOffset.Y);
            ptOffset.Offset(0, (int)g.MeasureString("F", _printFont).Height);

            g.DrawString("Freie Frauenbetten: " + _stationView.InfoFreieFrauenbetten, this._printFont, Brushes.Black, ptOffset.X, ptOffset.Y);
            ptOffset.Offset(0, (int)g.MeasureString("F", _printFont).Height);

            g.DrawString("Freie Betten/Zimmer:" + _stationView.InfoFreieZimmer, this._printFont, Brushes.Black, ptOffset.X, ptOffset.Y);
            ptOffset.Offset(0, (int)g.MeasureString("F", _printFont).Height);

            g.DrawString("Belegung:" + _stationView.InfoBelegung, this._printFont, Brushes.Black, ptOffset.X, ptOffset.Y);
            ptOffset.Offset(0, (int)g.MeasureString("F", _printFont).Height);

            int nInfektionenY = ptOffset.Y + 20;

            ptOffset.Y = nInfektionenY;
            int nCount = 0;
            foreach (ListViewItem lvi in this._stationView.ListViewInfektionen.Items)
            {
                nCount++;
                string strLine = lvi.Text + " " + lvi.SubItems[1].Text + " " + lvi.SubItems[2].Text;
                if (lvi.SubItems[3].Text != "0")
                {
                    strLine += " " + lvi.SubItems[3].Text + "T";
                }
                PrintLine(ev, ref ptOffset, strLine);
                if (nCount > 12)
                {
                    nCount = 0;
                    ptOffset.Y = nInfektionenY;
                    ptOffset.X += 250;
                }
            }
        }

        private void PrintPageStationInfo(PrintPageEventArgs ev, ref Point ptOffset)
        {
            Graphics g = ev.Graphics;
            int nInfektionenY = ptOffset.Y + ZIMMER_Y_OFFSET;

            PrintHeader(ev, ref ptOffset);

            PrintLine(ev, ref ptOffset, "Infektionen");
            PrintLine(ev, ref ptOffset, "");

            int nCount = 0;
            foreach (ListViewItem lvi in this._stationView.ListViewInfektionen.Items)
            {
                nCount++;
                string strLine = nCount.ToString() + " " + lvi.Text + ": " + lvi.SubItems[1].Text + ", " + lvi.SubItems[2].Text;
                if (lvi.SubItems[3].Text != "0")
                {
                    strLine += ", " + lvi.SubItems[3].Text + "T";
                }
                PrintLine(ev, ref ptOffset, strLine);
            }
        }

        private void PrintPageStation(PrintPageEventArgs ev, Point ptOffset)
        {
            Graphics g = ev.Graphics;

            Point ptInfo = new Point(ptOffset.X, 0);
            int nHeight = 0;

            string line = (string)_oDiagramm["Name"] + " (" + (string)_oDiagramm["Beschreibung"] + ")";
            PrintLine(ev, ref ptOffset, line);
            
            DataView dv = BusinessLayer.DatabaseLayer.DesignerGetZimmerForDiagramm((int)_oDiagramm["ID_Diagramme"]);
            foreach (DataRow rowZimmer in dv.Table.Rows)
            {
                PrintZimmer(g, ptOffset, rowZimmer);

                if (ptOffset.Y + (int)rowZimmer["LocationY"] > ptInfo.Y)
                {
                    ptInfo.Y = ptOffset.Y + (int)rowZimmer["LocationY"];
                    nHeight = (int)rowZimmer["Height"];
                }

                g.DrawString(
                    rowZimmer["Station"].ToString() + "." + rowZimmer["ZimmerNummer"].ToString(), 
                    _printFont, Brushes.Black,
                    ptOffset.X + (int)rowZimmer["NummerLocationX"], ptOffset.Y + (int)rowZimmer["NummerLocationY"]);
            }
            ptInfo.Y += nHeight;
            PrintPageStationGrafikInfo(ev, ptInfo);
        }
    }
}
