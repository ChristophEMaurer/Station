using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Printing;

namespace Station
{
    public partial class LegendeView : StationForm
    {
        public LegendeView(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }

        private void Legende_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle("Legende");
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
