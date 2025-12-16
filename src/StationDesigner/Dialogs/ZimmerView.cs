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
    public partial class ZimmerView : StationDesignerForm
    {
        private int _nStation;
        private int _nZimmernummer;

        public ZimmerView(BusinessLayer b, int nStation, int nZimmerNummer)
            : base(b)
        {
            _nStation = nStation;
            _nZimmernummer = nZimmerNummer;

            InitializeComponent();

            this.Text = "Zimmer " + _nStation + "." + _nZimmernummer;
        }

        public int Station
        {
            get { return _nStation; }
        }
        public int ZimmerNummer
        {
            get { return _nZimmernummer; }
        }


        protected override void Object2Control()
        {
            this.txtStation.Text = _nStation.ToString();
            this.txtZimmernummer.Text = _nZimmernummer.ToString();
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;
            string strMessage = "Eingabefehler:\n";
            int nResult;

            if (txtStation.Text.Length == 0)
            {
                strMessage += "\n- Station muss ausgefüllt sein.";
                fSuccess = false;
            }
            else if (!int.TryParse(txtStation.Text, out nResult))
            {
                strMessage += "\n- Station muss eine Zahl sein.";
                fSuccess = false;
            }
            if (txtZimmernummer.Text.Length == 0)
            {
                strMessage += "\n- Zimmernummer muss ausgefüllt sein.";
                fSuccess = false;
            }
            else if (!int.TryParse(txtZimmernummer.Text, out nResult))
            {
                strMessage += "\n- Zimmernummer muss eine Zahl sein.";
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
            int.TryParse(txtStation.Text, out _nStation);
            int.TryParse(txtZimmernummer.Text, out _nZimmernummer);
        }


        private void cmdApply_Click(object sender, EventArgs e)
        {
            this.OKClicked();
        }

        private void ZimmerView_Load(object sender, EventArgs e)
        {
            Object2Control();
        }
    }
}