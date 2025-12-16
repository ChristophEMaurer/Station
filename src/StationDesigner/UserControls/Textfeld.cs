using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using Station;


namespace StationDesigner.UserControls
{
    public class Textfeld : Panel, IDiagramVisitable
    {
        DesignerView _diagramView;
        private int _nID_Texte;
        string _strText = "Text";

        public Textfeld(DesignerView v, int nID_Texte)
        {
            _diagramView = v;

            _nID_Texte = nID_Texte;
        }

        public string TheText
        {
            get { return _strText; }
            set { _strText = value; }
        }

        public int ID_Texte
        {
            get { return _nID_Texte; }
            set { _nID_Texte = value; }
        }

        public int LocationX
        {
            get { return Location.X; }
        }
        public int LocationY
        {
            get { return Location.Y; }
        }

        public BusinessLayer BusinessLayer
        {
            get { return _diagramView.BusinessLayer; }
        }

        public void OnTextfeldDelete(System.Object sender, System.EventArgs e)
        {
            BusinessLayer.DatabaseLayer.DesignerDeleteTextfeld(_nID_Texte);

            _diagramView.OnTextfeldDelete(this);
        }

        public void OnTextfeldNew(Textfeld text)
        {
            _diagramView.OnTextfeldNew(text);
        }

        public void CreateContextMenu()
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Eigenschaften", new EventHandler(this.OnTextfeldEdit)));
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(new MenuItem("Textfeld löschen", new EventHandler(this.OnTextfeldDelete)));
            this.ContextMenu = menu;
        }

        public void Accept(IDiagramVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create font and brush.
            using (Font drawFont = new Font("Arial", 8))
            {
                using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                {
                    PointF drawPoint = new PointF(0.0f, 0.0f);

                    // Draw string to screen.
                    e.Graphics.DrawString(_strText, drawFont, drawBrush, drawPoint);
                }
            }
        }
        private void PropertyDialog()
        {
            Dialogs.TextfeldView dlg = new Dialogs.TextfeldView(BusinessLayer, _strText);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                _strText = dlg.TheText;
                Invalidate();
            }
        }

        public void OnTextfeldEdit(System.Object sender, System.EventArgs e)
        {
            PropertyDialog();
        }
    }
}

