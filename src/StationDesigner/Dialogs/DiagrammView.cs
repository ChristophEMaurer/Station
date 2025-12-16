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
    public partial class DiagrammView : StationDesignerForm
    {
        private string _strName;
        private string _strBeschreibung;

        public DiagrammView(BusinessLayer b, string strName, string strBeschreibung)
            : base(b)
        {
            _strName = strName;
            _strBeschreibung = strBeschreibung;

            InitializeComponent();

            this.Text = "Diagramm " + _strName + "-" + _strBeschreibung;
        }

        public string DiagrammName
        {
            get { return _strName; }
            set { _strName = value; }
        }
        public string DiagrammBeschreibung
        {
            get { return _strBeschreibung; }
            set { _strBeschreibung = value; }
        }

        protected override void Object2Control()
        {
            this.txtName.Text = _strName;
            this.txtBeschreibung.Text = _strBeschreibung;
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;
            string strMessage = "Eingabefehler:\n";

            if (txtName.Text.Length == 0)
            {
                strMessage += "\n- Name muss ausgefüllt sein.";
                fSuccess = false;
            }
            if (txtBeschreibung.Text.Length == 0)
            {
                strMessage += "\n- Beschreibung muss ausgefüllt sein.";
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
            _strName = txtName.Text;
            _strBeschreibung = txtBeschreibung.Text;
        }


        private void cmdApply_Click(object sender, EventArgs e)
        {
            this.OKClicked();
        }

        private void DiagrammView_Load(object sender, EventArgs e)
        {
            txtName.Text = _strName;
            txtBeschreibung.Text = _strBeschreibung;
        }
    }
}