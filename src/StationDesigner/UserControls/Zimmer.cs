using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using Station;

namespace StationDesigner.UserControls
{
    public class Zimmer : Panel, IDiagramVisitable
    {
        private const int BETT_WIDTH = 32;
        private int _nDefaultAnzahlBetten = 3;

        List<Bett> _betten = new List<Bett>();
        DesignerView _diagramView;

        private int _nID_Zimmer;
        private int _nStation;
        private int _nZimmerNummer;

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

        public Zimmer(DesignerView v, int nID_Zimmer, int nStation, int nZimmerNummer)
        {
            _diagramView = v;

            _nID_Zimmer = nID_Zimmer;
            _nStation = nStation;
            _nZimmerNummer = nZimmerNummer;
        }

        public void CreateComponents(
            int nInfoLocationX, int nInfoLocationY,
            int nIsolationLocationX, int nIsolationLocationY,
            int nNummerLocationX, int nNummerLocationY
            )
        {
            CreateZimmerInfoTextBox(nInfoLocationX, nInfoLocationY);
            CreateZimmerIsolationTextBox(nIsolationLocationX, nIsolationLocationY);
            CreateZimmerNummerLabel(nNummerLocationX, nNummerLocationY);
        }

        public void OnZimmerInfoNew(TextBox ctl)
        {
            this.Controls.Add(ctl);
            _diagramView.OnZimmerInfoNew(ctl);
        }

        public void OnZimmerIsolationNew(TextBox ctl)
        {
            this.Controls.Add(ctl);
            _diagramView.OnZimmerIsolationNew(ctl);
        }
        public void OnZimmerNummerNew(Label ctl)
        {
            _diagramView.OnZimmerNummerNew(ctl);
        }

        public Label CreateZimmerNummerLabel(int nLocationX, int nLocationY)
        {
            Label ctl = new Label();
            ctl.AutoSize = true;
            ctl.BorderStyle = BorderStyle.FixedSingle;
            ctl.Location = new System.Drawing.Point(nLocationX, nLocationY);
            ctl.TabStop = false;
            ctl.TabIndex = 0;
            ctl.Text = _nStation + "." + _nZimmerNummer;

            OnZimmerNummerNew(ctl);

            _lblNummer = ctl;

            return ctl;
        }

        public TextBox CreateZimmerInfoTextBox(int nLocationX, int nLocationY)
        {
            TextBox ctl = new TextBox();

            ctl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ctl.Location = new System.Drawing.Point(nLocationX, nLocationY);
            ctl.Multiline = true;
            ctl.ReadOnly = true;
            //ctl.Enabled = false;
            ctl.Size = new System.Drawing.Size(115, 47);
            ctl.TabIndex = 0;
            ctl.TabStop = false;
            ctl.WordWrap = false;
            ctl.Text = "ZimmerInfo";

            OnZimmerInfoNew(ctl);

            _txtInfo = ctl;

            return ctl;
        }
        public TextBox CreateZimmerIsolationTextBox(int nLocationX, int nLocationY)
        {
            TextBox ctl = new TextBox();
            ctl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ctl.Location = new System.Drawing.Point(nLocationX, nLocationY);
            ctl.Multiline = true;
            ctl.ReadOnly = true;
            //ctl.Enabled = false;
            ctl.Size = new System.Drawing.Size(79, 31);
            ctl.TabIndex = 0;
            ctl.TabStop = false;
            ctl.WordWrap = false;
            ctl.Text = "Isolation";

            OnZimmerIsolationNew(ctl);

            _txtIsolation = ctl;

            return ctl;
        }
        public int Station
        {
            get { return _nStation; }
            set { _nStation = value; }
        }

        public int ID_Zimmer
        {
            get { return _nID_Zimmer; }
            set { _nID_Zimmer = value; }
        }

        public int ZimmerNummer
        {
            get { return _nZimmerNummer; }
            set { _nZimmerNummer = value; }
        }

        public List<Bett> Betten
        {
            get { return _betten; }
        }

        public int NummerLocationX
        {
            get { return this._lblNummer.Location.X; }
        }
        public int NummerLocationY
        {
            get { return this._lblNummer.Location.Y; }
        }
        public int IsolationLocationX
        {
            get { return _txtIsolation.Location.X; }
        }
        public int IsolationLocationY
        {
            get { return _txtIsolation.Location.Y; }
        }
        public int InfoLocationX
        {
            get { return _txtInfo.Location.X; }
        }
        public int InfoLocationY
        {
            get { return _txtInfo.Location.Y; }
        }
        public int LocationX
        {
            get { return Location.X; }
        }
        public int LocationY
        {
            get { return Location.Y; }
        }
        public int DefaultAnzahlBetten
        {
            get { return _nDefaultAnzahlBetten; }
            set { _nDefaultAnzahlBetten = value; }
        }

        public BusinessLayer BusinessLayer
        {
            get { return _diagramView.BusinessLayer; }
        }

