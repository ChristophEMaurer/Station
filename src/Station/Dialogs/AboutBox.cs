using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace Station
{
    partial class AboutBox : StationForm
    {
        private bool _bIsSplashscreen;

        public AboutBox(BusinessLayer businessLayer, bool bIsSplashscreen) : base(businessLayer)
        {
            _bIsSplashscreen = bIsSplashscreen;

            InitializeComponent();

            pbProgress.Visible = bIsSplashscreen;
            cmdOK.Visible = !bIsSplashscreen;
            ControlBox = !bIsSplashscreen;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            Text = BusinessLayer.AppTitle("Info über ");
            lblProduct.Text = BusinessLayer.AppTitle();
            lblVersion.Text = "Version " + BusinessLayer.VersionString;
            lblCopyright.Text = "Copyright © 2007-2010 Christoph Maurer"
                + Environment.NewLine
                + "ch.maurer@gmx.de"
                ;

            PopulateInfos();

            pbProgress.Minimum = 0;
            pbProgress.Maximum = 20;
        }
        private void AddRow(string strKey, string strValue)
        {
            ListViewItem lvi = new ListViewItem(strKey);
            lvi.SubItems.Add(strValue);
            lvInfos.Items.Add(lvi);
        }

        private void PopulateInfos()
        {
            lvInfos.BeginUpdate();
            lvInfos.Clear();

            DefaultListViewProperties(lvInfos);

            lvInfos.Columns.Add("", 100, HorizontalAlignment.Left);
            lvInfos.Columns.Add("", -2, HorizontalAlignment.Left);

            System.OperatingSystem os = System.Environment.OSVersion;

            AddRow("Version", BusinessLayer.DatabaseVersion);
            AddRow("Datenbank", BusinessLayer.PathDatabase);
            AddRow("Logon", System.Environment.MachineName + System.IO.Path.DirectorySeparatorChar + System.Environment.UserName);
            AddRow("Betriebssystem", os.VersionString);
            AddRow(".NET Framework", System.Environment.Version.ToString());
        }

        public void Increment()
        {
            if (pbProgress.Value >= pbProgress.Maximum)
            {
                pbProgress.Value = pbProgress.Minimum;
            }
            else
            {
                pbProgress.Value++;
            }
            Application.DoEvents();
        }

    }
}
