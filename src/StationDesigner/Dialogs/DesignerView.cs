using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using StationDesigner.UserControls;
using StationDesigner.Dialogs;
using Station;

namespace StationDesigner
{
    public partial class DesignerView : StationDesignerForm
    {
        private UserControls.Diagramm pnlDiagram;
        DataRow oRowLayoutSettings;
        Diagramm _diagramm;
        PickBox _pickBox = new PickBox();

        public DesignerView(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            this.pnlDiagram = new UserControls.Diagramm();
            // 
            // pnlDiagram
            // 
            this.pnlDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDiagram.GridSize = 8;
            this.pnlDiagram.Location = new System.Drawing.Point(3, 150);
            this.pnlDiagram.Name = "pnlDiagram";
            this.pnlDiagram.ShowGrid = true;
            this.pnlDiagram.Size = new System.Drawing.Size(852, 620);
            this.pnlDiagram.TabIndex = 1;
            this.pnlDiagram.Text = "Station";
            this.Controls.Add(this.pnlDiagram);

            oRowLayoutSettings = BusinessLayer.DatabaseLayer.DesignerGetDesignerSettings();

            ApplySettings();
        }

        private void ApplySettings()
        {
            _pickBox.SnapToGrid = (1 == (int)oRowLayoutSettings["SnapToGrid"]) ? true : false;
            _pickBox.GridSize = (int)oRowLayoutSettings["GridSize"];

            pnlDiagram.ShowGrid = (1 == (int)oRowLayoutSettings["ShowGrid"]) ? true : false;
            pnlDiagram.GridSize = (int)oRowLayoutSettings["GridSize"];
        }

        public Diagramm Diagramm
        {
            get { return _diagramm; }
        }

        public List<Textfeld> Texte
        {
            get { return _diagramm.Texte; }
        }
        public List<Zimmer> Zimmer
        {
            get { return _diagramm.Zimmer; }
        }
        public List<Label> ZimmerNummern
        {
            get { return _diagramm.ZimmerNummern; }
        }

        internal void OnTextfeldDelete(Textfeld text)
        {
            _pickBox.UnWireControl(text);
            Texte.Remove(text);

            pnlDiagram.Controls.Remove(text);
        }
        internal void OnZimmerDelete(Zimmer zimmer)
        {
            Label lblZimmerNummer = zimmer.ControlZimmerNummer;

            _pickBox.UnWireControl(zimmer);
            Zimmer.Remove(zimmer);

            // Das Label muss weg.
            _pickBox.UnWireControl(lblZimmerNummer);
            lblZimmerNummer.Visible = false;
            ZimmerNummern.Remove(lblZimmerNummer);

            pnlDiagram.Controls.Remove(zimmer);
        }

        internal void OnInfoBoxNew(GroupBox ctl)
        {
            _diagramm.InfoBox = ctl;
            pnlDiagram.Controls.Add(ctl);
            _pickBox.WireControl(ctl, false);
        }

        internal void OnTextfeldNew(Textfeld text)
        {
            Texte.Add(text);
            pnlDiagram.Controls.Add(text);
            _pickBox.WireControl(text);

            text.BringToFront();
        }
        internal void OnZimmerNew(Zimmer zimmer)
        {
            Zimmer.Add(zimmer);
            pnlDiagram.Controls.Add(zimmer);
            _pickBox.WireControl(zimmer);

            zimmer.BringToFront();
        }
        internal void OnBettDelete(Bett bett)
        {
            _pickBox.UnWireControl(bett);
        }

        internal void OnBettNew(Bett bett)
        {
            _pickBox.WireControl(bett, false);
        }
        internal void OnZimmerInfoNew(TextBox ctl)
        {
            _pickBox.WireControl(ctl);
        }
        internal void OnZimmerIsolationNew(TextBox ctl)
        {
            _pickBox.WireControl(ctl);
        }
        public void OnZimmerNummerNew(Label ctl)
        {
            ZimmerNummern.Add(ctl);
            pnlDiagram.Controls.Add(ctl);
            _pickBox.WireControl(ctl, false);

            ctl.BringToFront();
        }

        public void ZimmerKopie(Zimmer zimmer)
        {
            Zimmer zimmer2 = CreateZimmer();

            zimmer2.AddBetten(zimmer.AnzahlBetten);
            zimmer2.Location = new Point(zimmer.Location.X + zimmer.Width + 10, zimmer.Location.Y);
        }

        /// <summary>
        /// Erzeugt in der Datenbank ein neues Zimmer.
        /// </summary>
        /// <returns></returns>
        private Zimmer CreateZimmer()
        {
            Zimmer zimmer = null;

            if (_diagramm != null)
            {
                DataRow rowZimmer = BusinessLayer.DatabaseLayer.CreateDataRowZimmer();
                rowZimmer["ID_Diagramme"] = _diagramm.ID_Diagramme;
                int nID_Zimmer = BusinessLayer.DatabaseLayer.DesignerInsertZimmer(rowZimmer);
                rowZimmer = BusinessLayer.DatabaseLayer.DesignerGetZimmer(nID_Zimmer);

                zimmer = CreateZimmer(rowZimmer);
            }
            else
            {
                MessageBox("Es ist kein Diagramm ausgewählt.");
            }

            return zimmer;
        }

