using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace StationDesigner.UserControls
{
    public class Diagramm : Panel
    {
        private bool _bShowGrid = true;
        private int _nGridSize = 8;

        public Diagramm()
        {
        }

        public bool ShowGrid
        {
            get { return _bShowGrid; }
            set { _bShowGrid = value; }
        }
        public int GridSize
        {
            get { return _nGridSize; }
            set { _nGridSize = value; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_bShowGrid)
            {
                Graphics g = e.Graphics;

                for (int i = 0; i < this.Width; i += _nGridSize)
                {
                    g.DrawLine(Pens.Gray, i, 0, i, Height); 
                }
                for (int i = 0; i < this.Height; i += _nGridSize)
                {
                    g.DrawLine(Pens.Gray, 0, i, Width, i);
                }
            }
        }
    }
}

