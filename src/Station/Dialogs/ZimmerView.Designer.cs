using Windows.Forms;

namespace Station
{
    partial class ZimmerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZimmerView));
            this.grpBetten = new System.Windows.Forms.GroupBox();
            this.cmdPatient = new System.Windows.Forms.Button();
            this.lvPatienten = new OplListView();
            this.cmdBettDelete = new System.Windows.Forms.Button();
            this.cmdBettAdd = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpBetten.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBetten
            // 
            this.grpBetten.Controls.Add(this.cmdPatient);
            this.grpBetten.Controls.Add(this.lvPatienten);
            this.grpBetten.Controls.Add(this.cmdBettDelete);
            this.grpBetten.Controls.Add(this.cmdBettAdd);
            this.grpBetten.Location = new System.Drawing.Point(6, 12);
            this.grpBetten.Name = "grpBetten";
            this.grpBetten.Size = new System.Drawing.Size(580, 164);
            this.grpBetten.TabIndex = 2;
            this.grpBetten.TabStop = false;
            this.grpBetten.Text = "Bettenbelegung";
            // 
            // cmdPatient
            // 
            this.cmdPatient.Location = new System.Drawing.Point(220, 125);
            this.cmdPatient.Name = "cmdPatient";
            this.cmdPatient.Size = new System.Drawing.Size(111, 28);
            this.cmdPatient.TabIndex = 13;
            this.cmdPatient.Text = "Patient...";
            this.toolTip.SetToolTip(this.cmdPatient, "Hier klicken um den Patienten zu entfernen");
            this.cmdPatient.UseVisualStyleBackColor = true;
            this.cmdPatient.Click += new System.EventHandler(this.cmdPatient_Click);
            // 
            // lvPatienten
            // 
            this.lvPatienten.Location = new System.Drawing.Point(6, 19);
            this.lvPatienten.Name = "lvPatienten";
            this.lvPatienten.Size = new System.Drawing.Size(568, 100);
            this.lvPatienten.TabIndex = 12;
            this.lvPatienten.UseCompatibleStateImageBehavior = false;
            this.lvPatienten.DoubleClick += new System.EventHandler(this.lvPatienten_DoubleClick);
            // 
            // cmdBettDelete
            // 
            this.cmdBettDelete.Location = new System.Drawing.Point(337, 125);
            this.cmdBettDelete.Name = "cmdBettDelete";
            this.cmdBettDelete.Size = new System.Drawing.Size(111, 28);
            this.cmdBettDelete.TabIndex = 11;
            this.cmdBettDelete.Text = "Bett freigeben";
            this.toolTip.SetToolTip(this.cmdBettDelete, "Hier klicken um den Patienten zu entfernen");
            this.cmdBettDelete.UseVisualStyleBackColor = true;
            this.cmdBettDelete.Click += new System.EventHandler(this.cmdBettDelete_Click);
            // 
            // cmdBettAdd
            // 
            this.cmdBettAdd.Location = new System.Drawing.Point(454, 125);
            this.cmdBettAdd.Name = "cmdBettAdd";
            this.cmdBettAdd.Size = new System.Drawing.Size(111, 28);
            this.cmdBettAdd.TabIndex = 6;
            this.cmdBettAdd.Text = "Bett belegen...";
            this.toolTip.SetToolTip(this.cmdBettAdd, "Hier klicken um einen Patienten auszuwählen");
            this.cmdBettAdd.UseVisualStyleBackColor = true;
            this.cmdBettAdd.Click += new System.EventHandler(this.cmdBettAdd_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(504, 182);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 28);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(416, 182);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(82, 28);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // ZimmerView
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(598, 225);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.grpBetten);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZimmerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zimmer Belegung";
            this.Load += new System.EventHandler(this.ZimmerView_Load);
            this.grpBetten.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBetten;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdBettAdd;
        private System.Windows.Forms.Button cmdBettDelete;
        private System.Windows.Forms.ToolTip toolTip;
        private OplListView lvPatienten;
        private System.Windows.Forms.Button cmdPatient;
    }
}