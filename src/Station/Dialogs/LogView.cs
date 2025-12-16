using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utility;
using Windows.Forms;

namespace Station
{
    public partial class LogView : StationForm
    {
        public LogView(BusinessLayer b) : base(b)
        {
            InitializeComponent();
        }

        private void Search()
        {
            if (ValidateInput())
            {
                PopulateLogTable(txtNumRecords.Text, txtUser.Text, txtVon.Text, txtBis.Text, txtAktion.Text, txtMessage.Text);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void InitLogTable()
        {
            DefaultListViewProperties(lvLogTable);

            lvLogTable.Clear();
            lvLogTable.Columns.Add("Zeit", 100, HorizontalAlignment.Left);
            lvLogTable.Columns.Add("Benutzer", 100, HorizontalAlignment.Left);
            lvLogTable.Columns.Add("Aktion", 100, HorizontalAlignment.Left);
            lvLogTable.Columns.Add("Text", -2, HorizontalAlignment.Left);
        }

        private void PopulateLogTable(string strNumRecords, string strUser, string strVon, string strBis, string strAktion, string strMessage)
        {
            DataView dataview = BusinessLayer.GetLogTable(strNumRecords, strUser, strVon, strBis, strAktion, strMessage);

            lvLogTable.Items.Clear();
            lvLogTable.BeginUpdate();

            DataTable dataTable = dataview.Table;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["TimeStamp"]));
                lvi.SubItems.Add((string)dataRow["User"]);
                lvi.SubItems.Add((string)dataRow["Action"]);
                lvi.SubItems.Add((string)dataRow["Message"]);

                lvLogTable.Items.Add(lvi);
            }
            lvLogTable.EndUpdate();
        }


        private void LogView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Log Tabelle");
            InitLogTable();
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;

            int nNumRecords;
            string strMessage = EINGABEFEHLER;

            if (txtNumRecords.Text.Length > 0 && !Int32.TryParse(txtNumRecords.Text, out nNumRecords))
            {
                strMessage += "\n- '" + lblNumRecords.Text + "' ist keine gültige Zahl";
                fSuccess = false;
            }
            if (txtVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtVon.Text))
            {
                strMessage += "\n- '" + lblVon.Text + "' ist kein gültiges Datum";
                fSuccess = false;
            }
            if (txtBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtBis.Text))
            {
                strMessage += "\n- '" + lblBis.Text + "' ist kein gültiges Datum";
                fSuccess = false;
            }
            if (!fSuccess)
            {
                MessageBox(strMessage);
            }

            return fSuccess;
        }

        protected  void CancelClicked(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        private void cmdDateVon_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtVon.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtVon.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdDateBis_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtBis.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtBis.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdClearFields_Click(object sender, EventArgs e)
        {
            txtVon.Text = "";
            txtBis.Text = "";
            txtNumRecords.Text = "";
            txtUser.Text = "";
            txtAktion.Text = "";
            txtMessage.Text = "";
        }

    }
}