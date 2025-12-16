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
    public partial class DiagnosenView : StationForm
    {
        DataRow _oDiagnose;

        public DiagnosenView(BusinessLayer b)
            : base(b)
        {
            _oDiagnose = this.BusinessLayer.CreateDataRowDiagnose();

            InitializeComponent();
        }

        private void InitDiagnosen()
        {
            DefaultListViewProperties(lvDiagnosen);

            lvDiagnosen.Columns.Add("DRG", 60, HorizontalAlignment.Left);
            lvDiagnosen.Columns.Add("untere GVD", 80, HorizontalAlignment.Left);
            lvDiagnosen.Columns.Add("mittlere GVD", 80, HorizontalAlignment.Left);
            lvDiagnosen.Columns.Add("obere GVD", 80, HorizontalAlignment.Left);
            lvDiagnosen.Columns.Add("Diagnose", -2, HorizontalAlignment.Left);
        }

        private void PopulateDiagnosen()
        {
            DataView dataview = BusinessLayer.GetDiagnosen();

            lvDiagnosen.Items.Clear();
            lvDiagnosen.Sorting = SortOrder.None;

            lvDiagnosen.BeginUpdate();
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["DRG"]);
                lvi.Tag = (int)dataRow["ID_Diagnosen"];
                lvi.SubItems.Add(dataRow["U_GVD"].ToString());
                lvi.SubItems.Add(dataRow["M_GVD"].ToString());
                lvi.SubItems.Add(dataRow["O_GVD"].ToString());
                lvi.SubItems.Add((string)dataRow["DRG_Name"]);

                lvDiagnosen.Items.Add(lvi);
            }
            lvDiagnosen.EndUpdate();
        }

        private void DiagnosenKatalogView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Prozeduren/DRG-Katalog pflegen");

            radSortDRG.Checked = false;
            radSortDRG_Name.Checked = true;

            InitDiagnosen();
            PopulateDiagnosen();
        }

        protected void ValidateGVD(TextBox txt, string strName, ref string strMessage, ref bool bSuccess)
        {
            if (txt.Text.Length <= 0)
            {
                strMessage += "\n- " + strName + " fehlt";
                bSuccess = false;
            }
            else
            {
                float f;
                if (!float.TryParse(txt.Text, out f))
                {
                    strMessage += "\n- " + strName + " muss eine Zahl sein";
                    bSuccess = false;
                }
            }
        }
        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = "Eingabefehler:\n";

            ValidateGVD(txtU_GVD, "untere Grenzverweildauer", ref strMessage, ref bSuccess);
            ValidateGVD(txtM_GVD, "mittlere Grenzverweildauer", ref strMessage, ref bSuccess);
            ValidateGVD(txtO_GVD, "obere Grenzverweildauer", ref strMessage, ref bSuccess);

            if (txtDRG_Name.Text.Length <= 0)
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
            _oDiagnose["DRG"] = txtDRG.Text;
            _oDiagnose["DRG_Name"] = txtDRG_Name.Text;
            _oDiagnose["U_GVD"] = txtU_GVD.Text;
            _oDiagnose["M_GVD"] = txtM_GVD.Text;
            _oDiagnose["O_GVD"] = txtO_GVD.Text;
        }

        protected override void SaveObject()
        {
            BusinessLayer.InsertDiagnose(_oDiagnose);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();
                PopulateDiagnosen();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (Confirm("Hinweis:\n\nSie haben " + lvDiagnosen.SelectedItems.Count + " Einträge ausgewählt."
                + "\nEin Eintrag kann nur gelöscht werden, wenn er nirgends benutzt wird."
                + "\n\nFortfahren mit Löschen?"))
            {
                foreach (ListViewItem lvi in lvDiagnosen.SelectedItems)
                {
                    int nID_Diagnosen = (int)lvi.Tag;

                    if (nID_Diagnosen != -1)
                    {
                        BusinessLayer.DeleteDiagnose(nID_Diagnosen);
                    }
                }
                PopulateDiagnosen();
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvDiagnosen, true);
            if (lvi != null)
            {
                DataRow dataRow = BusinessLayer.CreateDataRowDiagnose();

                dataRow["DRG"] = txtDRG.Text;
                dataRow["DRG_Name"] = txtDRG_Name.Text;
                dataRow["U_GVD"] = txtU_GVD.Text;
                dataRow["M_GVD"] = txtM_GVD.Text;
                dataRow["O_GVD"] = txtO_GVD.Text;
                dataRow["ID_Diagnosen"] = lvi.Tag;

                BusinessLayer.UpdateDiagnose(dataRow);
                PopulateDiagnosen();
            }
        }

        private void lvDiagnosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvDiagnosen, false);
            if (lvi != null)
            {
                txtDRG.Text = lvi.Text;
                txtU_GVD.Text = lvi.SubItems[1].Text;
                txtM_GVD.Text = lvi.SubItems[2].Text;
                txtO_GVD.Text = lvi.SubItems[3].Text;
                txtDRG_Name.Text = lvi.SubItems[4].Text;
            }
        }

        private void radSortDRG_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortDRG.Checked)
            {
                // DRG ist die erste Spalte, das kann man sortieren, ohne irgendwelche Daten neu zu holen
                lvDiagnosen.Sorting = SortOrder.Ascending;
            }
        }

        private void radSortDRG_Name_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortDRG_Name.Checked)
            {
                PopulateDiagnosen();
            }
        }
    }
}