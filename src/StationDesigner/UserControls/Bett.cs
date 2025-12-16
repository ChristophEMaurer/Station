using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace StationDesigner.UserControls
{
    public class Bett : Panel, IDiagramVisitable
    {
        private int _nID_Betten;
        private int _nBettenNummer;
        private Zimmer _oZimmer;

        public Bett(Zimmer zimmer, int nID_Betten, int nBettenNummer)
        {
            _oZimmer = zimmer;
            _nID_Betten = nID_Betten;
            _nBettenNummer = nBettenNummer;
        }

        public void CreateContextMenu()
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Bett löschen", new EventHandler(this.OnBettDelete)));
            this.ContextMenu = menu;
        }

        public int BettenNummer
        {
            get { return _nBettenNummer; }
            set { _nBettenNummer = value; }
        }
        public int ID_Zimmer
        {
            get { return _oZimmer.ID_Zimmer; }
        }
        public int ID_Betten
        {
            get { return _nID_Betten; }
            set { _nID_Betten = value; }
        }
        public int LocationX
        {
            get { return Location.X; }
        }
        public int LocationY
        {
            get { return Location.Y; }
        }


        public void OnBettDelete(System.Object sender, System.EventArgs e)
        {
            _oZimmer.BusinessLayer.DatabaseLayer.DesignerDeleteBett(_nID_Betten);
            _oZimmer.OnBettDelete(this);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create font and brush.
            using (Font drawFont = new Font("Arial", 16))
            {
                using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                {
                    // Create point for upper-left corner of drawing.
                    PointF drawPoint = new PointF(5.0F, 5.0F);

                    // Draw string to screen.
                    e.Graphics.DrawString(_nBettenNummer.ToString(), drawFont, drawBrush, drawPoint);
                }
            }
        }

        public void Accept(IDiagramVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
