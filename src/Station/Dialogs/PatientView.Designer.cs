using Windows.Forms;

namespace Station
{
    partial class PatientView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientView));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHervorhebenGrund = new System.Windows.Forms.TextBox();
            this.cmdKatalog = new System.Windows.Forms.Button();
            this.cbDiagnosen = new System.Windows.Forms.ComboBox();
            this.lvInfektionen = new OplListView();
            this.chkIsolation = new System.Windows.Forms.CheckBox();
            this.cmdInfektionNew = new System.Windows.Forms.Button();
            this.cbIsolation = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkHervorheben = new System.Windows.Forms.CheckBox();
            this.cmdInjektionDelete = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdAufnahmedatum = new System.Windows.Forms.Button();
            this.cmdEntlassungsdatum = new System.Windows.Forms.Button();
            this.txtAufnahmedatum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEntlassungsdatum = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdZimmer = new System.Windows.Forms.Button();
            this.txtBett = new System.Windows.Forms.TextBox();
            this.cmdGeburtsdatum = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAnzahlBetten = new System.Windows.Forms.ComboBox();
            this.radFrau = new System.Windows.Forms.RadioButton();
            this.radMann = new System.Windows.Forms.RadioButton();
            this.chkPrivat = new System.Windows.Forms.CheckBox();
            this.txtGeburtsdatum = new System.Windows.Forms.TextBox();
            this.txtNachname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVorname = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtHervorhebenGrund);
            this.groupBox3.Controls.Add(this.cmdKatalog);
            this.groupBox3.Controls.Add(this.cbDiagnosen);
            this.groupBox3.Controls.Add(this.lvInfektionen);
            this.groupBox3.Controls.Add(this.chkIsolation);
            this.groupBox3.Controls.Add(this.cmdInfektionNew);
            this.groupBox3.Controls.Add(this.cbIsolation);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.chkHervorheben);
            this.groupBox3.Controls.Add(this.cmdInjektionDelete);
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(649, 198);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Prozeduren/ICD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(478, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 14);
            this.label8.TabIndex = 23;
            this.label8.Text = "Grund:";
            // 
            // txtHervorhebenGrund
            // 
            this.txtHervorhebenGrund.Location = new System.Drawing.Point(481, 101);
            this.txtHervorhebenGrund.Name = "txtHervorhebenGrund";
            this.txtHervorhebenGrund.Size = new System.Drawing.Size(153, 20);
            this.txtHervorhebenGrund.TabIndex = 22;
            // 
            // cmdKatalog
            // 
            this.cmdKatalog.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKatalog.Location = new System.Drawing.Point(219, 65);
            this.cmdKatalog.Name = "cmdKatalog";
            this.cmdKatalog.Size = new System.Drawing.Size(75, 41);
            this.cmdKatalog.TabIndex = 21;
            this.cmdKatalog.Text = "Katalog verwalten...";
            this.toolTip1.SetToolTip(this.cmdKatalog, "Hier klicken, um neue Einträge hinzuzufügen");
            this.cmdKatalog.UseVisualStyleBackColor = true;
            this.cmdKatalog.Click += new System.EventHandler(this.cmdKatalog_Click);
            // 
            // cbDiagnosen
            // 
            this.cbDiagnosen.FormattingEnabled = true;
            this.cbDiagnosen.Location = new System.Drawing.Point(6, 19);
            this.cbDiagnosen.MaxDropDownItems = 10;
            this.cbDiagnosen.MaxLength = 255;
            this.cbDiagnosen.Name = "cbDiagnosen";
            this.cbDiagnosen.Size = new System.Drawing.Size(637, 22);
            this.cbDiagnosen.TabIndex = 7;
            this.cbDiagnosen.SelectedIndexChanged += new System.EventHandler(this.cbDiagnosen_SelectedIndexChanged);
            this.cbDiagnosen.TextChanged += new System.EventHandler(this.cbDiagnosen_TextChanged);
            // 
            // lvInfektionen
            // 
            this.lvInfektionen.Location = new System.Drawing.Point(6, 65);
            this.lvInfektionen.Name = "lvInfektionen";
            this.lvInfektionen.Size = new System.Drawing.Size(207, 124);
            this.lvInfektionen.TabIndex = 17;
            this.lvInfektionen.UseCompatibleStateImageBehavior = false;
            // 
            // chkIsolation
            // 
            this.chkIsolation.AutoSize = true;
            this.chkIsolation.Location = new System.Drawing.Point(314, 60);
            this.chkIsolation.Name = "chkIsolation";
            this.chkIsolation.Size = new System.Drawing.Size(65, 18);
            this.chkIsolation.TabIndex = 0;
            this.chkIsolation.Text = "Isolation";
            this.chkIsolation.UseVisualStyleBackColor = true;
            this.chkIsolation.CheckedChanged += new System.EventHandler(this.chkIsolation_CheckedChanged);
            // 
            // cmdInfektionNew
            // 
            this.cmdInfektionNew.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInfektionNew.Location = new System.Drawing.Point(219, 127);
            this.cmdInfektionNew.Name = "cmdInfektionNew";
            this.cmdInfektionNew.Size = new System.Drawing.Size(75, 28);
            this.cmdInfektionNew.TabIndex = 19;
            this.cmdInfektionNew.Text = "Neu...";
            this.cmdInfektionNew.UseVisualStyleBackColor = true;
            this.cmdInfektionNew.Click += new System.EventHandler(this.cmdInfektionNew_Click);
            // 
            // cbIsolation
            // 
            this.cbIsolation.FormattingEnabled = true;
            this.cbIsolation.Location = new System.Drawing.Point(311, 99);
            this.cbIsolation.Name = "cbIsolation";
            this.cbIsolation.Size = new System.Drawing.Size(161, 22);
            this.cbIsolation.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "Nosokomiale Infektionen/Sonstiges:";
            // 
            // chkHervorheben
            // 
            this.chkHervorheben.AutoSize = true;
            this.chkHervorheben.Location = new System.Drawing.Point(483, 60);
            this.chkHervorheben.Name = "chkHervorheben";
            this.chkHervorheben.Size = new System.Drawing.Size(123, 18);
            this.chkHervorheben.TabIndex = 1;
            this.chkHervorheben.Text = "Patient hervorheben";
            this.chkHervorheben.UseVisualStyleBackColor = true;
            this.chkHervorheben.CheckedChanged += new System.EventHandler(this.chkHervorheben_CheckedChanged);
            // 
            // cmdInjektionDelete
            // 
            this.cmdInjektionDelete.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInjektionDelete.Location = new System.Drawing.Point(219, 161);
            this.cmdInjektionDelete.Name = "cmdInjektionDelete";
            this.cmdInjektionDelete.Size = new System.Drawing.Size(75, 28);
            this.cmdInjektionDelete.TabIndex = 20;
            this.cmdInjektionDelete.Text = "Löschen";
            this.cmdInjektionDelete.UseVisualStyleBackColor = true;
            this.cmdInjektionDelete.Click += new System.EventHandler(this.cmdInfektionDelete_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.cmdAufnahmedatum);
            this.groupBox4.Controls.Add(this.cmdEntlassungsdatum);
            this.groupBox4.Controls.Add(this.txtAufnahmedatum);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtEntlassungsdatum);
            this.groupBox4.Location = new System.Drawing.Point(355, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 125);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Aufnahme und Entlassungsdatum";
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label9.Location = new System.Drawing.Point(7, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(284, 48);
            this.label9.TabIndex = 15;
            this.label9.Text = "Die Mittlere GVD wird automatisch auf das Aufnahmedatum addiert, wenn eine Prozed" +
                "ur-ICD ausgewählt wird.";
            // 
            // cmdAufnahmedatum
            // 
            this.cmdAufnahmedatum.Location = new System.Drawing.Point(101, 93);
            this.cmdAufnahmedatum.Name = "cmdAufnahmedatum";
            this.cmdAufnahmedatum.Size = new System.Drawing.Size(28, 20);
            this.cmdAufnahmedatum.TabIndex = 13;
            this.cmdAufnahmedatum.Text = "...";
            this.cmdAufnahmedatum.UseVisualStyleBackColor = true;
            this.cmdAufnahmedatum.Click += new System.EventHandler(this.cmdAufnahmedatum_Click);
            // 
            // cmdEntlassungsdatum
            // 
            this.cmdEntlassungsdatum.Location = new System.Drawing.Point(245, 93);
            this.cmdEntlassungsdatum.Name = "cmdEntlassungsdatum";
            this.cmdEntlassungsdatum.Size = new System.Drawing.Size(28, 20);
            this.cmdEntlassungsdatum.TabIndex = 14;
            this.cmdEntlassungsdatum.Text = "...";
            this.cmdEntlassungsdatum.UseVisualStyleBackColor = true;
            this.cmdEntlassungsdatum.Click += new System.EventHandler(this.cmdEntlassungsdatum_Click);
            // 
            // txtAufnahmedatum
            // 
            this.txtAufnahmedatum.BackColor = System.Drawing.SystemColors.Info;
            this.txtAufnahmedatum.Location = new System.Drawing.Point(10, 93);
            this.txtAufnahmedatum.MaxLength = 10;
            this.txtAufnahmedatum.Name = "txtAufnahmedatum";
            this.txtAufnahmedatum.Size = new System.Drawing.Size(85, 20);
            this.txtAufnahmedatum.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtAufnahmedatum, "Format: TT.MM.JJJJ");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Entlassungsdatum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Aufnahmedatum:";
            // 
            // txtEntlassungsdatum
            // 
            this.txtEntlassungsdatum.Location = new System.Drawing.Point(155, 92);
            this.txtEntlassungsdatum.MaxLength = 10;
            this.txtEntlassungsdatum.Name = "txtEntlassungsdatum";
            this.txtEntlassungsdatum.Size = new System.Drawing.Size(84, 20);
            this.txtEntlassungsdatum.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txtEntlassungsdatum, "Format: TT.MM.JJJJ");
            this.txtEntlassungsdatum.TextChanged += new System.EventHandler(this.txtEntlassungsdatum_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdZimmer);
            this.groupBox2.Controls.Add(this.txtBett);
            this.groupBox2.Location = new System.Drawing.Point(355, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 59);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zimmer";
            // 
            // cmdZimmer
            // 
            this.cmdZimmer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdZimmer.Location = new System.Drawing.Point(155, 16);
            this.cmdZimmer.Name = "cmdZimmer";
            this.cmdZimmer.Size = new System.Drawing.Size(119, 27);
            this.cmdZimmer.TabIndex = 10;
            this.cmdZimmer.Text = "Zimmer auswählen...";
            this.toolTip1.SetToolTip(this.cmdZimmer, "Hier klicken, um dem Patienten direkt ein Zimmer/Bett zuzuweisen");
            this.cmdZimmer.UseVisualStyleBackColor = true;
            this.cmdZimmer.Click += new System.EventHandler(this.cmdZimmer_Click);
            // 
            // txtBett
            // 
            this.txtBett.Location = new System.Drawing.Point(25, 19);
            this.txtBett.Name = "txtBett";
            this.txtBett.ReadOnly = true;
            this.txtBett.Size = new System.Drawing.Size(104, 20);
            this.txtBett.TabIndex = 9;
            // 
            // cmdGeburtsdatum
            // 
            this.cmdGeburtsdatum.Location = new System.Drawing.Point(97, 116);
            this.cmdGeburtsdatum.Name = "cmdGeburtsdatum";
            this.cmdGeburtsdatum.Size = new System.Drawing.Size(28, 20);
            this.cmdGeburtsdatum.TabIndex = 14;
            this.cmdGeburtsdatum.Text = "...";
            this.cmdGeburtsdatum.UseVisualStyleBackColor = true;
            this.cmdGeburtsdatum.Click += new System.EventHandler(this.cmdGeburtsdatum_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "Anzahl Betten: ";
            // 
            // cbAnzahlBetten
            // 
            this.cbAnzahlBetten.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnzahlBetten.FormattingEnabled = true;
            this.cbAnzahlBetten.Location = new System.Drawing.Point(159, 117);
            this.cbAnzahlBetten.Name = "cbAnzahlBetten";
            this.cbAnzahlBetten.Size = new System.Drawing.Size(88, 22);
            this.cbAnzahlBetten.TabIndex = 0;
            // 
            // radFrau
            // 
            this.radFrau.AutoSize = true;
            this.radFrau.Location = new System.Drawing.Point(63, 24);
            this.radFrau.Name = "radFrau";
            this.radFrau.Size = new System.Drawing.Size(47, 18);
            this.radFrau.TabIndex = 1;
            this.radFrau.TabStop = true;
            this.radFrau.Text = "Frau";
            this.radFrau.UseVisualStyleBackColor = true;
            // 
            // radMann
            // 
            this.radMann.AutoSize = true;
            this.radMann.Location = new System.Drawing.Point(6, 24);
            this.radMann.Name = "radMann";
            this.radMann.Size = new System.Drawing.Size(51, 18);
            this.radMann.TabIndex = 1;
            this.radMann.TabStop = true;
            this.radMann.Text = "Mann";
            this.radMann.UseVisualStyleBackColor = true;
            // 
            // chkPrivat
            // 
            this.chkPrivat.AutoSize = true;
            this.chkPrivat.Location = new System.Drawing.Point(159, 25);
            this.chkPrivat.Name = "chkPrivat";
            this.chkPrivat.Size = new System.Drawing.Size(53, 18);
            this.chkPrivat.TabIndex = 2;
            this.chkPrivat.Text = "Privat";
            this.chkPrivat.UseVisualStyleBackColor = true;
            // 
            // txtGeburtsdatum
            // 
            this.txtGeburtsdatum.BackColor = System.Drawing.SystemColors.Info;
            this.txtGeburtsdatum.Location = new System.Drawing.Point(6, 116);
            this.txtGeburtsdatum.MaxLength = 10;
            this.txtGeburtsdatum.Name = "txtGeburtsdatum";
            this.txtGeburtsdatum.Size = new System.Drawing.Size(85, 20);
            this.txtGeburtsdatum.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtGeburtsdatum, "Format: TT.MM.JJJJ");
            // 
            // txtNachname
            // 
            this.txtNachname.BackColor = System.Drawing.SystemColors.Info;
            this.txtNachname.Location = new System.Drawing.Point(6, 74);
            this.txtNachname.MaxLength = 50;
            this.txtNachname.Name = "txtNachname";
            this.txtNachname.Size = new System.Drawing.Size(147, 20);
            this.txtNachname.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "Geburtsdatum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nachname: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vorname: ";
            // 
            // txtVorname
            // 
            this.txtVorname.BackColor = System.Drawing.SystemColors.Window;
            this.txtVorname.Location = new System.Drawing.Point(159, 74);
            this.txtVorname.MaxLength = 50;
            this.txtVorname.Name = "txtVorname";
            this.txtVorname.Size = new System.Drawing.Size(168, 20);
            this.txtVorname.TabIndex = 4;
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(493, 412);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(81, 28);
            this.cmdOK.TabIndex = 11;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(578, 412);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 28);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNachname);
            this.groupBox1.Controls.Add(this.txtVorname);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmdGeburtsdatum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbAnzahlBetten);
            this.groupBox1.Controls.Add(this.txtGeburtsdatum);
            this.groupBox1.Controls.Add(this.radFrau);
            this.groupBox1.Controls.Add(this.chkPrivat);
            this.groupBox1.Controls.Add(this.radMann);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 190);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient";
            // 
            // PatientView
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(672, 449);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cmdOK);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient - Aufnahme";
            this.Shown += new System.EventHandler(this.PatientView_Shown);
            this.Load += new System.EventHandler(this.PatientEingangView_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, System.EventArgs e)
        {
            base.CancelClicked();
        }

        void cmdOK_Click(object sender, System.EventArgs e)
        {
            base.OKClicked();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDiagnosen;
        private System.Windows.Forms.TextBox txtAufnahmedatum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVorname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNachname;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEntlassungsdatum;
        private System.Windows.Forms.RadioButton radFrau;
        private System.Windows.Forms.RadioButton radMann;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdZimmer;
        private System.Windows.Forms.TextBox txtBett;
        private System.Windows.Forms.TextBox txtGeburtsdatum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkPrivat;
        private System.Windows.Forms.Button cmdAufnahmedatum;
        private System.Windows.Forms.Button cmdEntlassungsdatum;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.ComboBox cbAnzahlBetten;
        private System.Windows.Forms.CheckBox chkHervorheben;
        private System.Windows.Forms.CheckBox chkIsolation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdGeburtsdatum;
        private System.Windows.Forms.Button cmdInjektionDelete;
        private System.Windows.Forms.Button cmdInfektionNew;
        private System.Windows.Forms.Label label7;
        private OplListView lvInfektionen;
        private System.Windows.Forms.ComboBox cbIsolation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdKatalog;
        private System.Windows.Forms.TextBox txtHervorhebenGrund;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}