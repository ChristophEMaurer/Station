using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Station
{
    public partial class BettenAuswahlView : StationForm
    {
        DataRow _oPatient;
        int _nID_Betten = -1;
        string _strData = "";
        private int _nID_Diagramme;

        public BettenAuswahlView(BusinessLayer b, int nID_Diagramme, DataRow oPatient) : base(b)
        {
            _nID_Diagramme = nID_Diagramme;
            _oPatient = oPatient;

            InitializeComponent();
        }

        public string Data
        {
            get { return _strData; }
        }

        public int ID_Betten
        {
            get { return _nID_Betten; }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvBetten.SelectedItems)
            {
                _nID_Betten = (int)lvi.Tag;
                _strData = lvi.SubItems[0].Text;
                break;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FillListViewBetten()
        {
            DataView dataView = BusinessLayer.GetFreieBetten(_nID_Diagramme);

            foreach (DataRow oBett in dataView.Table.Rows)
            {
                string s = oBett["Station"].ToString() + "." + oBett["Zimmernummer"].ToString();
                ListViewItem lvi = new ListViewItem(s);

                lvi.Tag = (int)oBett["ID_Betten"];
                lvi.SubItems.Add(oBett["Bettennummer"].ToString());

                lvBetten.Items.Add(lvi);
            }
        }
        private void InitializeListViewBetten()
        {
            lvBetten.View = View.Details;
            lvBetten.FullRowSelect = true;

            lvBetten.Columns.Add("Zimmer", 100, HorizontalAlignment.Left);
            lvBetten.Columns.Add("Bett", 100, HorizontalAlignment.Left);
        }

        private void ZimmerAuswahlView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Freies Bett auswählen");
            InitializeListViewBetten();
            FillListViewBetten();
        }

        private void lvBetten_DoubleClick(object sender, EventArgs e)
        {
            cmdOK_Click(null, null);
        }
    }
}

