using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms;
using Utility;

namespace Station
{
    public partial class PatientView : StationForm
    {
        bool _bEdit;
        DataRow _oPatient;
        bool _bIgnoreEvents = false;
        private int _nID_Diagramme;

        public PatientView(BusinessLayer b, int nID_Diagramme, DataRow oPatient) :
            base(b)
        {
            _nID_Diagramme = nID_Diagramme;

            InitializeComponent();

            if (oPatient == null)
            {
                // neuer Patient
                _oPatient = this.BusinessLayer.CreateDataRowPatient();
                _bEdit = false;
                this.Text = BusinessLayer.AppTitle("Patient Aufnahme");
            }
            else
            {
                _oPatient = oPatient;
                _bEdit =true;
                this.Text = BusinessLayer.AppTitle("Patient bearbeiten");
            }
        }

        private void InitializeAnzahlBetten()
        {
            cbAnzahlBetten.Items.Clear();

            cbAnzahlBetten.Items.Add("1");
            cbAnzahlBetten.Items.Add("2");
            cbAnzahlBetten.Items.Add("3");
        }

        private void PatientEingangView_Load(object sender, EventArgs e)
        {
            InitializeAnzahlBetten();
            InitializeIsolation();
            InitializeInfektionen();

            PopulateInfektionen();

            Object2Control();
            XableControls();
        }

        private void InitializeInfektionen()
        {
            DefaultListViewProperties(lvInfektionen);
            lvInfektionen.Clear();
            lvInfektionen.Columns.Add("Diagnose", 100, HorizontalAlignment.Left);
            lvInfektionen.Columns.Add("Datum", -2, HorizontalAlignment.Left);
        }

        private void PopulateInfektionen()
        {
            if (_bEdit)
            {
                int nID_Patienten = (int)_oPatient["ID_Patienten"];

                DataView dataview = BusinessLayer.GetPatientenInfektionen(nID_Patienten, "Sonstiges, Name");

                lvInfektionen.Items.Clear();

                ListViewItem lvi = new ListViewItem("Infektionen");
                lvi.Tag = -1;
                lvi.SubItems.Add("");
                lvInfektionen.Items.Add(lvi);

                bool bIstSonstiges = false;
                foreach (DataRow dataRow in dataview.Table.Rows)
                {
                    if (!bIstSonstiges && (int)dataRow["Sonstiges"] == 1)
                    {
                        bIstSonstiges = true;
                        lvi = new ListViewItem("Sonstiges");
                        lvi.Tag = -1;
                        lvi.SubItems.Add("");
                        lvInfektionen.Items.Add(lvi);
                    }
                    ListViewItem lvi2 = new ListViewItem(((string)dataRow["Name"]).ToString());
                    lvi2.Tag = (int)dataRow["ID_PatientenInfektionen"];
                    lvi2.SubItems.Add(((DateTime)dataRow["Datum"]).ToShortDateString());

                    lvInfektionen.Items.Add(lvi2);
                }
            }
        }

        private void InitializeIsolation()
        {
            cbIsolation.Items.Clear();
            cbIsolation.Items.Add("MRSA");
            cbIsolation.Items.Add("Intensivbetreuung");
        }

        protected override void SaveObject()
        {
            if (this._bEdit)
            {
                BusinessLayer.UpdatePatient(_oPatient);
            }
            else
            {
                int nID_Patienten = BusinessLayer.InsertPatient(_oPatient);
                if (nID_Patienten > 0)
                {
                    // lvInfektionen kann erst danach gespeichert werden, weil es ID_Patienten referenziert,
                    // den gibt es aber erst, wenn er zum ersten Mal gespeichert wurde.
                    foreach (ListViewItem lvi in lvInfektionen.Items)
                    {
                        DataRow dataRow = BusinessLayer.CreateDataRowPatientenInfektionen();

                        dataRow["ID_Patienten"] = nID_Patienten;
                        dataRow["ID_Infektionen"] = (int) lvi.Tag;
                        dataRow["Datum"] = Utility.Tools.InputTextDate2NullableDatabaseDateTime(lvi.SubItems[1].Text);
                        BusinessLayer.InsertPatientenInfektionen(dataRow);
                    }

                    _oPatient["ID_Patienten"] = nID_Patienten;
                    _bEdit = true;
                    this.Text = BusinessLayer.AppTitle("Patient bearbeiten");
                    XableControls();
                }
            }
        }