        private Textfeld CreateTextfeld()
        {
            Textfeld text = null;

            if (_diagramm != null)
            {
                DataRow row = BusinessLayer.DatabaseLayer.CreateDataRowTextfeld();
                row["ID_Diagramme"] = _diagramm.ID_Diagramme;
                int nID_Textfeld = BusinessLayer.DatabaseLayer.DesignerInsertTextfeld(row);
                row = BusinessLayer.DatabaseLayer.DesignerGetTextfeld(nID_Textfeld);

                text = CreateTextfeld(row);
            }
            else
            {
                MessageBox("Es ist kein Diagramm ausgewählt.");
            }

            return text;
        }

        /// <summary>
        /// Legt fuer ein Zimmer in der Datenbank das GUI Objekt an.
        /// </summary>
        /// <param name="rowZimmer"></param>
        /// <returns></returns>
        private Zimmer CreateZimmer(DataRow rowZimmer)
        {
            Zimmer pnl = new Zimmer(this, (int)rowZimmer["ID_Zimmer"], (int)rowZimmer["Station"], (int)rowZimmer["ZimmerNummer"]);

            pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnl.Location = new System.Drawing.Point((int)rowZimmer["LocationX"], (int)rowZimmer["LocationY"]);
            pnl.Size = new System.Drawing.Size((int)rowZimmer["Width"], (int)rowZimmer["Height"]);
            pnl.TabStop = false;
            pnl.TabIndex = 0;
            pnl.CreateContextMenu();

            OnZimmerNew(pnl);

            pnl.CreateComponents(
                (int)rowZimmer["InfoLocationX"], (int)rowZimmer["InfoLocationY"],
                (int)rowZimmer["IsolationLocationX"], (int)rowZimmer["IsolationLocationY"],
                (int)rowZimmer["NummerLocationX"], (int)rowZimmer["NummerLocationY"]
                );

            return pnl;
        }

        private Textfeld CreateTextfeld(DataRow row)
        {
            Textfeld pnl = new Textfeld(this, (int)row["ID_Texte"]);

            pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnl.Location = new System.Drawing.Point((int)row["LocationX"], (int)row["LocationY"]);
            pnl.Size = new System.Drawing.Size((int)row["Width"], (int)row["Height"]);
            pnl.TabStop = false;
            pnl.TabIndex = 0;
            pnl.CreateContextMenu();
            pnl.TheText = (string)row["Text"];

            OnTextfeldNew(pnl);

            return pnl;
        }

        private void cmdZimmer_Click(object sender, EventArgs e)
        {
            CreateZimmer();
        }

        public void WriteDiagramXML()
        {
            if (_diagramm != null)
            {
                DiagrammVisitorXMLWriter visitor = new DiagrammVisitorXMLWriter();
                _diagramm.Accept(visitor);
            }
        }

        private void cmdWriteXML_Click(object sender, EventArgs e)
        {
            WriteDiagramXML();
        }

        private void PopulateListDiagramme()
        {
            EnableControls(false);

            DataView dv = BusinessLayer.DatabaseLayer.DesignerGetDiagramme();

            this.DefaultListViewProperties(lvDiagramme);

            lvDiagramme.Clear();

            lvDiagramme.Columns.Add("Name", 120);
            lvDiagramme.Columns.Add("Beschreibung", -2, HorizontalAlignment.Left);

            lvDiagramme.Sorting = SortOrder.None;

            lvDiagramme.BeginUpdate();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Name"]);
                lvi.Tag = (int)dataRow["ID_Diagramme"];
                lvi.SubItems.Add((string)dataRow["Beschreibung"]);

                lvDiagramme.Items.Add(lvi);
            }
            lvDiagramme.EndUpdate();

            _diagramm = null;
        }


        private void EnableControls(bool bEnable)
        {
            cmdDiagrammEdit.Enabled = bEnable;
            cmdDiagrammDelete.Enabled = bEnable;
        }
        private void DesignerView_Load(object sender, EventArgs e)
        {
            PopulateListDiagramme();
        }

        private void UnloadDiagramm()
        {
            if (_diagramm != null)
            {
                _diagramm.InfoBox.Visible = false;
                _pickBox.UnWireControl(_diagramm.InfoBox);
                pnlDiagram.Controls.Remove(_diagramm.InfoBox);

                foreach (Textfeld text in Texte)
                {
                    text.Visible = false;

                    _pickBox.UnWireControl(text);
                    pnlDiagram.Controls.Remove(text);
                }
                Texte.Clear();

                foreach (Zimmer zimmer in Zimmer)
                {
                    Label lblZimmerNummer = zimmer.ControlZimmerNummer;

                    zimmer.Visible = false;
                    lblZimmerNummer.Visible = false;

                    _pickBox.UnWireControl(lblZimmerNummer);
                    _pickBox.UnWireControl(zimmer);
                    pnlDiagram.Controls.Remove(lblZimmerNummer);
                    pnlDiagram.Controls.Remove(zimmer);
                    zimmer.Unload();
                }
                Zimmer.Clear();
                _pickBox.Remove();
            }
        }

