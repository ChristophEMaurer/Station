using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms;
using Station;

namespace StationDesigner
{
    /// <summary>
    /// Basisklassen von allen Views. Enthaelt Station und damit den Zugriff 
    /// auf den BusinessLayer
    /// </summary>
    public class StationDesignerForm : DatabaseForm
    {
        protected const string EINGABEFEHLER = "Eingabefehler:\n";

        protected StationDesignerForm()
        {
        }

        protected StationDesignerForm(BusinessLayer b) : base(b)
        {
        }
        
        protected internal BusinessLayer BusinessLayer
        {
            get { return (BusinessLayer)_businessLayerBase; }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationDesignerForm));
            this.SuspendLayout();
            // 
            // StationDesignerView
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StationDesignerView";
            this.ResumeLayout(false);
        }

        protected override string GetFormNameForResourceTexts()
        {
            throw new NotImplementedException();
        }
        public override void DebugPrintControlContents(Control control)
        {
        }
        public override void DebugPrintControlClicked(Control control)
        {
        }

    }
}
