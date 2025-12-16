namespace Station.UserControls
{
    partial class InformationBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpInformation = new System.Windows.Forms.GroupBox();
            this.lvInfektionen = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBelegung = new System.Windows.Forms.Label();
            this.lblFreieZimmer = new System.Windows.Forms.Label();
            this.lblFreieFrauenbetten = new System.Windows.Forms.Label();
            this.lblFreieMaennerbetten = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInformation
            // 
            this.grpInformation.Controls.Add(this.lvInfektionen);
            this.grpInformation.Controls.Add(this.label2);
            this.grpInformation.Controls.Add(this.lblBelegung);
            this.grpInformation.Controls.Add(this.lblFreieZimmer);
            this.grpInformation.Controls.Add(this.lblFreieFrauenbetten);
            this.grpInformation.Controls.Add(this.lblFreieMaennerbetten);
            this.grpInformation.Controls.Add(this.label12);
            this.grpInformation.Controls.Add(this.label11);
            this.grpInformation.Controls.Add(this.label8);
            this.grpInformation.Controls.Add(this.label7);
            this.grpInformation.Location = new System.Drawing.Point(0, 0);
            this.grpInformation.Name = "grpInformation";
            this.grpInformation.Size = new System.Drawing.Size(262, 512);
            this.grpInformation.TabIndex = 12;
            this.grpInformation.TabStop = false;
            this.grpInformation.Text = "Information";
            // 
            // lvInfektionen
            // 
            this.lvInfektionen.Location = new System.Drawing.Point(6, 113);
            this.lvInfektionen.Name = "lvInfektionen";
            this.lvInfektionen.Size = new System.Drawing.Size(250, 391);
            this.lvInfektionen.TabIndex = 24;
            this.lvInfektionen.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 2);
            this.label2.TabIndex = 23;
            // 
            // lblBelegung
            // 
            this.lblBelegung.Location = new System.Drawing.Point(146, 72);
            this.lblBelegung.Name = "lblBelegung";
            this.lblBelegung.Size = new System.Drawing.Size(110, 17);
            this.lblBelegung.TabIndex = 20;
            this.lblBelegung.Text = "lblBelegung";
            // 
            // lblFreieZimmer
            // 
            this.lblFreieZimmer.Location = new System.Drawing.Point(146, 56);
            this.lblFreieZimmer.Name = "lblFreieZimmer";
            this.lblFreieZimmer.Size = new System.Drawing.Size(110, 17);
            this.lblFreieZimmer.TabIndex = 19;
            this.lblFreieZimmer.Text = "lblFreieZimmer";
            // 
            // lblFreieFrauenbetten
            // 
            this.lblFreieFrauenbetten.Location = new System.Drawing.Point(146, 38);
            this.lblFreieFrauenbetten.Name = "lblFreieFrauenbetten";
            this.lblFreieFrauenbetten.Size = new System.Drawing.Size(110, 17);
            this.lblFreieFrauenbetten.TabIndex = 18;
            this.lblFreieFrauenbetten.Text = "lblFreieFrauenbetten";
            // 
            // lblFreieMaennerbetten
            // 
            this.lblFreieMaennerbetten.Location = new System.Drawing.Point(146, 21);
            this.lblFreieMaennerbetten.Name = "lblFreieMaennerbetten";
            this.lblFreieMaennerbetten.Size = new System.Drawing.Size(110, 17);
            this.lblFreieMaennerbetten.TabIndex = 17;
            this.lblFreieMaennerbetten.Text = "lblFreieMaennerbetten";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(7, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 14);
            this.label12.TabIndex = 8;
            this.label12.Text = "Belegung:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 14);
            this.label11.TabIndex = 6;
            this.label11.Text = "Freie Betten/Zimmer:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "Freie Frauenbetten:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "Freie Männerbetten:";
            // 
            // InformationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpInformation);
            this.Name = "InformationBox";
            this.Size = new System.Drawing.Size(270, 522);
            this.Load += new System.EventHandler(this.InformationBox_Load);
            this.grpInformation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInformation;
        protected internal System.Windows.Forms.ListView lvInfektionen;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblBelegung;
        public System.Windows.Forms.Label lblFreieZimmer;
        public System.Windows.Forms.Label lblFreieFrauenbetten;
        public System.Windows.Forms.Label lblFreieMaennerbetten;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}