        private void Object2ComboBox(ComboBox cb, DataView dataview, DataRow oDataRow, string strValue, string strDisplay, string strText)
        {
            cb.DataSource = dataview;
            cb.ValueMember = strValue;
            cb.DisplayMember = strDisplay;

            if (oDataRow[strValue] == DBNull.Value)
            {
                cb.SelectedIndex = -1;
                cb.Text = (string)oDataRow[strText];
            }
            else
            {
                for (int i = 0; i < cb.Items.Count; i++)
                {
                    DataRowView dataRowView = (DataRowView)cb.Items[i];
                    DataRow dataRow = dataRowView.Row;
                    if ((int)dataRow[strValue] == (int)oDataRow[strValue])
                    {
                        cb.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        protected override void Object2Control()
        {
            _bIgnoreEvents = true;

            // Diagnose
            DataView dataview = BusinessLayer.GetDiagnosen();
            Object2ComboBox(cbDiagnosen, dataview, _oPatient, "ID_Diagnosen", "DisplayMember", "Diagnose");

            _bIgnoreEvents = false;

            this.txtNachname.Text = (string)_oPatient["Nachname"];
            this.txtVorname.Text = (string)_oPatient["Vorname"];

            chkIsolation.Checked = ((int)_oPatient["Isolation"]) > 0 ? true : false;
            cbIsolation.Text = (string)_oPatient["IsolationText"];
            chkHervorheben.Checked = ((int)_oPatient["Hervorheben"]) > 0 ? true : false;
            txtHervorhebenGrund.Text = (string)_oPatient["HervorhebenGrund"];
            cbAnzahlBetten.SelectedIndex = (int)_oPatient["AnzahlBetten"] - 1;

            this.txtGeburtsdatum.Text = Utility.Tools.DBNullableDateTime2DateString(_oPatient["Geburtsdatum"]);
            this.txtAufnahmedatum.Text = Utility.Tools.DBNullableDateTime2DateString(_oPatient["Aufnahmedatum"]);
            this.txtEntlassungsdatum.Text = Utility.Tools.DBNullableDateTime2DateString(_oPatient["Entlassungsdatum"]);
            this.radMann.Checked = ((int)_oPatient["Geschlecht"]) == 0 ? false : true;
            this.radFrau.Checked = !this.radMann.Checked;
            this.chkPrivat.Checked = ((int)_oPatient["Privat"]) == 0 ? false : true;
            if (_oPatient["ID_Betten"] != DBNull.Value)
            {
                this.txtBett.Text = _oPatient["Station"].ToString() + "." + _oPatient["ZimmerNummer"].ToString();
            }
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;
            string strMessage = "Eingabefehler:\n";

            if (txtNachname.Text.Length == 0)
            {
                strMessage += "\n- Nachname muss ausgefüllt sein.";
                fSuccess = false;
            }
            if (txtGeburtsdatum.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtGeburtsdatum.Text))
            {
                strMessage += "\n- Geburtsdatum hat falsches Format oder ist ungültig.";
                fSuccess = false;
            }
            if (this.txtAufnahmedatum.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtAufnahmedatum.Text))
            {
                strMessage += "\n- Aufnahmedatum hat falsches Format oder ist ungültig.";
                fSuccess = false;
            }
            if (this.txtEntlassungsdatum.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtEntlassungsdatum.Text))
            {
                strMessage += "\n- Entlassungsdatum hat falsches Format oder ist ungültig.";
                fSuccess = false;
            }
            if (!fSuccess)
            {
                MessageBox(strMessage);
            }
            return fSuccess;
        }
        protected override void Control2Object()
        {
            _oPatient["Nachname"] = this.txtNachname.Text;
            _oPatient["Vorname"] = this.txtVorname.Text;
            _oPatient["Geburtsdatum"] = Utility.Tools.InputTextDate2NullableDatabaseDateTime(txtGeburtsdatum.Text);
            _oPatient["Aufnahmedatum"] = Utility.Tools.InputTextDate2NullableDatabaseDateTime(txtAufnahmedatum.Text);

            _oPatient["Isolation"] = this.chkIsolation.Checked;
            _oPatient["IsolationText"] = cbIsolation.Text;
            _oPatient["Hervorheben"] = this.chkHervorheben.Checked;
            _oPatient["HervorhebenGrund"] = txtHervorhebenGrund.Text;
            _oPatient["AnzahlBetten"] = cbAnzahlBetten.SelectedIndex + 1;

            if (this.txtEntlassungsdatum.Text.Length > 0)
            {
                _oPatient["Entlassungsdatum"] = Utility.Tools.InputTextDate2NullableDatabaseDateTime(this.txtEntlassungsdatum.Text);
            }
            _oPatient["Geschlecht"] = this.radMann.Checked ? 1 : 0;
            _oPatient["Privat"] = chkPrivat.Checked ? 1 : 0;

            // Diagnose
            if (cbDiagnosen.SelectedIndex == -1)
            {
                _oPatient["Diagnose"] = cbDiagnosen.Text;
                _oPatient["ID_Diagnosen"] = DBNull.Value;
            }
            else
            {
                _oPatient["Diagnose"] = "";
                _oPatient["ID_Diagnosen"] = (int)cbDiagnosen.SelectedValue;
            }
        }

        private void DiagnoseChanged()
        {
            if (txtAufnahmedatum.Text.Length > 0)
            {
                if (!_bIgnoreEvents)
                {
                    if (cbDiagnosen.SelectedValue != null)
                    {
                        int nID_Diagnosen;

                        if (int.TryParse(cbDiagnosen.SelectedValue.ToString(), out nID_Diagnosen))
                        {
                            DataRow oDiagnose = this.BusinessLayer.GetDiagnose(nID_Diagnosen);
                            float fMittlereLiegedauer = (float)oDiagnose["M_GVD"];
                            DateTime dtEntlassungsdatum = 
                                Tools.InputTextDate2DateTime(txtAufnahmedatum.Text).AddDays(fMittlereLiegedauer);
                            this.txtEntlassungsdatum.Text = dtEntlassungsdatum.ToShortDateString();
                        }
                        else
                        {
                            this.txtEntlassungsdatum.Text = "";
                        }
                    }
                }
            }
        }

        private void cbDiagnosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreEvents)
            {
                DiagnoseChanged();
            }
        }

