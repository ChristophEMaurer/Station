using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Utility;
using Station.UserControls;

namespace Station
{
    /// <summary>
    /// PictureBox.Tag = Betten.ID_Betten
    /// Panel.Tag = Zimmer.ID_Zimmer
    /// </summary>
    public partial class DynamicDiagramView : StationForm
    {
        private AboutBox _About;
        private PrintStation _oPrinting;
        private Rectangle dragBoxFromMouseDown;
        private Point screenOffset;
        private string _strAppPath;
        private InformationBox _oInfoBox;
        private DataRow _oDiagram;
        private MainView _oMainView;

        public DynamicDiagramView(BusinessLayer b, DataRow diagram)
            : this(null, b, diagram)
        {
        }

        public DynamicDiagramView(MainView mainView, BusinessLayer b, DataRow diagram)
            : base(b)
        {
            _oMainView = mainView;
            _oDiagram = diagram;
            _strAppPath = Tools.GetAppSubDir(Application.StartupPath.ToLower(), "");

            _oPrinting = new PrintStation(b, this, diagram);

            InitializeComponent();

            this.Text = BusinessLayer.AppTitle(diagram["Name"] + "-" + diagram["Beschreibung"]);
        }

        public void Progress()
        {
            _About.Increment();
            Application.DoEvents();
        }
        public DataRow Diagram
        {
            get { return _oDiagram; }
        }

        public ListView ListViewInfektionen
        {
            get { return _oInfoBox.ListViewInfektionen; }
        }
        public string InfoFreieMaennerbetten
        {
            get { return _oInfoBox.FreieMaennerbetten; }
        }
        public string InfoFreieFrauenbetten
        {
            get { return _oInfoBox.FreieFrauenbetten; }
        }
        public string InfoFreieZimmer
        {
            get { return _oInfoBox.FreieZimmer; }
        }
        public string InfoBelegung
        {
            get { return _oInfoBox.Belegung; }
        }

