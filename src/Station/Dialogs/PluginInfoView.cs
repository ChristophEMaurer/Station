using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Station;

namespace Station
{
    public partial class PluginInfoView : Form
    {
        private string _strFilename;
        private BusinessLayer _oBusinessLayer;

        public PluginInfoView()
        {
            InitializeComponent();
        }

        public PluginInfoView(BusinessLayer b, string strFilename) : this()
        {
            _oBusinessLayer = b;
            _strFilename = strFilename;
        }

        private void PluginInfoView_Load(object sender, EventArgs e)
        {
            StationImport o = null;
            this.Text = _oBusinessLayer.AppTitle("Plugin Info");
            Assembly plugin = Assembly.LoadFile(_strFilename);

            Type[] types = plugin.GetTypes();
            // Iterate and find types derived from Form Instantiate them
            foreach (Type t in types)
            {
                if (t.BaseType == typeof(StationImport))
                {
                    o = (StationImport)Activator.CreateInstance(t);
                    string strAssemblyDescription =
                        ((AssemblyDescriptionAttribute)
                        plugin.GetCustomAttributes(
                        typeof(AssemblyDescriptionAttribute), false)[0]).Description;

                    txtAsmFilename.Text = _strFilename;
                    txtAsmDescription.Text = strAssemblyDescription;
                    txtInfo.Text = o.ImportDescription();
                }
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}