        private void cmdZimmer_Click(object sender, EventArgs e)
        {
            BettenAuswahlView dlg = new BettenAuswahlView(BusinessLayer, _nID_Diagramme, _oPatient);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtBett.Text = dlg.Data;
                this._oPatient["ID_Betten"] = dlg.ID_Betten;
            }
        }

        private void cmdAufnahmedatum_Click(object sender, EventArgs e)
        {
            CalendarPickerView dlg = new CalendarPickerView(txtAufnahmedatum.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtAufnahmedatum.Text = dlg.SelectedDate.ToShortDateString();
            }
        }

        private void cmdEntlassungsdatum_Click(object sender, EventArgs e)
        {
            CalendarPickerView dlg = new CalendarPickerView(txtEntlassungsdatum.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtEntlassungsdatum.Text = dlg.SelectedDate.ToShortDateString();
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();
                Object2Control();
            }
        }

        private void cbDiagnosen_TextChanged(object sender, EventArgs e)
        {
            this.DiagnoseChanged();
        }

        private void chkIsolation_CheckedChanged(object sender, EventArgs e)
        {
            EinBett(chkIsolation.Checked);

            cbIsolation.Enabled = chkIsolation.Checked;
        }

        private void chkHervorheben_CheckedChanged(object sender, EventArgs e)
        {
            txtHervorhebenGrund.Enabled = chkHervorheben.Checked; 
        }
        private void EinBett(bool bIsolation)
        {
            if (bIsolation)
            {
                this.cbAnzahlBetten.SelectedIndex = 0;
                this.cbAnzahlBetten.Enabled = false;
            }
            else
            {
                this.cbAnzahlBetten.SelectedIndex = 2;
                this.cbAnzahlBetten.Enabled = true;
            }
        }

        private void PatientView_Shown(object sender, EventArgs e)
        {
            txtNachname.Focus();
        }

        private void cmdGeburtsdatum_Click(object sender, EventArgs e)
        {
            CalendarPickerView dlg = new CalendarPickerView(txtGeburtsdatum.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtGeburtsdatum.Text = dlg.SelectedDate.ToShortDateString();
            }

        }

        private void XableControls()
        {
            cbIsolation.Enabled = chkIsolation.Checked;
            txtHervorhebenGrund.Enabled = chkHervorheben.Checked;
        }

        private void cmdInfektionDelete_Click(object sender, EventArgs e)
        {
            int nID_PatientenInfektionen = GetFirstSelectedTag(lvInfektionen, true);
            if (nID_PatientenInfektionen != -1)
            {
                BusinessLayer.DeletePatientenInfektionen(nID_PatientenInfektionen);
                PopulateInfektionen();
            }
        }

        private void cmdInfektionNew_Click(object sender, EventArgs e)
        {
            SelectInfektionenView dlg = new SelectInfektionenView(BusinessLayer);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int nIDInfektionen = dlg._nID_Infektionen;
                if (_bEdit)
                {
                    DataRow dataRow = BusinessLayer.CreateDataRowPatientenInfektionen();

                    dataRow["ID_Patienten"] = _oPatient["ID_Patienten"];
                    dataRow["ID_Infektionen"] = dlg._nID_Infektionen;
                    dataRow["Datum"] = dlg._strDatum;
                    BusinessLayer.InsertPatientenInfektionen(dataRow);
                    PopulateInfektionen();
                }
                else
                {
                    ListViewItem lvi = new ListViewItem(dlg._strName);
                    lvi.Tag = dlg._nID_Infektionen;
                    lvi.SubItems.Add(dlg._strDatum);
                    lvInfektionen.Items.Add(lvi);
                }
            }
        }

        private void SetColorEntlassungDatum()
        {
            if (Tools.DateIsValidGermanDate(txtEntlassungsdatum.Text))
            {
                DateTime dtEntlassungsDatum = Utility.Tools.InputTextDate2DateTime(txtEntlassungsdatum.Text);

                if (dtEntlassungsDatum.Date < DateTime.Now.Date)
                {
                    txtEntlassungsdatum.BackColor = Color.Red;
                }
                else if (dtEntlassungsDatum.Date < DateTime.Now.Date.AddDays(3))
                {
                    txtEntlassungsdatum.BackColor = Color.Orange;
                }
                else
                {
                    txtEntlassungsdatum.BackColor = this.BackColor;
                }
            }
        }

        private void txtEntlassungsdatum_TextChanged(object sender, EventArgs e)
        {
            SetColorEntlassungDatum();
        }

        private void cmdKatalog_Click(object sender, EventArgs e)
        {
            InfektionenView dlg = new InfektionenView(BusinessLayer);
            dlg.ShowDialog();
        }
    }
}