        public int ID_Diagramme
        {
            get { return (int)_oDiagram["ID_Diagramme"]; }
        }
        private void CreateDiagram()
        {
            pnlDiagram.SuspendLayout();

            DataRow oDiagram = BusinessLayer.DatabaseLayer.DesignerGetDiagramm(ID_Diagramme);

            _oInfoBox = new InformationBox(BusinessLayer, ID_Diagramme);
            _oInfoBox.Location = new Point((int)oDiagram["InfoLocationX"], (int)oDiagram["InfoLocationY"]);
            pnlDiagram.Controls.Add(_oInfoBox);

            DataView dvTexte = BusinessLayer.DatabaseLayer.DesignerGetTexte(ID_Diagramme);
            foreach (DataRow rowText in dvTexte.Table.Rows)
            {
                Progress();

                // Ein Text wird dargestellt durch ein Label
                Label lblText = new Label();
                lblText.Text = (string)rowText["Text"];
                lblText.BackColor = System.Drawing.SystemColors.Control;
                lblText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                lblText.Location = new System.Drawing.Point((int)rowText["LocationX"], (int)rowText["LocationY"]);
                lblText.AutoSize = true;
                lblText.TabStop = false;
                lblText.TabIndex = 0;

                pnlDiagram.Controls.Add(lblText);
            }

            DataView dvZimmer = BusinessLayer.DatabaseLayer.DesignerGetZimmers(ID_Diagramme);

            int nID_Zimmer;
            int nStation;
            int nZimmerNummer;

            foreach (DataRow rowZimmer in dvZimmer.Table.Rows)
            {
                Progress();

                nID_Zimmer = (int)rowZimmer["ID_Zimmer"];
                nStation = (int)rowZimmer["Station"];
                nZimmerNummer = (int)rowZimmer["ZimmerNummer"];

                // Ein Zimmer wird dargestellt durch ein Panel
                Zimmer pnlZimmer = new Zimmer();
                pnlZimmer.Tag = nID_Zimmer;
                pnlZimmer.BackColor = System.Drawing.SystemColors.Control;
                pnlZimmer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pnlZimmer.Location = new System.Drawing.Point((int)rowZimmer["LocationX"], (int)rowZimmer["LocationY"]);
                pnlZimmer.Size = new System.Drawing.Size((int)rowZimmer["Width"], (int)rowZimmer["Height"]);
                pnlZimmer.TabStop = false;
                pnlZimmer.TabIndex = 0;

                // Alle Betten des Zimmers
                DataView dvBetten = BusinessLayer.DatabaseLayer.DesignerGetBetten(nID_Zimmer);
                foreach (DataRow rowBett in dvBetten.Table.Rows)
                {
                    Progress();

                    // Ein Bett wird dargestellt durch eine PictureBox
                    PictureBox pb = new PictureBox();
                    pb.Tag = (int)rowBett["ID_Betten"];
                    pb.Location = new System.Drawing.Point((int)rowBett["LocationX"], (int)rowBett["LocationY"]);
                    pb.Size = new System.Drawing.Size(32, 32);
                    pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                    pb.TabStop = false;
                    pb.TabIndex = 0;
                    InitDragAndDrop(pb);

                    pnlZimmer.Controls.Add(pb);
                }

                // Der kleine Kasten, der die Zimmernummer anzeigt
                Label lblNummer = new Label();
                pnlZimmer.ControlZimmerNummer = lblNummer;
                lblNummer.AutoSize = true;
                lblNummer.Location = new System.Drawing.Point((int)rowZimmer["NummerLocationX"], (int)rowZimmer["NummerLocationY"]);
                lblNummer.Size = new System.Drawing.Size(19, 14);
                lblNummer.TabStop = false;
                lblNummer.TabIndex = 0;
                lblNummer.Text = nStation + "." + nZimmerNummer;
                pnlDiagram.Controls.Add(lblNummer);

                // TextBox "Isolation", die erscheint, wenn nur ein Bett im Zimmer ist
                TextBox txtIsolation = new TextBox();
                pnlZimmer.ControlIsolation = txtIsolation;
                txtIsolation.BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtIsolation.Location = new System.Drawing.Point((int)rowZimmer["IsolationLocationX"], (int)rowZimmer["IsolationLocationY"]);
                txtIsolation.Multiline = true;
                txtIsolation.ReadOnly = true;
                txtIsolation.Size = new System.Drawing.Size(79, 31);
                txtIsolation.TabStop = false;
                txtIsolation.TabIndex = 0;
                txtIsolation.WordWrap = false;
                pnlZimmer.Controls.Add(txtIsolation);

                // TextBox, die für alle Betten die Information anzeigt
                TextBox txtZimmer = new TextBox();
                pnlZimmer.ControlInfo = txtZimmer;
                txtZimmer.BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtZimmer.Location = new System.Drawing.Point((int)rowZimmer["InfoLocationX"], (int)rowZimmer["InfoLocationY"]);
                txtZimmer.Multiline = true;
                txtZimmer.ReadOnly = true;
                txtZimmer.Size = new System.Drawing.Size(115, 47);
                txtZimmer.TabIndex = 0;
                txtZimmer.TabStop = false;
                txtZimmer.WordWrap = false;
                pnlZimmer.Controls.Add(txtZimmer);
                InitTextBoxZimmer(txtZimmer);

                DisplayZimmerDynamic(pnlZimmer);

                pnlDiagram.Controls.Add(pnlZimmer);
            }

            this.pnlDiagram.ResumeLayout(false);
            this.pnlDiagram.PerformLayout();
        }

