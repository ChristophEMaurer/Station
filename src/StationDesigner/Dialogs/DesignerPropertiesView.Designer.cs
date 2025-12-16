namespace StationDesigner.Dialogs
{
    partial class DesignerPropertiesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerPropertiesView));
            this.lblGridSize = new System.Windows.Forms.Label();
            this.txtGridSize = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpGitter = new System.Windows.Forms.GroupBox();
            this.chkSnapToGrid = new System.Windows.Forms.CheckBox();
            this.chkShowGrid = new System.Windows.Forms.CheckBox();
            this.grpGitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Location = new System.Drawing.Point(6, 70);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(70, 14);
            this.lblGridSize.TabIndex = 2;
            this.lblGridSize.Text = "Gitter Größe:";
            // 
            // txtGridSize
            // 
            this.txtGridSize.BackColor = System.Drawing.SystemColors.Window;
            this.txtGridSize.Location = new System.Drawing.Point(91, 67);
            this.txtGridSize.MaxLength = 50;
            this.txtGridSize.Name = "txtGridSize";
            this.txtGridSize.Size = new System.Drawing.Size(46, 20);
            this.txtGridSize.TabIndex = 3;
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(103, 124);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(81, 28);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(188, 124);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 28);
            this.cmdCancel.TabIndex = 2;
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
            // grpGitter
            // 
            this.grpGitter.Controls.Add(this.chkShowGrid);
            this.grpGitter.Controls.Add(this.chkSnapToGrid);
            this.grpGitter.Controls.Add(this.txtGridSize);
            this.grpGitter.Controls.Add(this.lblGridSize);
            this.grpGitter.Location = new System.Drawing.Point(12, 12);
            this.grpGitter.Name = "grpGitter";
            this.grpGitter.Size = new System.Drawing.Size(258, 96);
            this.grpGitter.TabIndex = 0;
            this.grpGitter.TabStop = false;
            this.grpGitter.Text = "Gitter";
            // 
            // chkSnapToGrid
            // 
            this.chkSnapToGrid.AutoSize = true;
            this.chkSnapToGrid.Location = new System.Drawing.Point(9, 19);
            this.chkSnapToGrid.Name = "chkSnapToGrid";
            this.chkSnapToGrid.Size = new System.Drawing.Size(147, 18);
            this.chkSnapToGrid.TabIndex = 3;
            this.chkSnapToGrid.Text = "An Gitterlinien ausrichten";
            this.chkSnapToGrid.UseVisualStyleBackColor = true;
            // 
            // chkShowGrid
            // 
            this.chkShowGrid.AutoSize = true;
            this.chkShowGrid.Location = new System.Drawing.Point(9, 43);
            this.chkShowGrid.Name = "chkShowGrid";
            this.chkShowGrid.Size = new System.Drawing.Size(99, 18);
            this.chkShowGrid.TabIndex = 4;
            this.chkShowGrid.Text = "Gitter anzeigen";
            this.chkShowGrid.UseVisualStyleBackColor = true;
            // 
            // DesignerPropertiesView
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(281, 167);
            this.Controls.Add(this.grpGitter);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DesignerPropertiesView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Designer Einstellungen";
            this.Load += new System.EventHandler(this.DesignerPropertiesView_Load);
            this.grpGitter.ResumeLayout(false);
            this.grpGitter.PerformLayout();
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

        private System.Windows.Forms.TextBox txtGridSize;
        private System.Windows.Forms.Label lblGridSize;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox grpGitter;
        private System.Windows.Forms.CheckBox chkSnapToGrid;
        private System.Windows.Forms.CheckBox chkShowGrid;
    }
}