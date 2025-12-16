using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Station.UserControls
{
    public partial class InformationBox : UserControl
    {
        private BusinessLayer _oBusinessLayer;
        private int _nID_Diagramme;

        public InformationBox(BusinessLayer b, int nID_Diagramme)
        {
            _nID_Diagramme = nID_Diagramme;
            _oBusinessLayer = b;

            InitializeComponent();
        }

        public string FreieMaennerbetten
        {
            get { return lblFreieMaennerbetten.Text; }
        }
        public string FreieFrauenbetten
        {
            get { return lblFreieFrauenbetten.Text; }
        }
        public string FreieZimmer
        {
            get { return lblFreieZimmer.Text; }
        }
        public string Belegung
        {
            get { return lblBelegung.Text; }
        }
        public ListView ListViewInfektionen
        {
            get { return this.lvInfektionen; }
        }
        private BusinessLayer BusinessLayer
        {
            get { return _oBusinessLayer; }
        }
        
        public void AnzeigeInfos()
        {
            long nFreieMaennerbetten;
            long nFreieFrauenbetten;
            long nFreieZimmer;
            long nVerfuegbareBettenGesamt;
            long nBelegteBettenGesamt;
            string strBelegung;

            this.BusinessLayer.FreieBetten(_nID_Diagramme, out nFreieMaennerbetten, out nFreieFrauenbetten);
            nFreieZimmer = this.BusinessLayer.FreieZimmer(_nID_Diagramme);

            this.lblFreieFrauenbetten.Text = nFreieFrauenbetten.ToString();
            this.lblFreieMaennerbetten.Text = nFreieMaennerbetten.ToString();

            this.BusinessLayer.Belegung(_nID_Diagramme, out nVerfuegbareBettenGesamt, out nBelegteBettenGesamt);

            lblFreieZimmer.Text = (nVerfuegbareBettenGesamt - nBelegteBettenGesamt).ToString() + "/" + nFreieZimmer.ToString();

            strBelegung = nBelegteBettenGesamt.ToString() + " von " + nVerfuegbareBettenGesamt.ToString() + " (";
            strBelegung += ((int)(100.0 * ((float)nBelegteBettenGesamt / (float)nVerfuegbareBettenGesamt))).ToString() + "%)";
            lblBelegung.Text = strBelegung;
        }

        protected void DefaultListViewProperties(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.ShowItemToolTips = true;
            lv.GridLines = false;
            lv.HideSelection = false;
        }

        public void PopulateInfektionen()
        {
            DataView dataview = BusinessLayer.GetPatientenInfektionen(_nID_Diagramme);

            DefaultListViewProperties(lvInfektionen);

            lvInfektionen.Clear();

            lvInfektionen.Columns.Add("Diagnose", 70, HorizontalAlignment.Left);
            lvInfektionen.Columns.Add("Name", 70, HorizontalAlignment.Left);
            lvInfektionen.Columns.Add("Zi", 50, HorizontalAlignment.Left);
            lvInfektionen.Columns.Add("Dauer (Tage)", -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                string s = "";

                ListViewItem lvi = new ListViewItem(((string)dataRow["Infektionsname"]).ToString());

                s = (string)dataRow["Nachname"];
                if ((int)dataRow["Hervorheben"] > 0)
                {
                    s = "! " + s;
                }
                lvi.SubItems.Add(s);
                if (dataRow["Station"] != DBNull.Value && dataRow["ZimmerNummer"] != DBNull.Value)
                {
                    s = dataRow["Station"].ToString() + "." + dataRow["ZimmerNummer"].ToString();
                }
                lvi.SubItems.Add(s);

                s = "";
                DateTime dtNow = DateTime.Now;
                DateTime dtDatum = (DateTime)dataRow["Datum"];
                if (dtDatum < dtNow)
                {
                    TimeSpan ts = dtNow.Subtract(dtDatum);
                    s = ts.Days.ToString();
                    lvi.SubItems.Add(s);
                }
                lvInfektionen.Items.Add(lvi);
            }
        }

        private void InformationBox_Load(object sender, EventArgs e)
        {
            AnzeigeInfos();
            PopulateInfektionen();
        }
    }
}