        private void pbMouseDown(object sender, MouseEventArgs e)
        {
            // the size that the mouse can move before a drag event should be started.                
            Size dragSize = SystemInformation.DragSize;

            // Create a rectangle using the DragSize, with the mouse position being
            // at the center of the rectangle.
            dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                           e.Y - (dragSize.Height / 2)), dragSize);
        }

        private void pbMouseUp(object sender, MouseEventArgs e)
        {
            dragBoxFromMouseDown = Rectangle.Empty;

            if (e.Button == MouseButtons.Left)
            {
                PictureBoxClicked(sender, e);
            }
        }
        private void EditZimmer(Zimmer panel)
        {
            DataRow zimmer = Panel2Zimmer(panel);

            DialogResult dr = EditZimmer(zimmer);
            if (dr == DialogResult.OK)
            {
                PopulateInfos();
                DisplayZimmerDynamic(panel);
            }
        }
        private DialogResult EditZimmer(DataRow zimmer)
        {
            ZimmerView dlg = new ZimmerView(BusinessLayer, ID_Diagramme, zimmer);

            return dlg.ShowDialog();
        }
        private void PictureBoxClicked(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Zimmer panel = (Zimmer)pictureBox.Parent;

            EditZimmer(panel);
        }

        private void pbMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // The screenOffset is used to account for any desktop bands 
                    // that may be at the top or left side of the screen when 
                    // determining when to cancel the drag drop operation.
                    screenOffset = SystemInformation.WorkingArea.Location;

                    // Proceed with the drag-and-drop, passing in the list item.                    
                    PictureBox pb = (PictureBox)sender;

                    DragDropEffects dropEffect = pb.DoDragDrop(pb, DragDropEffects.All | DragDropEffects.Link);

                    // If the drag operation was a move then remove the item.
                    if (dropEffect == DragDropEffects.Move)
                    {
                    }
                }
            }
        }
        private void pbDragDrop(object sender, DragEventArgs e)
        {
            PictureBox pbDst = sender as PictureBox;

            // Ensure that the list item index is contained in the data.
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {

                PictureBox pbSrc = (PictureBox)e.Data.GetData(typeof(PictureBox));

                // Perform drag-and-drop, depending upon the effect.
                if (e.Effect == DragDropEffects.Copy ||
                    e.Effect == DragDropEffects.Move)
                {
                    if (MovePatient((int)pbSrc.Tag, (int)pbDst.Tag))
                    {
                        DisplayZimmerDynamic((Zimmer)pbSrc.Parent);
                        DisplayZimmerDynamic((Zimmer)pbDst.Parent);
                    }
                }
            }
        }
        private bool MovePatient(int nID_BettenFrom, int ID_BettenTo)
        {
            return BusinessLayer.MovePatient(nID_BettenFrom, ID_BettenTo);
        }
        private void pbDragOver(object sender, DragEventArgs e)
        {
            // Determine whether string data exists in the drop data. If not, then
            // the drop effect reflects that the drop cannot occur.
            if (!e.Data.GetDataPresent(typeof(PictureBox)))
            {

                e.Effect = DragDropEffects.None;
                return;
            }

            // Set the effect based upon the KeyState.
            if ((e.KeyState & (8 + 32)) == (8 + 32) &&
                (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                // KeyState 8 + 32 = CTL + ALT

                // Link drag-and-drop effect.
                e.Effect = DragDropEffects.Link;

            }
            else if ((e.KeyState & 32) == 32 &&
                (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {

                // ALT KeyState for link.
                e.Effect = DragDropEffects.Link;

            }
            else if ((e.KeyState & 4) == 4 &&
              (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {

                // SHIFT KeyState for move.
                e.Effect = DragDropEffects.Move;

            }
            else if ((e.KeyState & 8) == 8 &&
              (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {

                // CTL KeyState for copy.
                e.Effect = DragDropEffects.Copy;

            }
            else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {

                // By default, the drop action should be move, if allowed.
                e.Effect = DragDropEffects.Move;

            }
            else
                e.Effect = DragDropEffects.None;

            // Get the index of the item the mouse is below. 

            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.

        }
        private void pbQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            // Cancel the drag if the mouse moves off the form.
            PictureBox pb = sender as PictureBox;

            if (pb != null)
            {

                Form f = pb.FindForm();

                // Cancel the drag if the mouse moves off the form. The screenOffset
                // takes into account any desktop bands that may be at the top or left
                // side of the screen.
                if (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) ||
                    ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) ||
                    ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) ||
                    ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom))
                {
                    e.Action = DragAction.Cancel;
                }
            }
        }
        void ContextMenuPatient_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;

            PictureBox pb = (PictureBox)mi.Tag;
            int nID_Betten = (int)pb.Tag;

            DataRow oPatient = BusinessLayer.GetPatientByBett(nID_Betten);
            if (oPatient != null)
            {
                PatientView dlg = new PatientView(BusinessLayer, ID_Diagramme, oPatient);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    this.PopulateForm();
                }
            }
        }

        private void PopulateInfos()
        {
            _oInfoBox.AnzeigeInfos();
        }
        private void PopulateInfektionen()
        {
            _oInfoBox.PopulateInfektionen();
        }

        private void DisplayStation()
        {
            foreach (Control c in this.pnlDiagram.Controls)
            {
                Progress();

                if (c.GetType() == typeof(Panel))
                {
                    DisplayZimmerDynamic((Zimmer)c);
                }
            }

            PopulateInfos();
            PopulateInfektionen();
        }

        private void PopulateForm()
        {
            _About = new AboutBox(BusinessLayer, true);
            _About.Show();

            DisplayStation();

            _About.Close();
            _About = null;
        }

        private void InitDragAndDrop(PictureBox pb)
        {
            pb.AllowDrop = true;
            pb.MouseDown += new MouseEventHandler(pbMouseDown);
            pb.MouseMove += new MouseEventHandler(pbMouseMove);
            pb.MouseUp += new MouseEventHandler(pbMouseUp);

            pb.DragDrop += new DragEventHandler(pbDragDrop);
            pb.DragOver += new DragEventHandler(pbDragOver);
            pb.QueryContinueDrag += new QueryContinueDragEventHandler(pbQueryContinueDrag);

            ContextMenu cm = new ContextMenu();
            MenuItem mi = new MenuItem("Patient bearbeiten...");
            mi.Tag = pb;
            mi.Click += new EventHandler(ContextMenuPatient_Click);

            cm.MenuItems.Add(mi);
            pb.ContextMenu = cm;
        }

        private void InitTextBoxZimmer(TextBox txt)
        {
            txt.Multiline = true;
            txt.WordWrap = false;
            txt.ScrollBars = ScrollBars.None;
        }
        
        private Zimmer FindPanelForPatient(int nID_Patient)
        {
            Zimmer pnlZimmer = null;

            DataRow row = BusinessLayer.GetPatient(nID_Patient);

            foreach (Control c in this.pnlDiagram.Controls)
            {
                if (c.GetType() == typeof(Zimmer))
                {
                    Zimmer pnl = (Zimmer)c;
                    foreach (Control pb in pnl.Controls)
                    {
                        if (pb.GetType() == typeof(PictureBox))
                        {
                            int nID_Betten = (int)pb.Tag;
                            if (row["ID_Betten"] != DBNull.Value && nID_Betten == (int)row["ID_Betten"])
                            {
                                pnlZimmer = (Zimmer) pnl;
                                break;
                            }
                        }
                    }
                }
                if (pnlZimmer != null)
                {
                    break;
                }
            }

            return pnlZimmer;
        }

        private void DisplayZimmerDynamicPatient(int nID_Patient)
        {
            Zimmer pnlZimmer = FindPanelForPatient(nID_Patient);
            DisplayZimmerDynamic(pnlZimmer);
        }

        /// <summary>
        /// Rechts neben dem ersten Bett ist eine TextBox mit Format txt5_51_
        /// Unten unter den Betten ist eine TextBox mit Format wie die oben, nur ohne den Unterstrich hinten, txt5_51
        /// </summary>
        /// <param name="panel"></param>
        public void DisplayZimmerDynamic(Zimmer panel)
        {
            TextBox txtZimmer = panel.ControlInfo;
            TextBox txtIsolation = panel.ControlIsolation;

            DisplayZimmerDynamic(panel, txtZimmer, txtIsolation);
        }

        private void DisplayPictureBox(PictureBox pb, DataRow oPatient)
        {
            int nPrivat = 0;

            pb.Visible = true;

            pb.Tag = oPatient["BettenID_Betten"];

            if (oPatient["ID_Patienten"] == DBNull.Value)
            {
                pb.Image = ImageBettLeer();
            }
            else
            {
                nPrivat = (int)oPatient["Privat"];

                if (nPrivat > 0)
                {
                    if (oPatient["Entlassungsdatum"] != DBNull.Value && DateTime.Today > (DateTime)oPatient["Entlassungsdatum"])
                    {
                        pb.Image = ImageBettPrivatED();
                    }
                    else if (oPatient["Aufnahmedatum"] != DBNull.Value && DateTime.Today < (DateTime)oPatient["Aufnahmedatum"])
                    {
                        pb.Image = ImageBettPrivatAD();
                    }
                    else
                    {
                        pb.Image = ImageBettPrivat();
                    }
                }
                else
                {
                    if (oPatient["Entlassungsdatum"] != DBNull.Value && DateTime.Today > (DateTime)oPatient["Entlassungsdatum"])
                    {
                        pb.Image = ImageBettKasseED();
                    }
                    else if (oPatient["Aufnahmedatum"] != DBNull.Value && DateTime.Today < (DateTime)oPatient["Aufnahmedatum"])
                    {
                        pb.Image = ImageBettKasseAD();
                    }
                    else
                    {
                        pb.Image = ImageBettKasse();
                    }
                }

                SetTooltip(pb, oPatient);
            }
        }
        protected void SetTooltip(PictureBox pb, DataRow oPatient)
        {
            if (oPatient != null && oPatient["ID_Patienten"] != DBNull.Value)
            {
                StringBuilder sb = new StringBuilder((string)oPatient["Nachname"]);
                sb.Append(", " + (string)oPatient["Vorname"]);
                sb.Append("\nDiagnose:" + this.BusinessLayer.GetPatientDiagnose(oPatient));
                sb.Append("\nAufnahmedatum: " + Utility.Tools.DBNullableDateTime2DateString(oPatient["Aufnahmedatum"]));
                sb.Append("\nEntlassungsdatum: " + Utility.Tools.DBNullableDateTime2DateString(oPatient["Entlassungsdatum"]));
                if ((int)oPatient["Isolation"] > 0)
                {
                    sb.Append("\nIsolation: ");
                    sb.Append((string)oPatient["IsolationText"]);
                }
                if ((int)oPatient["Hervorheben"] > 0)
                {
                    sb.Append("\nBesonderes Merkmal: ");
                    sb.Append((string)oPatient["HervorhebenGrund"]);
                }

                if (toolTip1 != null)
                {
                    this.toolTip1.SetToolTip(pb, sb.ToString());
                }
            }
            else
            {
                if (toolTip1 != null)
                {
                    this.toolTip1.SetToolTip(pb, "");
                }
            }
        }

        private PictureBox FindPictureBoxByID(Panel pnl, string id)
        {
            PictureBox pb = null;

            foreach (Control c in pnl.Controls)
            {
                if (c.Name.IndexOf(id) != -1)
                {
                    pb = (PictureBox)c;
                    break;
                }
            }
            if (pb == null)
            {
                throw new Exception("FindPictureBoxByID(): no picturebox found for id = " + id);
            }

            return pb;
        }

        private DataRow Panel2Zimmer(Panel panel)
        {
            int nID_Zimmer = (int)panel.Tag;

            return this.BusinessLayer.GetZimmer(nID_Zimmer);
        }

        public void SetTextIsolation(TextBox txtIsolation, DataRow oBett)
        {
            if (oBett != null && oBett["ID_Patienten"] != DBNull.Value)
            {
                string s = "";

                DataRow oPatient = this.BusinessLayer.GetPatient((int)oBett["ID_Patienten"]);

                txtIsolation.Visible = true;

                if ((int)oPatient["Isolation"] > 0)
                {
                    s = (string)oPatient["IsolationText"];
                }
                if ((int)oPatient["Hervorheben"] > 0)
                {
                    if (s.Length > 0)
                    {
                        s += "\r\n";
                    }
                    s += (string)oPatient["HervorhebenGrund"];
                }

                txtIsolation.Text = s;
            }
        }
        public void DisplayTextDynamic(Panel pnlText)
        {
            int nID_Texte = (int)pnlText.Tag;

            DataRow row = BusinessLayer.GetTextfeld(nID_Texte);
        }

        public void DisplayZimmerDynamic(Panel pnlZimmer, TextBox txtZimmer, TextBox txtIsolation)
        {
            DataRow rowZimmer = Panel2Zimmer(pnlZimmer);
            int nStation = (int)rowZimmer["Station"];
            int nZimmerNummer = (int)rowZimmer["ZimmerNummer"];
            int nBelegt = (int)rowZimmer["Belegt"];
            int nAnzahlBetten = (int)rowZimmer["AnzahlBetten"];
            int nCountBetten = 0;
            txtZimmer.Clear();

            StringBuilder sb = new StringBuilder();

            // Alle PictureBox durchlaufen und Visible=false setzen.
            txtIsolation.Visible = false;
            foreach (Control c in pnlZimmer.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = (PictureBox)c;
                    pb.Visible = false;
                }
            }

            // Alle Betten des Zimmers durchlaufen
            foreach (Control c in pnlZimmer.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    nCountBetten++;
                    PictureBox pb = (PictureBox)c;

                    int nID_Betten = (int)pb.Tag;

                    // Bett mit Bild füllen
                    if (nCountBetten <= nAnzahlBetten)
                    {
                        DataRow rowBettInfo = BusinessLayer.GetBettPatient(nID_Betten);

                        DisplayPictureBox(pb, rowBettInfo);

                        // Text fuer Zimmerinfo TextBox hinzufuegen
                        if (sb.Length > 0)
                        {
                            sb.Append(Environment.NewLine);
                        }
                        if (rowBettInfo["ID_Patienten"] != DBNull.Value)
                        {
                            DataRow oPatient = this.BusinessLayer.GetPatient((int)rowBettInfo["ID_Patienten"]);
                            sb.Append((string)oPatient["Nachname"] + "-" + BusinessLayer.GetPatientDiagnose(oPatient));
                        }
                        if (nAnzahlBetten == 1)
                        {
                            SetTextIsolation(txtIsolation, rowBettInfo);
                            break;
                        }
                    }
                }
            }

            txtZimmer.Text = sb.ToString();

            switch ((int)rowZimmer["Geschlecht"])
            {
                case 1:
                    //Maennerzimmer
                    pnlZimmer.BackColor = Color.LightBlue;
                    txtZimmer.BackColor = Color.LightBlue;
                    txtIsolation.BackColor = Color.LightBlue;
                    break;

                case 0:
                    // Frauenzimmer
                    pnlZimmer.BackColor = Color.LightPink;
                    txtZimmer.BackColor = Color.LightPink;
                    txtIsolation.BackColor = Color.LightPink;
                    break;

                default:
                    //nicht belegt
                    pnlZimmer.BackColor = this.BackColor;
                    txtZimmer.BackColor = this.BackColor;
                    txtIsolation.BackColor = this.BackColor;
                    break;
            }
        }

        public Image ImageBettLeer()
        {
            return pbLeer.Image;
        }
        public Image ImageBettPrivat()
        {
            return pbPrivat.Image;
        }
        public Image ImageBettPrivatAD()
        {
            return pbPrivatAD.Image;
        }
        public Image ImageBettPrivatED()
        {
            return pbPrivatED.Image;
        }
        public Image ImageBettKasse()
        {
            return pbKasse.Image;
        }
        public Image ImageBettKasseAD()
        {
            return pbKasseAD.Image;
        }
        public Image ImageBettKasseED()
        {
            return pbKasseED.Image;
        }

        private void DynamicStationView_Load(object sender, EventArgs e)
        {
            _About = new AboutBox(BusinessLayer, true);
            _About.Show();

            CreateDiagram();

            _About.Close();
            _About = null;
        }

        private void aktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateForm();
        }

        private void druckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetup();
        }

        private void allesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintStation(true, true, true, true);

        }

        private void grafikToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintStation(true, true, false, false);

        }

        private void patientenlisteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintStation(true, false, true, false);

        }

        private void infektionenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintStation(true, false, false, true);

        }

        private void allesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStation(false, true, true, true);

        }

        private void grafikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStation(false, true, false, false);

        }

        private void patientenlisteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStation(false, false, true, false);

        }

        private void infektionenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStation(false, false, false, true);

        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void neuerPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aufnahme();

        }

        private void patientBearbeitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientBearbeiten();

        }

        private void patientEntlassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientEntlassen();

        }

        private void infektionenKatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfektionenView dlg = new InfektionenView(BusinessLayer);
            dlg.ShowDialog();

        }

        private void diagnosenKatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagnosenView dlg = new DiagnosenView(BusinessLayer);
            dlg.ShowDialog();

        }

        private void patientenImportierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StationImportView dlg = new StationImportView(BusinessLayer);
            dlg.ShowDialog();

        }

        private void historieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogView dlg = new LogView(BusinessLayer);
            dlg.ShowDialog();

        }

        private void legendeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegendeView dlg = new LegendeView(BusinessLayer);
            dlg.ShowDialog();

        }

        private void ueberStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox dlg = new AboutBox(BusinessLayer, false);
            dlg.ShowDialog();

        }
        private void Aufnahme()
        {
            PatientView dlg = new PatientView(BusinessLayer, ID_Diagramme, null);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.PopulateForm();
            }
        }
        private void PrintStation(bool bPreview, bool bPrintGrafik, bool bPrintPatienten, bool bPrintInfo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (bPreview)
                {
                    this._oPrinting.PrintPreview(800, 600, bPrintGrafik, bPrintPatienten, bPrintInfo);
                }
                else
                {
                    this._oPrinting.Print(bPrintGrafik, bPrintPatienten, bPrintInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void PatientEntlassen()
        {
            PatientenAuswahlView dlg = new PatientenAuswahlView(BusinessLayer);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                int nID_Patienten = dlg.ID_Patienten;
                if (nID_Patienten != -1)
                {
                    string s = "Hiermit werden der Patient sowie alle seine Daten gelöscht."
                        + "\nFortfahren?";

                    if (Confirm(s))
                    {
                        Zimmer pnlZimmer = FindPanelForPatient(nID_Patienten);

                        BusinessLayer.PatientEntlassen(nID_Patienten);

                        if (pnlZimmer != null)
                        {
                            DisplayZimmerDynamic(pnlZimmer);
                        }
                        PopulateInfos();
                        PopulateInfektionen();
                    }
                }
            }
        }

        private void PatientBearbeiten()
        {
            DataRow oPatient = null;
            PatientenAuswahlView dlg = new PatientenAuswahlView(BusinessLayer);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                int nID_Patienten = dlg.ID_Patienten;
                if (nID_Patienten != -1)
                {
                    oPatient = BusinessLayer.GetPatient(nID_Patienten);
                    PatientView dlg2 = new PatientView(BusinessLayer, ID_Diagramme, oPatient);
                    if (DialogResult.OK == dlg2.ShowDialog())
                    {
                        Zimmer pnlZimmer = FindPanelForPatient(nID_Patienten);
                        DisplayZimmerDynamic(pnlZimmer);
                        PopulateInfos();
                        PopulateInfektionen();
                    }
                }
            }
        }

        private void PageSetup()
        {
            _oPrinting.PageSetup();
        }

        private void DynamicDiagramView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_oMainView != null)
            {
                _oMainView.RemoveDiagram(this);
            }
        }
    }
}