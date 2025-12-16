using System;
using System.Reflection;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;

using Windows.Forms;
using Utility;
using Station;

namespace Station
{
	/// <summary>
    /// Zusammenfassung für OperationenImportView.
	/// </summary>
    public class StationImportView : StationForm
	{
        private string _strLogfileName;

        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblInfo;

        private Button cmdOK;
        private GroupBox groupBox1;
        private Label lblInserted;
        private Button cmdAbort;
        private bool _bAbort = false;
        private CheckBox chkInsertUnknown;
        private Label label3;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private OplListView lvPlugins;

        private bool _bInsertUnknown;
        private bool _bImportError; // one time error message flag
        private StreamWriter _oProtokoll;
        private int _nCountTotal;
        private int _nCountNew;
        private Button cmdInfo;
        private Label label2;
        private Label lblTotal;

		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public StationImportView(BusinessLayer b)
            :
            base(b)
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationImportView));
            this.cmdImport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkInsertUnknown = new System.Windows.Forms.CheckBox();
            this.lblInserted = new System.Windows.Forms.Label();
            this.cmdAbort = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdInfo = new System.Windows.Forms.Button();
            this.lvPlugins = new OplListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdImport
            // 
            this.cmdImport.Location = new System.Drawing.Point(13, 373);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(144, 26);
            this.cmdImport.TabIndex = 0;
            this.cmdImport.Text = "Import starten...";
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 60);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(585, 24);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(6, 18);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(577, 40);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = resources.GetString("lblInfo.Text");
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Location = new System.Drawing.Point(514, 374);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(96, 26);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "Schließen";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkInsertUnknown);
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Location = new System.Drawing.Point(12, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 99);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // chkInsertUnknown
            // 
            this.chkInsertUnknown.AutoSize = true;
            this.chkInsertUnknown.Checked = true;
            this.chkInsertUnknown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsertUnknown.Location = new System.Drawing.Point(15, 61);
            this.chkInsertUnknown.Name = "chkInsertUnknown";
            this.chkInsertUnknown.Size = new System.Drawing.Size(225, 17);
            this.chkInsertUnknown.TabIndex = 5;
            this.chkInsertUnknown.Text = "Fehlende Patienten automatisch einfügen ";
            this.chkInsertUnknown.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkInsertUnknown.UseVisualStyleBackColor = true;
            // 
            // lblInserted
            // 
            this.lblInserted.Location = new System.Drawing.Point(446, 25);
            this.lblInserted.Name = "lblInserted";
            this.lblInserted.Size = new System.Drawing.Size(89, 20);
            this.lblInserted.TabIndex = 4;
            this.lblInserted.Text = "0";
            // 
            // cmdAbort
            // 
            this.cmdAbort.Enabled = false;
            this.cmdAbort.Location = new System.Drawing.Point(163, 373);
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.Size = new System.Drawing.Size(144, 26);
            this.cmdAbort.TabIndex = 5;
            this.cmdAbort.Text = "Import abbrechen";
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Gesamtanzahl Datensätze:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblInserted);
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Location = new System.Drawing.Point(12, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 103);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fortschritt";
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(147, 25);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(89, 20);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Importierte neue Datensätze:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdInfo);
            this.groupBox3.Controls.Add(this.lvPlugins);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(598, 149);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import Plugins";
            // 
            // cmdInfo
            // 
            this.cmdInfo.Location = new System.Drawing.Point(6, 115);
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.Size = new System.Drawing.Size(96, 26);
            this.cmdInfo.TabIndex = 1;
            this.cmdInfo.Text = "Beschreibung...";
            this.cmdInfo.Click += new System.EventHandler(this.cmdInfo_Click);
            // 
            // lvPlugins
            // 
            this.lvPlugins.Location = new System.Drawing.Point(6, 19);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.Size = new System.Drawing.Size(577, 90);
            this.lvPlugins.TabIndex = 0;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            // 
            // StationImportView
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdOK;
            this.ClientSize = new System.Drawing.Size(624, 412);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdAbort);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StationImportView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Import";
            this.Load += new System.EventHandler(this.OperationenImportView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion


        private void PatientenImport(string strAssemblyFilename)
        {
            try
            {
                BusinessLayer.OpenDatabaseForImport();

                string strPath = BusinessLayer.PathLogfiles;
                string strFilename = Tools.DBNullableDateTime2DateString(DateTime.Now) + "-" + Tools.DBNullableDateTime2TimeString(DateTime.Now) + ".log";

                // Zeit enthaelt einen :, der ist nicht in Dateinamen erlaubt
                strFilename = strFilename.Replace(':', '.');
                _strLogfileName = strPath + System.IO.Path.DirectorySeparatorChar + strFilename;
                _oProtokoll = new StreamWriter(_strLogfileName, true);

                Protokoll("Lade Plugin " + strAssemblyFilename + "...");
                Assembly plugin = Assembly.LoadFile(strAssemblyFilename);

                Type[] types = plugin.GetTypes();

                foreach (Type t in types)
                {
                    if (t.BaseType == typeof(StationImport))
                    {
                        StationImport o = (StationImport)Activator.CreateInstance(t);
                        Protokoll("... Plugin wurde erfolgreich geladen: " + strAssemblyFilename + ".");
                        Protokoll("Beginn des Datenimports.");

                        o.Import += new StationImport.ImportHandler(o_Import);

                        _nCountTotal = 0;
                        _nCountNew = 0;

                        o.ImportInit();
                        o.ImportRun();
                        o.ImportFinalize();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox("Daten Import fehlgeschlagen:\n\n" + ex.Message);
            }
            finally
            {
                BusinessLayer.CloseDatabaseForImport();

                Protokoll("Ende des Daten Imports.");
                if (_oProtokoll != null)
                {
                    _oProtokoll.Flush();
                    _oProtokoll.Close();
                }
            }
        }

        void o_Import(object sender, ImportEvent e)
        {
            this.ProgressInc();

            _nCountTotal++;

            if (_bAbort)
            {
                e.Abort = true;
                Protokoll("Info: Import durch Benutzer abgebrochen.");
            }
            else
            {
                switch (e.State)
                {
                    case EVENT_STATE.STATE_ERROR:
                        Protokoll("Fehler:" + e.StateText);
                        break;

                    case EVENT_STATE.STATE_DATA:
                        ImportLine(e);
                        break;

                    case EVENT_STATE.STATE_INFO:
                        Protokoll("Info: " + e.StateText);
                        break;

                    default:
                        break;
                }
            }

            lblInserted.Text = _nCountNew.ToString();
            lblTotal.Text = _nCountTotal.ToString();

            Application.DoEvents();
        }

        private void ProgressStart()
        {
            progressBar.Style = ProgressBarStyle.Continuous;
        }

        private void ProgressStop()
        {
            progressBar.Value = 0;
        }

        private void ProgressInc()
        {
            progressBar.Increment(1);
            if (progressBar.Value >= progressBar.Maximum)
            {
                progressBar.Value = 0;
            }
        }

        private void cmdImport_Click(object sender, System.EventArgs e)
        {
            string strPlugin = this.GetFirstSelectedTagString(lvPlugins, true);

            if (strPlugin != null && strPlugin.Length > 0)
            {
                Control2Object();

                _bAbort = false;
                cmdImport.Enabled = false;
                cmdOK.Enabled = false;
                cmdInfo.Enabled = false;
                chkInsertUnknown.Enabled = false;
                cmdAbort.Enabled = true;
                lblInserted.Text = "0";
                ProgressStart();
                PatientenImport(strPlugin);
                ProgressStop();
                cmdAbort.Enabled = false;
                cmdImport.Enabled = true;
                cmdOK.Enabled = true;
                cmdInfo.Enabled = true;
                chkInsertUnknown.Enabled = true;

                ViewProtocol();
            }
        }

        private void InitPlugins()
        {
            lvPlugins.Clear();

            DefaultListViewProperties(lvPlugins);

            lvPlugins.Columns.Add("Dateiname", 300, HorizontalAlignment.Left);
            lvPlugins.Columns.Add("Beschreibung", -2, HorizontalAlignment.Left);
        }

        private void PopulatePlugins()
        {
            lvPlugins.BeginUpdate();
            lvPlugins.Items.Clear();

            string strPluginPath = BusinessLayer.PathPlugins;

            if (Directory.Exists(strPluginPath))
            {
                DirectoryInfo dir = new DirectoryInfo(strPluginPath);
                FileInfo[] files = dir.GetFiles("*.dll");

                foreach (FileInfo file in files)
                {
                    Assembly asm = null;

                    try
                    {
                        asm = Assembly.LoadFile(file.FullName);
                    }
                    catch (Exception e)
                    {
                        this.MessageBox("Plugin"
                            + Environment.NewLine
                            + Environment.NewLine
                            + "'" + file.FullName + "'"
                            + Environment.NewLine
                            + Environment.NewLine
                            + " konnte nicht geladen werden:"
                            + Environment.NewLine
                            + Environment.NewLine
                            + e.Message
                            );
                        continue;
                    }

                    Type[] types = null;

                    try
                    {
                        types = asm.GetTypes();
                    }
                    catch (Exception e)
                    {
                        this.MessageBox("Plugin"
                            + Environment.NewLine
                            + Environment.NewLine
                            + "'" + file.FullName + "'"
                            + Environment.NewLine
                            + Environment.NewLine
                            + " ist nicht vom richtigen Typ:"
                            + Environment.NewLine
                            + Environment.NewLine
                            + e.Message
                            );
                        continue;
                    }

                    foreach (Type t in types)
                    {
                        if (t.BaseType == typeof(StationImport))
                        {
                            string strAssemblyDescription =
                                ((AssemblyDescriptionAttribute)
                                asm.GetCustomAttributes(
                                typeof(AssemblyDescriptionAttribute), false)[0]).Description;

                            ListViewItem lvi = new ListViewItem(file.Name);
                            lvi.Tag = file.FullName;

                            lvi.SubItems.Add(strAssemblyDescription);

                            lvPlugins.Items.Add(lvi);
                        }
                    }
                }
            }
            lvPlugins.EndUpdate();
        }

        private void OperationenImportView_Load(object sender, EventArgs e)
        {
            Text = BusinessLayer.AppTitle("Patienten-Import");
            lblInfo.ForeColor = BusinessLayer.InfoColor;

            InitPlugins();
            PopulatePlugins();
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            _bAbort = true;
        }

        protected override void Control2Object() 
        { 
            _bInsertUnknown = chkInsertUnknown.Checked;
        }

        protected override void Object2Control() 
        { 
            chkInsertUnknown.Checked = _bInsertUnknown;
        }

        public void ImportLine(ImportEvent e)
        {
            // Hier braucht man nur die ID des Datensatzes
            int nID_Patienten = BusinessLayer.GetID_Patienten(e.LastName, e.FirstName, e.BirthDate);

            if (nID_Patienten == -1)
            {
                if (_bInsertUnknown)
                {
                    DataRow oPatient;

                    Protokoll("Unbekannter Patient wird eingefügt...");
                    oPatient = this.BusinessLayer.CreateDataRowPatient();
                    oPatient["Nachname"] = e.LastName;
                    oPatient["Vorname"] = e.FirstName;
                    oPatient["Geburtsdatum"] = e.BirthDate;

                    nID_Patienten = BusinessLayer.InsertPatient(oPatient);
                    if (nID_Patienten > 0)
                    {
                        Protokoll("...unbekannter Patient wurde erfolgreich eingefügt.");
                    }
                    else
                    {
                        string s = "...unbekannter Patient '" + e.LastName + "' konnte NICHT eingefügt werden.";
                        ImportError(e, s);
                        Protokoll(s);
                    }
                }
            }
        }

        private void ViewProtocol()
        {
            try
            {
                if (_strLogfileName != null)
                {
                    Process oProcess = Process.Start(_strLogfileName);
                }
            }
            catch
            {
            }
        }
        private void Protokoll(string sMessage)
        {
            if (_oProtokoll != null)
            {
                _oProtokoll.WriteLine(DateTime.Now.ToLongTimeString() + ": " + sMessage);
                _oProtokoll.Flush();
            }
        }

        private void ImportError(ImportEvent e, string s)
        {
            if (!_bImportError)
            {
                // Nur beim ersten Fehler eine Messagebox anzeigen
                MessageBox(s);
            }
            _bImportError = true;
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            string strPlugin = this.GetFirstSelectedTagString(lvPlugins, true);

            if (strPlugin != null && strPlugin.Length > 0)
            {
                PluginInfoView dlg = new PluginInfoView(BusinessLayer, strPlugin);
                dlg.ShowDialog();
            }
        }
    }
}



