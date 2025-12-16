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
    partial class LogView : StationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
            this.cmdSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdClearFields = new System.Windows.Forms.Button();
            this.cmdDateBis = new System.Windows.Forms.Button();
            this.cmdDateVon = new System.Windows.Forms.Button();
            this.txtAktion = new System.Windows.Forms.TextBox();
            this.lblAktion = new System.Windows.Forms.Label();
            this.txtNumRecords = new System.Windows.Forms.TextBox();
            this.lblNumRecords = new System.Windows.Forms.Label();
            this.txtBis = new System.Windows.Forms.TextBox();
            this.lblBis = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtVon = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblVon = new System.Windows.Forms.Label();
            this.lvLogTable = new OplListView();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(404, 36);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(82, 26);
            this.cmdSearch.TabIndex = 7;
            this.cmdSearch.Text = "Suchen";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdClearFields);
            this.groupBox1.Controls.Add(this.cmdDateBis);
            this.groupBox1.Controls.Add(this.cmdDateVon);
            this.groupBox1.Controls.Add(this.txtAktion);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.lblAktion);
            this.groupBox1.Controls.Add(this.txtNumRecords);
            this.groupBox1.Controls.Add(this.lblNumRecords);
            this.groupBox1.Controls.Add(this.txtBis);
            this.groupBox1.Controls.Add(this.lblBis);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtMessage);
            this.groupBox1.Controls.Add(this.txtVon);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.lblUser);
            this.groupBox1.Controls.Add(this.lblVon);
            this.groupBox1.Controls.Add(this.lvLogTable);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 488);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // cmdClearFields
            // 
            this.cmdClearFields.Location = new System.Drawing.Point(404, 68);
            this.cmdClearFields.Name = "cmdClearFields";
            this.cmdClearFields.Size = new System.Drawing.Size(82, 26);
            this.cmdClearFields.TabIndex = 16;
            this.cmdClearFields.Text = "Felder leeren";
            this.cmdClearFields.UseVisualStyleBackColor = true;
            this.cmdClearFields.Click += new System.EventHandler(this.cmdClearFields_Click);
            // 
            // cmdDateBis
            // 
            this.cmdDateBis.Location = new System.Drawing.Point(82, 85);
            this.cmdDateBis.Name = "cmdDateBis";
            this.cmdDateBis.Size = new System.Drawing.Size(35, 20);
            this.cmdDateBis.TabIndex = 15;
            this.cmdDateBis.Text = "...";
            this.cmdDateBis.UseVisualStyleBackColor = true;
            this.cmdDateBis.Click += new System.EventHandler(this.cmdDateBis_Click);
            // 
            // cmdDateVon
            // 
            this.cmdDateVon.Location = new System.Drawing.Point(82, 39);
            this.cmdDateVon.Name = "cmdDateVon";
            this.cmdDateVon.Size = new System.Drawing.Size(35, 20);
            this.cmdDateVon.TabIndex = 14;
            this.cmdDateVon.Text = "...";
            this.cmdDateVon.UseVisualStyleBackColor = true;
            this.cmdDateVon.Click += new System.EventHandler(this.cmdDateVon_Click);
            // 
            // txtAktion
            // 
            this.txtAktion.Location = new System.Drawing.Point(172, 85);
            this.txtAktion.MaxLength = 20;
            this.txtAktion.Name = "txtAktion";
            this.txtAktion.Size = new System.Drawing.Size(188, 20);
            this.txtAktion.TabIndex = 13;
            // 
            // lblAktion
            // 
            this.lblAktion.AutoSize = true;
            this.lblAktion.Location = new System.Drawing.Point(169, 68);
            this.lblAktion.Name = "lblAktion";
            this.lblAktion.Size = new System.Drawing.Size(40, 13);
            this.lblAktion.TabIndex = 12;
            this.lblAktion.Text = "Aktion:";
            // 
            // txtNumRecords
            // 
            this.txtNumRecords.Location = new System.Drawing.Point(6, 139);
            this.txtNumRecords.MaxLength = 5;
            this.txtNumRecords.Name = "txtNumRecords";
            this.txtNumRecords.Size = new System.Drawing.Size(70, 20);
            this.txtNumRecords.TabIndex = 11;
            // 
            // lblNumRecords
            // 
            this.lblNumRecords.AutoSize = true;
            this.lblNumRecords.Location = new System.Drawing.Point(3, 123);
            this.lblNumRecords.Name = "lblNumRecords";
            this.lblNumRecords.Size = new System.Drawing.Size(128, 13);
            this.lblNumRecords.TabIndex = 10;
            this.lblNumRecords.Text = "Max. Anzahl der Eintr‰ge:";
            // 
            // txtBis
            // 
            this.txtBis.Location = new System.Drawing.Point(6, 85);
            this.txtBis.MaxLength = 10;
            this.txtBis.Name = "txtBis";
            this.txtBis.Size = new System.Drawing.Size(70, 20);
            this.txtBis.TabIndex = 9;
            // 
            // lblBis
            // 
            this.lblBis.AutoSize = true;
            this.lblBis.Location = new System.Drawing.Point(3, 69);
            this.lblBis.Name = "lblBis";
            this.lblBis.Size = new System.Drawing.Size(57, 13);
            this.lblBis.TabIndex = 8;
            this.lblBis.Text = "Datum bis:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(172, 38);
            this.txtUser.MaxLength = 50;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(188, 20);
            this.txtUser.TabIndex = 7;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(172, 138);
            this.txtMessage.MaxLength = 50;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(188, 20);
            this.txtMessage.TabIndex = 6;
            // 
            // txtVon
            // 
            this.txtVon.Location = new System.Drawing.Point(6, 40);
            this.txtVon.MaxLength = 10;
            this.txtVon.Name = "txtVon";
            this.txtVon.Size = new System.Drawing.Size(70, 20);
            this.txtVon.TabIndex = 5;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(169, 122);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(31, 13);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Text:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(169, 23);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(52, 13);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "Benutzer:";
            // 
            // lblVon
            // 
            this.lblVon.AutoSize = true;
            this.lblVon.Location = new System.Drawing.Point(3, 24);
            this.lblVon.Name = "lblVon";
            this.lblVon.Size = new System.Drawing.Size(62, 13);
            this.lblVon.TabIndex = 2;
            this.lblVon.Text = "Datum von:";
            // 
            // lvLogTable
            // 
            this.lvLogTable.HideSelection = false;
            this.lvLogTable.Location = new System.Drawing.Point(6, 164);
            this.lvLogTable.Name = "lvLogTable";
            this.lvLogTable.Size = new System.Drawing.Size(735, 318);
            this.lvLogTable.TabIndex = 1;
            this.lvLogTable.UseCompatibleStateImageBehavior = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(675, 506);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 26);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Schlieﬂen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CancelClicked);
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(769, 544);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logtabelle";
            this.Load += new System.EventHandler(this.LogView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private OplListView lvLogTable;
        private Button cmdCancel;
        private Label lblVon;
        private TextBox txtUser;
        private TextBox txtMessage;
        private TextBox txtVon;
        private Label lblMessage;
        private Label lblUser;
        private TextBox txtBis;
        private Label lblBis;
        private TextBox txtNumRecords;
        private Label lblNumRecords;
        private TextBox txtAktion;
        private Label lblAktion;
        private Button cmdDateVon;
        private Button cmdDateBis;
        private Button cmdClearFields;
    }
}