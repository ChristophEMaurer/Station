using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Station
{
    public partial class PatientenAuswahlView : StationForm
    {
        private int _nID_Patienten = -1;
        private DataView _dataView;

        public PatientenAuswahlView(BusinessLayer b, DataView dv)
            : base(b)
        {
            _dataView = dv;
            InitializeComponent();
        }

        public PatientenAuswahlView(BusinessLayer b)
            : this(b, null)
        {
        }

        public int ID_Patienten
        {
            get { return _nID_Patienten; }
        }


        protected void cmdOK_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvPatienten.SelectedItems)
            {
                _nID_Patienten = (int) lvi.Tag;
                break;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        private void PatientenAuswahlView_Load(object sender, EventArgs e)
        {
            DataView dataview;

            if (_dataView != null)
            {
                dataview = _dataView;
            }
            else
            {
                dataview = BusinessLayer.GetPatienten();
            }

            lvPatienten.View = View.Details;
            lvPatienten.FullRowSelect = true;

            lvPatienten.Columns.Add("Nachname", 100, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("Vorname", 100, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("Aufnahmedatum", 100, HorizontalAlignment.Left);
            lvPatienten.Columns.Add("Diagnose", 200, HorizontalAlignment.Left);

            DataTable dataTable = dataview.Table;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {       
                DataRow dataRow = dataTable.Rows[i];
                ListViewItem lvi = new ListViewItem(((string)dataRow["Nachname"]).ToString());
                lvi.Tag = (int) dataRow["ID_Patienten"];
                lvi.SubItems.Add(dataRow["Vorname"].ToString());
                lvi.SubItems.Add(Utility.Tools.DBNullableDateTime2DateString(dataRow["Aufnahmedatum"]));
                lvi.SubItems.Add(this.BusinessLayer.GetPatientDiagnose(dataRow));

                lvPatienten.Items.Add(lvi);
            }

        }

        private void lvPatienten_DoubleClick(object sender, EventArgs e)
        {
            cmdOK_Click(null, null);
        }
    }
}