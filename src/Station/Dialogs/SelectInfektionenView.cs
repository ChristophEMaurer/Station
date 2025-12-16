using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Windows.Forms;

namespace Station
{
    public partial class SelectInfektionenView : StationForm
    {
        public int _nID_Infektionen = -1;
        public string _strName = "";
        public string _strDatum = "";

        public SelectInfektionenView(BusinessLayer b) : base(b)
        {
            InitializeComponent();
        }

        private void SelectInfektionenView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Nosokomiale Infektion - Sonstiges auswählen");

            PopulateInfektionen();

            txtDatum.Text = DateTime.Now.ToShortDateString();
        }

        private void PopulateInfektionen()
        {
            DataView dataview = BusinessLayer.GetInfektionen();

            DefaultListViewProperties(lvInfektionen);

            lvInfektionen.Clear();

            lvInfektionen.Columns.Add("Diagnose", -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(((string)dataRow["Name"]).ToString());
                lvi.Tag = (int)dataRow["ID_Infektionen"];

                lvInfektionen.Items.Add(lvi);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvInfektionen, true);

            if (lvi != null)
            {
                _nID_Infektionen = (int) lvi.Tag;
                _strName = lvi.SubItems[0].Text;
                _strDatum = txtDatum.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdDatum_Click(object sender, EventArgs e)
        {
            CalendarPickerView dlg = new CalendarPickerView();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtDatum.Text = dlg.SelectedDate.ToShortDateString();
            }
        }
    }
}
