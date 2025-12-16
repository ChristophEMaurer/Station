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
    public partial class TextfeldView : StationDesignerForm
    {
        private string _strText;

        public TextfeldView(BusinessLayer b, string strText)
            : base(b)
        {
            _strText = strText;

            InitializeComponent();

            this.Text = "Textfeld";
        }

        public string TheText
        {
            get { return _strText; }
            set { _strText = value; }
        }

        protected override void Object2Control()
        {
            this.txtText.Text = _strText;
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;
            string strMessage = "Eingabefehler:\n";

            if (txtText.Text.Length == 0)
            {
                strMessage += "\n- Text muss ausgefüllt sein.";
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
            _strText = txtText.Text;
        }


        private void cmdApply_Click(object sender, EventArgs e)
        {
            this.OKClicked();
        }

        private void DiagrammView_Load(object sender, EventArgs e)
        {
            txtText.Text = _strText;
        }
    }
}