        private void LoadDiagramm(int nID_Diagramme)
        {
            if (_diagramm != null)
            {
                UnloadDiagramm();
            }

            DataRow rowDiagramm = BusinessLayer.DatabaseLayer.DesignerGetDiagramm(nID_Diagramme);

            _diagramm = new Diagramm(
                        nID_Diagramme, 
                        (string)rowDiagramm["Name"], 
                        (string)rowDiagramm["Beschreibung"]);

            // Die Infobox ganz rechts
            GroupBox grpInformation = new GroupBox();
            grpInformation.Location = new System.Drawing.Point((int)rowDiagramm["InfoLocationX"], (int)rowDiagramm["InfoLocationY"]);
            grpInformation.Size = new System.Drawing.Size(262, 512);
            grpInformation.TabIndex = 0;
            grpInformation.TabStop = false;
            grpInformation.Text = "Information";
            OnInfoBoxNew(grpInformation);

            // Alle Zimmer
            DataView dvZimmer = BusinessLayer.DatabaseLayer.DesignerGetZimmerForDiagramm(nID_Diagramme);
            foreach (DataRow drZimmer in dvZimmer.Table.Rows)
            {
                Zimmer zimmer = CreateZimmer(drZimmer);

                DataView dvBetten = BusinessLayer.DatabaseLayer.DesignerGetBetten((int)drZimmer["ID_Zimmer"]);
                foreach (DataRow drBett in dvBetten.Table.Rows)
                {
                    zimmer.CreateBett(drBett);
                }
            }

            // Alle Texte
            DataView dv = BusinessLayer.DatabaseLayer.DesignerGetTextfelderForDiagramm(nID_Diagramme);
            foreach (DataRow row in dv.Table.Rows)
            {
                CreateTextfeld(row);
            }
        }

        private void lvDiagramme_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nID_Diagramme = this.GetFirstSelectedTag(lvDiagramme);

            if (nID_Diagramme != -1)
            {
                LoadDiagramm(nID_Diagramme);

                EnableControls(true);
            }
        }

        public void WriteDiagramDatabase()
        {
            if (_diagramm != null)
            {
                DiagrammVisitorDatabaseWriter visitor = new DiagrammVisitorDatabaseWriter(BusinessLayer.DatabaseLayer);
                _diagramm.Accept(visitor);
            }
            else
            {
                MessageBox("Es ist kein Diagramm ausgewählt.");
            }
        }

        private void cmdWriteDatabase_Click(object sender, EventArgs e)
        {
            WriteDiagramDatabase();
        }

        private void cmdDiagrammEdit_Click(object sender, EventArgs e)
        {
            if (_diagramm != null)
            {
                Dialogs.DiagrammView dlg = new Dialogs.DiagrammView(
                        BusinessLayer,
                        _diagramm.DiagrammName, 
                        _diagramm.DiagrammBeschreibung);

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    DataRow row = BusinessLayer.DatabaseLayer.DesignerGetDiagramm(_diagramm.ID_Diagramme);
                    row["Name"] = dlg.DiagrammName;
                    row["Beschreibung"] = dlg.DiagrammBeschreibung;
                    BusinessLayer.DatabaseLayer.DesignerUpdateDiagramm(row);
                    UnloadDiagramm();
                    PopulateListDiagramme();
                    Invalidate();
                }
            }
        }

        private void cmdDiagrammNew_Click(object sender, EventArgs e)
        {
            DataRow row = BusinessLayer.DatabaseLayer.CreateDataRowDiagramm();
            int nID_Diagramme = BusinessLayer.DatabaseLayer.DesignerInsertDiagramm(row);
            if (_diagramm != null)
            {
                UnloadDiagramm();
            }
            PopulateListDiagramme();
        }

        private void DeleteDiagramm()
        {
            if (_diagramm != null)
            {
                BusinessLayer.DatabaseLayer.DesignerDeleteDiagrammComplete(_diagramm.ID_Diagramme);
            }
        }

        private void cmdDiagrammDelete_Click(object sender, EventArgs e)
        {
            int nID_Diagramme = this.GetFirstSelectedTag(lvDiagramme);
            if (nID_Diagramme != -1)
            {
                if (Confirm("Sind Sie sicher, dass Sie dieses Diagramm löschen wollen?"
                   + Environment.NewLine + Environment.NewLine + "Alle Zimmer und Betten werden hiermit gelöscht."))
                {
                    UnloadDiagramm();
                    DeleteDiagramm();
                    PopulateListDiagramme();
                }
            }
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox dlg = new AboutBox(BusinessLayer, false);
            dlg.ShowDialog();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void optionenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesignerPropertiesView dlg = new DesignerPropertiesView(BusinessLayer, oRowLayoutSettings);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BusinessLayer.DatabaseLayer.UpdateDesignerSettings(oRowLayoutSettings);
                ApplySettings();
                pnlDiagram.Invalidate();
            }
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteDiagramDatabase();
        }

        private void neuesZimmerAnlegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateZimmer();
        }

        private void neuesTextfeldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTextfeld();
        }
    }
}
