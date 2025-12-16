using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Station.AppFramework;

namespace Station
{
    public partial class MainView : StationForm
    {
        private List<Form> _oForms = new List<Form>();

        public MainView(BusinessLayer b, string strPath)
            : base(b)
        {
            InitializeComponent();

            this.Location = new Point(0, 0);
        }

        public void RemoveDiagram(DynamicDiagramView v)
        {
            _oForms.Remove(v);
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.Text = BusinessLayer.AppTitle();

            CreateDiagramButtons();
        }

        private void CreateDiagramButtons()
        {
            int nX = 5;
            int nY = 15;

            DataView dvDiagrams = BusinessLayer.DatabaseLayer.GetDiagramme();

            foreach (DataRow row in dvDiagrams.Table.Rows)
            {
                Button b = new Button();
                b.Tag = row;
                b.Text = (string)row["Name"];
                b.Location = new Point(nX, nY);
                b.Size = new Size(200, 50);
                b.Click += new EventHandler(b_Click);

                this.Controls.Add(b);

                nY += b.Height + 10;
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            bool bFound = false;

            Button b = (Button)sender;

            DataRow diagram = (DataRow)b.Tag;

            foreach (DynamicDiagramView item in _oForms)
            {
                if (item.ID_Diagramme == (int)diagram["ID_Diagramme"])
                {
                    bFound = true;
                    item.Focus();
                    break;
                }
            }

            if (!bFound)
            {
                DynamicDiagramView view = new DynamicDiagramView(this, BusinessLayer, diagram);
                _oForms.Add(view);
                view.Show();
                view.Location = new Point(this.Location.X + this.Width, Location.Y);
            }
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_oForms.Count > 1)
            {
                if (!this.Confirm("Sind Sie sicher, dass Sie alle Fenster schließen möchten?"))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

