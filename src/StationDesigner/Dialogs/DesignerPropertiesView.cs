using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms;
using Utility;
using Station;

namespace StationDesigner.Dialogs
{
    public partial class DesignerPropertiesView : StationDesignerForm
    {
        DataRow _rowProperties;

        public DesignerPropertiesView(BusinessLayer b, DataRow oRow)
            : base(b)
        {
            _rowProperties = oRow;

            InitializeComponent();

            this.Text = BusinessLayer.AppTitle() + " - Eigenschaften";
        }

        public DataRow Properties
        {
            get { return _rowProperties; }
        }

        protected override void Object2Control()
        {
            chkSnapToGrid.Checked = (1 == (int)_rowProperties["SnapToGrid"]) ? true : false;
            chkShowGrid.Checked = (1 == (int)_rowProperties["ShowGrid"]) ? true : false;
            txtGridSize.Text = _rowProperties["GridSize"].ToString();
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;
            string strMessage = "Eingabefehler:\n";
            int nResult;

            if (txtGridSize.Text.Length == 0)
            {
                strMessage += "\n- '" + lblGridSize.Text + "' muss ausgefüllt sein.";
                fSuccess = false;
            }
            else
            {
                if (int.TryParse(txtGridSize.Text, out nResult))
                {
                    if (nResult < 1 || nResult > 32)
                    {
                        strMessage += "\n- '" + lblGridSize.Text + "' muss eine Zahl zwischen 1 und 32 sein.";
                        fSuccess = false;
                    }
                }
                else
                {
                    strMessage += "\n- '" + lblGridSize.Text + "' muss eine Zahl sein.";
                    fSuccess = false;
                }
            }
            if (!fSuccess)
            {
                MessageBox(strMessage);
            }
            return fSuccess;
        }

        protected override void Control2Object()
        {
            _rowProperties["SnapToGrid"] = chkSnapToGrid.Checked ? "1" : "0";
            _rowProperties["ShowGrid"] = chkShowGrid.Checked ? "1" : "0";
            _rowProperties["GridSize"] = txtGridSize.Text;
        }


        private void cmdApply_Click(object sender, EventArgs e)
        {
            this.OKClicked();
        }

        private void DesignerPropertiesView_Load(object sender, EventArgs e)
        {
            Object2Control();
        }
    }
}