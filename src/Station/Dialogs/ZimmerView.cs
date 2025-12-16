using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace Station
{
    public partial class ZimmerView : StationForm
    {
        private int _nID_Diagramme;
        private DataRow _oZimmer;
        private DataView _oBetten;

        public DataRow GetBett(int nID_Zimmer, int nBettenNummer)
        {
            return BusinessLayer.GetBett(nID_Zimmer, nBettenNummer);
        }

        public ZimmerView(BusinessLayer b, int nID_Diagramme, DataRow oZimmer) : base(b)
        {
            _nID_Diagramme = nID_Diagramme;
            _oZimmer = oZimmer;

            _oBetten = BusinessLayer.GetBetten((int)_oZimmer["ID_Zimmer"]);

            InitializeComponent();
        }

        private void UpdateBett(int nID_Zimmer, int nBettenNummer, object oID_Patienten)
        {
            if (oID_Patienten == DBNull.Value)
            {
                this.BusinessLayer.UpdateBettByZimmerBettNummer(nID_Zimmer, nBettenNummer);
            }
            else
            {
                this.BusinessLayer.UpdateBettByZimmerBettNummer(nID_Zimmer, nBettenNummer, (int)oID_Patienten);
            }
        }

        protected override void SaveObject()
        {
            foreach (DataRow row in _oBetten.Table.Rows)
            {
                UpdateBett((int)_oZimmer["ID_Zimmer"], (int)row["BettenNummer"], row["ID_Patienten"]);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            base.OKClicked();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        private void ZimmerView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Zimmer " + _oZimmer["Station"].ToString() + "." + _oZimmer["ZimmerNummer"].ToString());
            InitializeListViewPatienten();

            Object2Control();
        }

        private void Bett2Control(DataRow oBett)
        {
            if (oBett["ID_Patienten"] != DBNull.Value)
            {
                int nID_Patienten = (int)oBett["ID_Patienten"];

                DataRow oPatient = this.BusinessLayer.GetPatient(nID_Patienten);

                ListViewItem lvi = new ListViewItem(oPatient["Nachname"].ToString());
                lvi.Tag = oBett["BettenNummer"];
                lvi.SubItems.Add(oPatient["Vorname"].ToString());
                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(oPatient["Aufnahmedatum"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(oPatient["Entlassungsdatum"]));
                lvi.SubItems.Add(BusinessLayer.GetPatientDiagnose(oPatient));

                lvPatienten.Items.Add(lvi);
            }
        }
        private void InitializeListViewPatienten()
        {
            this.DefaultListViewProperties(lvPatienten);

            lvPatienten.Columns.Add("Nachname", 75, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("Vorname", 60, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("Aufnahmedatum", 100, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("gepl. Entl.datum", 120, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("Diagnose", -2, HorizontalAlignment.Left);
        }

        private void PopulateListViewPatienten()
        {
            lvPatienten.Items.Clear();

            foreach (DataRow row in _oBetten.Table.Rows)
            {
                Bett2Control(row);
            }
        }

        protected override void Object2Control()
        {
            PopulateListViewPatienten();
        }

        protected override void Control2Object()
        {
        }

        private DataRow AuswahlPatient()
        {
            DataRow oPatient = null;
            PatientenAuswahlView dlg = new PatientenAuswahlView(BusinessLayer);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                int nID_Patienten = dlg.ID_Patienten;
                if (nID_Patienten != -1)
                {
                    oPatient = this.BusinessLayer.GetPatient(nID_Patienten);
                }
            }

            return oPatient;
        }

        private void AssignPatient2Bett()
        {
            DataRow oPatient = AuswahlPatient();

            if (oPatient != null)
            {
                DataRow oBett = null;

                foreach (DataRow row in _oBetten.Table.Rows)
                {
                    if (row["ID_Patienten"] == DBNull.Value)
                    {
                        oBett = row;
                        break;
                    }
                }

                if (oBett == null)
                {
                    MessageBox("Alle Betten belegt");
                }
                else
                {
                    oBett["ID_Patienten"] = oPatient["ID_Patienten"];
                    PopulateListViewPatienten();
                }
            }
        }

        private void cmdBettAdd_Click(object sender, EventArgs e)
        {
            if (lvPatienten.Items.Count >= (int) _oZimmer["AnzahlBetten"])
            {
                MessageBox("Mehr als " + (int) _oZimmer["AnzahlBetten"] + " Patienten können nicht in diesem Zimmer liegen!");
            }
            else
            {
                AssignPatient2Bett();
            }
        }

        private void EntfernePatientAusBett(DataRow oBett)
        {
            oBett["ID_Patienten"] = DBNull.Value;
            PopulateListViewPatienten();
        }

        private void cmdBettDelete_Click(object sender, EventArgs e)
        {
            int nBettNummer = GetFirstSelectedTag(lvPatienten, true);

            foreach (ListViewItem lvi in lvPatienten.SelectedItems)
            {
                nBettNummer = (int)lvi.Tag;
                if (nBettNummer - 1 < _oBetten.Table.Rows.Count)
                {
                    EntfernePatientAusBett(_oBetten.Table.Rows[nBettNummer - 1]);
                }
                else
                {
                    MessageBox("Ungültige Bettennummer!");
                }
            }
        }

        private void cmdPatient_Click(object sender, EventArgs e)
        {
            EditSelectedPatient();
        }

        /// <summary>
        /// In der Tabelle ist nicht die ID_Patienten, also muss man den anhand des Namens suchen
        /// </summary>
        private void EditSelectedPatient()
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvPatienten, true);
            if (lvi != null)
            {
                DataRow oPatient = null;
                string strNachname = lvi.Text;

                DataView dv = BusinessLayer.GetPatientenByNachname(strNachname);
                if (dv.Table.Rows.Count == 1)
                {
                    // Es gibt genau einen Patienten mit diesem Nachnamen,
                    // genau diesen oeffnen
                    oPatient = dv.Table.Rows[0];
                }
                else if (dv.Table.Rows.Count > 1)
                {
                    // Es gibt mehr als einen Patienten mit diesem Nachnamen,
                    // Liste oeffnen mit Auswahl von allen mit diesem Nachnamen
                    PatientenAuswahlView dlg = new PatientenAuswahlView(BusinessLayer, dv);

                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        int nID_Patienten = dlg.ID_Patienten;
                        if (nID_Patienten != -1)
                        {
                            oPatient = BusinessLayer.GetPatient(nID_Patienten);
                        }
                    }
                }
                if (oPatient != null)
                {
                    PatientView dlg = new PatientView(BusinessLayer, _nID_Diagramme, oPatient);
                    dlg.ShowDialog();
                }
            }
        }

        private void lvPatienten_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedPatient();
        }
    }
}