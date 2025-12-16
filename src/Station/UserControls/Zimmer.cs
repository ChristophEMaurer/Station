using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Station.UserControls
{
    public class Zimmer : Panel
    {
        protected TextBox _txtIsolation;
        protected TextBox _txtInfo;
        protected Label _lblNummer;

        public Label ControlZimmerNummer
        {
            get { return _lblNummer; }
            set { _lblNummer = value; }
        }
        public TextBox ControlIsolation
        {
            get { return _txtIsolation; }
            set { _txtIsolation = value; }
        }
        public TextBox ControlInfo
        {
            get { return _txtInfo; }
            set { _txtInfo = value; }
        }
    }
}
