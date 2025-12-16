namespace StationDesigner.Dialogs
{
    partial class TextfeldView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextfeldView));
            this.label1 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpZimmer = new System.Windows.Forms.GroupBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.grpZimmer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text:";
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(206, 95);
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
            this.cmdCancel.Location = new System.Drawing.Point(291, 95);
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
            // grpZimmer
            // 
            this.grpZimmer.Controls.Add(this.txtText);
            this.grpZimmer.Controls.Add(this.label1);
            this.grpZimmer.Location = new System.Drawing.Point(12, 12);
            this.grpZimmer.Name = "grpZimmer";
            this.grpZimmer.Size = new System.Drawing.Size(361, 77);
            this.grpZimmer.TabIndex = 0;
            this.grpZimmer.TabStop = false;
            this.grpZimmer.Text = "Textfeld";
            // 
            // txtText
            // 
            this.txtText.BackColor = System.Drawing.SystemColors.Window;
            this.txtText.Location = new System.Drawing.Point(107, 35);
            this.txtText.MaxLength = 50;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(226, 20);
            this.txtText.TabIndex = 1;
            // 
            // TextfeldView
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(385, 132);
            this.Controls.Add(this.grpZimmer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextfeldView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Textfeld";
            this.Load += new System.EventHandler(this.DiagrammView_Load);
            this.grpZimmer.ResumeLayout(false);
            this.grpZimmer.PerformLayout();
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
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox grpZimmer;
        private System.Windows.Forms.TextBox txtText;
    }
}