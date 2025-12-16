using StationDesigner.UserControls;
using Windows.Forms;

namespace StationDesigner
{
    partial class DesignerView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerView));
            this.grpElemente = new System.Windows.Forms.GroupBox();
            this.cmdDiagrammDelete = new System.Windows.Forms.Button();
            this.cmdDiagrammNew = new System.Windows.Forms.Button();
            this.cmdDiagrammEdit = new System.Windows.Forms.Button();
            this.lvDiagramme = new OplListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.neuesTextfeldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuesZimmerAnlegenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grpElemente.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpElemente
            // 
            this.grpElemente.Controls.Add(this.cmdDiagrammDelete);
            this.grpElemente.Controls.Add(this.cmdDiagrammNew);
            this.grpElemente.Controls.Add(this.cmdDiagrammEdit);
            this.grpElemente.Controls.Add(this.lvDiagramme);
            this.grpElemente.Location = new System.Drawing.Point(3, 27);
            this.grpElemente.Name = "grpElemente";
            this.grpElemente.Size = new System.Drawing.Size(850, 122);
            this.grpElemente.TabIndex = 0;
            this.grpElemente.TabStop = false;
            this.grpElemente.Text = "Diagramme";
            // 
            // cmdDiagrammDelete
            // 
            this.cmdDiagrammDelete.Location = new System.Drawing.Point(727, 92);
            this.cmdDiagrammDelete.Name = "cmdDiagrammDelete";
            this.cmdDiagrammDelete.Size = new System.Drawing.Size(117, 24);
            this.cmdDiagrammDelete.TabIndex = 6;
            this.cmdDiagrammDelete.Text = "Löschen...";
            this.cmdDiagrammDelete.UseVisualStyleBackColor = true;
            this.cmdDiagrammDelete.Click += new System.EventHandler(this.cmdDiagrammDelete_Click);
            // 
            // cmdDiagrammNew
            // 
            this.cmdDiagrammNew.Location = new System.Drawing.Point(727, 19);
            this.cmdDiagrammNew.Name = "cmdDiagrammNew";
            this.cmdDiagrammNew.Size = new System.Drawing.Size(117, 24);
            this.cmdDiagrammNew.TabIndex = 5;
            this.cmdDiagrammNew.Text = "Neu";
            this.cmdDiagrammNew.UseVisualStyleBackColor = true;
            this.cmdDiagrammNew.Click += new System.EventHandler(this.cmdDiagrammNew_Click);
            // 
            // cmdDiagrammEdit
            // 
            this.cmdDiagrammEdit.Enabled = false;
            this.cmdDiagrammEdit.Location = new System.Drawing.Point(727, 49);
            this.cmdDiagrammEdit.Name = "cmdDiagrammEdit";
            this.cmdDiagrammEdit.Size = new System.Drawing.Size(117, 24);
            this.cmdDiagrammEdit.TabIndex = 4;
            this.cmdDiagrammEdit.Text = "Eigenschaften...";
            this.cmdDiagrammEdit.UseVisualStyleBackColor = true;
            this.cmdDiagrammEdit.Click += new System.EventHandler(this.cmdDiagrammEdit_Click);
            // 
            // lvDiagramme
            // 
            this.lvDiagramme.FullRowSelect = true;
            this.lvDiagramme.Location = new System.Drawing.Point(9, 19);
            this.lvDiagramme.Name = "lvDiagramme";
            this.lvDiagramme.Size = new System.Drawing.Size(699, 97);
            this.lvDiagramme.TabIndex = 2;
            this.lvDiagramme.UseCompatibleStateImageBehavior = false;
            this.lvDiagramme.View = System.Windows.Forms.View.Details;
            this.lvDiagramme.SelectedIndexChanged += new System.EventHandler(this.lvDiagramme_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.extrasToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(865, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speichernToolStripMenuItem,
            this.toolStripMenuItem2,
            this.neuesTextfeldToolStripMenuItem,
            this.neuesZimmerAnlegenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.dateiToolStripMenuItem.Text = "&Datei";
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.speichernToolStripMenuItem.Text = "&Speichern";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(190, 6);
            // 
            // neuesTextfeldToolStripMenuItem
            // 
            this.neuesTextfeldToolStripMenuItem.Name = "neuesTextfeldToolStripMenuItem";
            this.neuesTextfeldToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.neuesTextfeldToolStripMenuItem.Text = "Neues &Textfeld";
            this.neuesTextfeldToolStripMenuItem.Click += new System.EventHandler(this.neuesTextfeldToolStripMenuItem_Click);
            // 
            // neuesZimmerAnlegenToolStripMenuItem
            // 
            this.neuesZimmerAnlegenToolStripMenuItem.Name = "neuesZimmerAnlegenToolStripMenuItem";
            this.neuesZimmerAnlegenToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.neuesZimmerAnlegenToolStripMenuItem.Text = "Neues &Zimmer anlegen";
            this.neuesZimmerAnlegenToolStripMenuItem.Click += new System.EventHandler(this.neuesZimmerAnlegenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.beendenToolStripMenuItem.Text = "&Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionenToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.extrasToolStripMenuItem.Text = "&Extras";
            // 
            // optionenToolStripMenuItem
            // 
            this.optionenToolStripMenuItem.Name = "optionenToolStripMenuItem";
            this.optionenToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.optionenToolStripMenuItem.Text = "&Optionen...";
            this.optionenToolStripMenuItem.Click += new System.EventHandler(this.optionenToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem1});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "&Hilfe";
            // 
            // infoToolStripMenuItem1
            // 
            this.infoToolStripMenuItem1.Name = "infoToolStripMenuItem1";
            this.infoToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.infoToolStripMenuItem1.Text = "&Info...";
            this.infoToolStripMenuItem1.Click += new System.EventHandler(this.infoToolStripMenuItem1_Click);
            // 
            // DesignerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 800);
            this.Controls.Add(this.grpElemente);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DesignerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stationsplaner - Designer";
            this.Load += new System.EventHandler(this.DesignerView_Load);
            this.grpElemente.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpElemente;
        private OplListView lvDiagramme;
        private System.Windows.Forms.Button cmdDiagrammEdit;
        private System.Windows.Forms.Button cmdDiagrammDelete;
        private System.Windows.Forms.Button cmdDiagrammNew;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuesZimmerAnlegenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem neuesTextfeldToolStripMenuItem;
    }
}

