using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Forms;

namespace Station
{
    partial class DiagnosenView : StationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagnosenView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radSortDRG_Name = new System.Windows.Forms.RadioButton();
            this.radSortDRG = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtM_GVD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtO_GVD = new System.Windows.Forms.TextBox();
            this.txtDRG = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDRG_Name = new System.Windows.Forms.TextBox();
            this.lvDiagnosen = new OplListView();
            this.txtU_GVD = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.radSortDRG_Name);
            this.groupBox1.Controls.Add(this.radSortDRG);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtM_GVD);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtO_GVD);
            this.groupBox1.Controls.Add(this.txtDRG);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmdApply);
            this.groupBox1.Controls.Add(this.cmdDelete);
            this.groupBox1.Controls.Add(this.cmdInsert);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDRG_Name);
            this.groupBox1.Controls.Add(this.lvDiagnosen);
            this.groupBox1.Controls.Add(this.txtU_GVD);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 474);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alle Einträge";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(594, 297);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Sortierung:";
            // 
            // radSortDRG_Name
            // 
            this.radSortDRG_Name.AutoSize = true;
            this.radSortDRG_Name.Location = new System.Drawing.Point(597, 336);
            this.radSortDRG_Name.Name = "radSortDRG_Name";
            this.radSortDRG_Name.Size = new System.Drawing.Size(70, 17);
            this.radSortDRG_Name.TabIndex = 16;
            this.radSortDRG_Name.TabStop = true;
            this.radSortDRG_Name.Text = "Diagnose";
            this.radSortDRG_Name.UseVisualStyleBackColor = true;
            this.radSortDRG_Name.CheckedChanged += new System.EventHandler(this.radSortDRG_Name_CheckedChanged);
            // 
            // radSortDRG
            // 
            this.radSortDRG.AutoSize = true;
            this.radSortDRG.Location = new System.Drawing.Point(597, 313);
            this.radSortDRG.Name = "radSortDRG";
            this.radSortDRG.Size = new System.Drawing.Size(49, 17);
            this.radSortDRG.TabIndex = 15;
            this.radSortDRG.TabStop = true;
            this.radSortDRG.Text = "DRG";
            this.radSortDRG.UseVisualStyleBackColor = true;
            this.radSortDRG.CheckedChanged += new System.EventHandler(this.radSortDRG_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(10, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(706, 2);
            this.label6.TabIndex = 4;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Mittlere Grenzverweildauer:";
            // 
            // txtM_GVD
            // 
            this.txtM_GVD.Location = new System.Drawing.Point(287, 313);
            this.txtM_GVD.MaxLength = 10;
            this.txtM_GVD.Name = "txtM_GVD";
            this.txtM_GVD.Size = new System.Drawing.Size(64, 20);
            this.txtM_GVD.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(425, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Obere Grenzverweildauer:";
            // 
            // txtO_GVD
            // 
            this.txtO_GVD.Location = new System.Drawing.Point(429, 313);
            this.txtO_GVD.MaxLength = 10;
            this.txtO_GVD.Name = "txtO_GVD";
            this.txtO_GVD.Size = new System.Drawing.Size(64, 20);
            this.txtO_GVD.TabIndex = 14;
            // 
            // txtDRG
            // 
            this.txtDRG.Location = new System.Drawing.Point(10, 313);
            this.txtDRG.MaxLength = 20;
            this.txtDRG.Name = "txtDRG";
            this.txtDRG.Size = new System.Drawing.Size(111, 20);
            this.txtDRG.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "DRG:";
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(634, 436);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(82, 26);
            this.cmdApply.TabIndex = 2;
            this.cmdApply.Text = "Ändern";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(634, 247);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(82, 26);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Löschen...";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(546, 436);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(82, 26);
            this.cmdInsert.TabIndex = 1;
            this.cmdInsert.Text = "Einfügen";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Text:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Untere Grenzverweildauer:";
            // 
            // txtDRG_Name
            // 
            this.txtDRG_Name.Location = new System.Drawing.Point(6, 364);
            this.txtDRG_Name.MaxLength = 255;
            this.txtDRG_Name.Multiline = true;
            this.txtDRG_Name.Name = "txtDRG_Name";
            this.txtDRG_Name.Size = new System.Drawing.Size(706, 66);
            this.txtDRG_Name.TabIndex = 8;
            // 
            // lvDiagnosen
            // 
            this.lvDiagnosen.FullRowSelect = true;
            this.lvDiagnosen.GridLines = true;
            this.lvDiagnosen.HideSelection = false;
            this.lvDiagnosen.Location = new System.Drawing.Point(7, 19);
            this.lvDiagnosen.Name = "lvDiagnosen";
            this.lvDiagnosen.Size = new System.Drawing.Size(621, 254);
            this.lvDiagnosen.TabIndex = 0;
            this.lvDiagnosen.UseCompatibleStateImageBehavior = false;
            this.lvDiagnosen.View = System.Windows.Forms.View.Details;
            this.lvDiagnosen.SelectedIndexChanged += new System.EventHandler(this.lvDiagnosen_SelectedIndexChanged);
            // 
            // txtU_GVD
            // 
            this.txtU_GVD.Location = new System.Drawing.Point(147, 313);
            this.txtU_GVD.MaxLength = 10;
            this.txtU_GVD.Name = "txtU_GVD";
            this.txtU_GVD.Size = new System.Drawing.Size(64, 20);
            this.txtU_GVD.TabIndex = 10;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(644, 492);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 26);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Schließen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label8.Location = new System.Drawing.Point(16, 496);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(622, 45);
            this.label8.TabIndex = 2;
            this.label8.Text = "Die Mittlere GVD wird automatisch auf das Aufnahmedatum addiert und ergibt das En" +
                "tlassungsdatum, wenn beim Patient eine Prozedur/ICD ausgewählt wird.";
            // 
            // DiagnosenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(744, 552);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiagnosenView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Diagnosen - Katalog";
            this.Load += new System.EventHandler(this.DiagnosenKatalogView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private OplListView lvDiagnosen;
        private TextBox txtU_GVD;
        private Button cmdCancel;
        private TextBox txtDRG_Name;
        protected override void Object2Control() { }

        private Button cmdDelete;
        private Button cmdInsert;
        private Label label1;
        private Label label2;
        private Button cmdApply;
        private Label label5;
        private TextBox txtM_GVD;
        private Label label4;
        private TextBox txtO_GVD;
        private TextBox txtDRG;
        private Label label3;
        private Label label6;
        private Label label7;
        private RadioButton radSortDRG_Name;
        private RadioButton radSortDRG;
        private Label label8;
    }
}