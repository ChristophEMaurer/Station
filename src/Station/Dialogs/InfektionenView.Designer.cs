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
    partial class InfektionenView : StationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfektionenView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radSonstiges = new System.Windows.Forms.RadioButton();
            this.radInfektion = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lvInfektionen = new OplListView();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radSonstiges);
            this.groupBox1.Controls.Add(this.radInfektion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmdApply);
            this.groupBox1.Controls.Add(this.cmdDelete);
            this.groupBox1.Controls.Add(this.cmdInsert);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lvInfektionen);
            this.groupBox1.Controls.Add(this.txtOrder);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 492);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alle Einträge";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(443, 43);
            this.label3.TabIndex = 11;
            this.label3.Text = "Diese Einträge können im Fenster \'Patient bearbeiten\' einem Patienten unter \'Noso" +
                "komiale Infektionen/Sonstiges\' zugeordnet werden.";
            // 
            // radSonstiges
            // 
            this.radSonstiges.AutoSize = true;
            this.radSonstiges.Location = new System.Drawing.Point(299, 345);
            this.radSonstiges.Name = "radSonstiges";
            this.radSonstiges.Size = new System.Drawing.Size(71, 17);
            this.radSonstiges.TabIndex = 10;
            this.radSonstiges.TabStop = true;
            this.radSonstiges.Text = "Sonstiges";
            this.radSonstiges.UseVisualStyleBackColor = true;
            // 
            // radInfektion
            // 
            this.radInfektion.AutoSize = true;
            this.radInfektion.Location = new System.Drawing.Point(216, 345);
            this.radInfektion.Name = "radInfektion";
            this.radInfektion.Size = new System.Drawing.Size(66, 17);
            this.radInfektion.TabIndex = 9;
            this.radInfektion.TabStop = true;
            this.radInfektion.Text = "Infektion";
            this.radInfektion.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(12, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(526, 2);
            this.label6.TabIndex = 4;
            this.label6.Text = "label6";
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(453, 455);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(82, 26);
            this.cmdApply.TabIndex = 2;
            this.cmdApply.Text = "Ändern";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(456, 247);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(82, 26);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Löschen...";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(365, 455);
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
            this.label1.Location = new System.Drawing.Point(8, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Text:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Reihenfolge:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(81, 370);
            this.txtName.MaxLength = 255;
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(454, 79);
            this.txtName.TabIndex = 6;
            // 
            // lvInfektionen
            // 
            this.lvInfektionen.FullRowSelect = true;
            this.lvInfektionen.GridLines = true;
            this.lvInfektionen.HideSelection = false;
            this.lvInfektionen.Location = new System.Drawing.Point(7, 19);
            this.lvInfektionen.MultiSelect = false;
            this.lvInfektionen.Name = "lvInfektionen";
            this.lvInfektionen.Size = new System.Drawing.Size(442, 254);
            this.lvInfektionen.TabIndex = 0;
            this.lvInfektionen.UseCompatibleStateImageBehavior = false;
            this.lvInfektionen.View = System.Windows.Forms.View.Details;
            this.lvInfektionen.SelectedIndexChanged += new System.EventHandler(this.lvInfektionen_SelectedIndexChanged);
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(81, 344);
            this.txtOrder.MaxLength = 10;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(66, 20);
            this.txtOrder.TabIndex = 8;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(476, 510);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 26);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Schließen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // InfektionenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(566, 545);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfektionenView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Infektionen-Katalog";
            this.Load += new System.EventHandler(this.InfektionenKatalogView_Load);
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
        private OplListView lvInfektionen;
        private TextBox txtOrder;
        private Button cmdCancel;
        private TextBox txtName;
        protected override void Object2Control() { }

        private Button cmdDelete;
        private Button cmdInsert;
        private Label label1;
        private Label label2;
        private Button cmdApply;
        private Label label6;
        private RadioButton radSonstiges;
        private RadioButton radInfektion;
        private Label label3;
    }
}