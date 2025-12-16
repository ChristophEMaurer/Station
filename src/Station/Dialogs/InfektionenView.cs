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
    public partial class InfektionenView : StationForm
    {
        private DataRow _oInfektion;
        private const string INFEKTION = "Infektion";
        private const string SONSTIGES = "Sonstiges";


        public InfektionenView(BusinessLayer b) : base(b)
        {
            _oInfektion = this.BusinessLayer.CreateDataRowInfektion();

            InitializeComponent();
        }

        private void InitInfektionen()
        {
            DefaultListViewProperties(lvInfektionen);

            lvInfektionen.Columns.Add("Text", 200, HorizontalAlignment.Left);
            lvInfektionen.Columns.Add("Reihenfolge", 80, HorizontalAlignment.Left);
            lvInfektionen.Columns.Add("Infektion/Sonstiges", -2, HorizontalAlignment.Left);
        }


        private void PopulateInfektionen()
        {
            DataView dataview = BusinessLayer.GetInfektionen();

            lvInfektionen.Items.Clear();
            lvInfektionen.BeginUpdate();
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Name"]);
                lvi.Tag = (int)dataRow["ID_Infektionen"];
                lvi.SubItems.Add(dataRow["Reihenfolge"].ToString());
                if ((int)dataRow["Sonstiges"] == 1)
                {
                    lvi.SubItems.Add(SONSTIGES);
                }
                else
                {
                    lvi.SubItems.Add(INFEKTION);
                }

                lvInfektionen.Items.Add(lvi);
            }
            lvInfektionen.EndUpdate();
        }

        private void InfektionenKatalogView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Infektionen/Sonstiges-Katalog pflegen");

            InitInfektionen();
            PopulateInfektionen();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtOrder.Text.Length <= 0)
            {
                strMessage += "\n- Reihenfolge fehlt";
                bSuccess = false;
            }
            else
            {
                int n;
                if (!Int32.TryParse(txtOrder.Text, out n))
                {
                    strMessage += "\n- Reihenfolge muss eine Zahl sein";
                    bSuccess = false;
                }
            }
            if (txtName.Text.Length <= 0)
            {
                strMessage += "\n- Text fehlt";
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void Control2Object()
        {
            _oInfektion["Reihenfolge"] = txtOrder.Text;
            _oInfektion["Sonstiges"] = radSonstiges.Checked ? 1 : 0;
            _oInfektion["Name"] = txtName.Text;
        }

        protected override void SaveObject()
        {
            BusinessLayer.InsertInfektion(_oInfektion);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();
                PopulateInfektionen();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int nID_Infektionen = GetFirstSelectedTag(lvInfektionen, true);

            if (nID_Infektionen != -1)
            {
                if (Confirm("ACHTUNG:\nEine Eintrag kann nur gelöscht werden,"
                    + " wenn er nirgends benutzt wird."
                    + "\n\nFortfahren mit Löschen?"))
                {
                    BusinessLayer.DeleteInfektion(nID_Infektionen);
                    PopulateInfektionen();
                }
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvInfektionen, true);
            if (lvi != null)
            {
                _oInfektion["Name"] = txtName.Text;
                _oInfektion["Reihenfolge"] = txtOrder.Text;
                _oInfektion["Sonstiges"] = radSonstiges.Checked ? 1 : 0;
                _oInfektion["ID_Infektionen"] = lvi.Tag;

                BusinessLayer.UpdateInfektion(_oInfektion);
                PopulateInfektionen();
            }

        }

        private void lvInfektionen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvInfektionen, false);
            if (lvi != null)
            {
                txtName.Text = lvi.Text;
                txtOrder.Text = lvi.SubItems[1].Text;
                if (lvi.SubItems[2].Text == SONSTIGES)
                {
                    radSonstiges.Checked = true;
                    radInfektion.Checked = false;
                }
                else
                {
                    radSonstiges.Checked = false;
                    radInfektion.Checked = true;
                }
            }
        }
    }
}