        public int AnzahlBetten
        {
            get { return _betten.Count; }
        }

        private void UpdateBettenNummern()
        {
            for (int i = 0; i < _betten.Count; i++)
            {
                _betten[i].BettenNummer = i + 1;
                _betten[i].Invalidate();
            }
        }

        internal void OnBettNew(Bett bett)
        {
            _betten.Add(bett);
            this.Controls.Add(bett);
            _diagramView.OnBettNew(bett);
        }

        internal void OnBettDelete(Bett bett)
        {
            _betten.Remove(bett);
            this.Controls.Remove(bett);
            _diagramView.OnBettDelete(bett);

            UpdateBettenNummern();
        }

        public void OnZimmerDelete(System.Object sender, System.EventArgs e)
        {
            BusinessLayer.DatabaseLayer.DesignerDeleteBetten(_nID_Zimmer);
            BusinessLayer.DatabaseLayer.DesignerDeleteZimmer(_nID_Zimmer);

            _diagramView.OnZimmerDelete(this);
        }

        public void OnZimmerNew(Zimmer zimmer)
        {
            _diagramView.OnZimmerNew(zimmer);
        }

        public void CreateContextMenu()
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Zimmer kopieren", new EventHandler(this.OnZimmerKopie)));
            menu.MenuItems.Add(new MenuItem(_nDefaultAnzahlBetten + " neue Betten", new EventHandler(this.OnZimmerNeueBetten)));
            menu.MenuItems.Add(new MenuItem("Neues Bett anlegen", new EventHandler(this.OnZimmerNeuesBett)));
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(new MenuItem("Eigenschaften", new EventHandler(this.OnZimmerEdit)));
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(new MenuItem("Zimmer löschen", new EventHandler(this.OnZimmerDelete)));
            this.ContextMenu = menu;
        }

        public void OnZimmerNeueBetten(System.Object sender, System.EventArgs e)
        {
            AddDefaultBetten();
        }
        public void OnZimmerNeuesBett(System.Object sender, System.EventArgs e)
        {
            AddBetten(1);
        }

        public void OnZimmerKopie(System.Object sender, System.EventArgs e)
        {
            _diagramView.ZimmerKopie(this);
        }

        public Bett CreateBett(DataRow rowBett)
        {
            Bett pb = new Bett(this, (int) rowBett["ID_Betten"], (int) rowBett["BettenNummer"]);
            pb.BorderStyle = BorderStyle.FixedSingle;
            pb.Size = new System.Drawing.Size(BETT_WIDTH, BETT_WIDTH);
            pb.Location = new System.Drawing.Point((int)rowBett["LocationX"], (int)rowBett["LocationY"]);
            pb.CreateContextMenu();

            OnBettNew(pb);

            return pb;
        }

        public Bett CreateBett(int nID_Zimmer, int nBettenNummer, int nLocationX, int nLocationY)
        {
            DataRow row = _diagramView.BusinessLayer.DatabaseLayer.CreateDataRowBett();
            row["ID_Zimmer"] = (int)nID_Zimmer;
            row["BettenNummer"] = (int)nBettenNummer;
            row["LocationX"] = (int)nLocationX;
            row["LocationY"] = (int)nLocationY;

            int nID_Betten = _diagramView.BusinessLayer.DatabaseLayer.DesignerInsertBett(row);
            row = _diagramView.BusinessLayer.DatabaseLayer.DesignerGetBett(nID_Betten);

            return CreateBett(row);
        }


        public void AddBetten(int nCount)
        {
            for (int i = 0; i < nCount; i++)
            {
                Bett bett = CreateBett(_nID_Zimmer, i + 1, i * BETT_WIDTH, 0);
            }
            UpdateBettenNummern();
        }

        public void AddDefaultBetten()
        {
            AddBetten(_nDefaultAnzahlBetten);
        }

        public void Accept(IDiagramVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Unload()
        {
            foreach (Bett bett in Betten)
            {
                this.Controls.Remove(bett);
            }
            Betten.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            string strText = _nStation + "." + _nZimmerNummer;

            // Create font and brush.
            using (Font drawFont = new Font("Arial", 8))
            {
                using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                {
                    PointF drawPoint = new PointF(0.0f, 0.0f);

                    // Draw string to screen.
                    e.Graphics.DrawString(strText, drawFont, drawBrush, drawPoint);
                }
            }
        }
        private void PropertyDialog()
        {
            Dialogs.ZimmerView dlg = new Dialogs.ZimmerView(BusinessLayer, _nStation, _nZimmerNummer);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                _nStation = dlg.Station;
                _nZimmerNummer = dlg.ZimmerNummer;
                Invalidate();
                ControlZimmerNummer.Text = _nStation + "." + _nZimmerNummer;
                this.ControlZimmerNummer.Invalidate();
            }
        }

        public void OnZimmerEdit(System.Object sender, System.EventArgs e)
        {
            PropertyDialog();
        }
    }
}

