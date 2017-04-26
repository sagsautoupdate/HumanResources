namespace HumanResources.Forms.Bonus
{
    partial class frm_ImportBonus
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition5 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.rddlBonusTitle = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.rDTPPayDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.rDTPBonusYear = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.rbtnImport = new Telerik.WinControls.UI.RadButton();
            this.rbtnBrowse = new Telerik.WinControls.UI.RadButton();
            this.rtxtFileName = new Telerik.WinControls.UI.RadTextBox();
            this.rGVData = new Telerik.WinControls.UI.RadGridView();
            this.progressBarImport = new Telerik.WinControls.UI.RadProgressBar();
            this.backgroundWorkerImport = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rddlBonusTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDTPPayDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDTPBonusYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtFileName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rGVData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rGVData.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.rddlBonusTitle);
            this.radGroupBox1.Controls.Add(this.radLabel4);
            this.radGroupBox1.Controls.Add(this.rDTPPayDate);
            this.radGroupBox1.Controls.Add(this.radLabel3);
            this.radGroupBox1.Controls.Add(this.rDTPBonusYear);
            this.radGroupBox1.Controls.Add(this.radLabel2);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.Controls.Add(this.rbtnImport);
            this.radGroupBox1.Controls.Add(this.rbtnBrowse);
            this.radGroupBox1.Controls.Add(this.rtxtFileName);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radGroupBox1.HeaderText = "Dữ Liệu File Nguồn (Excel)";
            this.radGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(792, 115);
            this.radGroupBox1.TabIndex = 13;
            this.radGroupBox1.Text = "Dữ Liệu File Nguồn (Excel)";
            // 
            // rddlBonusTitle
            // 
            this.rddlBonusTitle.DropDownSizingMode = ((Telerik.WinControls.UI.SizingMode)((Telerik.WinControls.UI.SizingMode.RightBottom | Telerik.WinControls.UI.SizingMode.UpDown)));
            this.rddlBonusTitle.Location = new System.Drawing.Point(474, 53);
            this.rddlBonusTitle.Name = "rddlBonusTitle";
            this.rddlBonusTitle.Size = new System.Drawing.Size(163, 20);
            this.rddlBonusTitle.TabIndex = 10;
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(379, 53);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(89, 18);
            this.radLabel4.TabIndex = 6;
            this.radLabel4.Text = "Loại tiền thưởng";
            // 
            // rDTPPayDate
            // 
            this.rDTPPayDate.CustomFormat = "dd/MM/yyyy";
            this.rDTPPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.rDTPPayDate.Location = new System.Drawing.Point(270, 51);
            this.rDTPPayDate.Name = "rDTPPayDate";
            this.rDTPPayDate.Size = new System.Drawing.Size(103, 20);
            this.rDTPPayDate.TabIndex = 6;
            this.rDTPPayDate.TabStop = false;
            this.rDTPPayDate.Text = "25/02/2014";
            this.rDTPPayDate.Value = new System.DateTime(2014, 2, 25, 16, 30, 59, 265);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(192, 52);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(75, 18);
            this.radLabel3.TabIndex = 5;
            this.radLabel3.Text = "Ngày thực chi";
            // 
            // rDTPBonusYear
            // 
            this.rDTPBonusYear.CustomFormat = "yyyy";
            this.rDTPBonusYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.rDTPBonusYear.Location = new System.Drawing.Point(121, 51);
            this.rDTPBonusYear.Name = "rDTPBonusYear";
            this.rDTPBonusYear.Size = new System.Drawing.Size(65, 20);
            this.rDTPBonusYear.TabIndex = 5;
            this.rDTPBonusYear.TabStop = false;
            this.rDTPBonusYear.Text = "2014";
            this.rDTPBonusYear.Value = new System.DateTime(2014, 2, 25, 16, 30, 59, 265);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(27, 52);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(91, 18);
            this.radLabel2.TabIndex = 4;
            this.radLabel2.Text = "Thưởng của năm";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(15, 27);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(100, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Đường dẫn lưu file";
            // 
            // rbtnImport
            // 
            this.rbtnImport.Location = new System.Drawing.Point(341, 80);
            this.rbtnImport.Name = "rbtnImport";
            this.rbtnImport.Size = new System.Drawing.Size(110, 24);
            this.rbtnImport.TabIndex = 2;
            this.rbtnImport.Text = "Import";
            this.rbtnImport.Click += new System.EventHandler(this.rbtnImport_Click);
            // 
            // rbtnBrowse
            // 
            this.rbtnBrowse.Location = new System.Drawing.Point(643, 24);
            this.rbtnBrowse.Name = "rbtnBrowse";
            this.rbtnBrowse.Size = new System.Drawing.Size(110, 24);
            this.rbtnBrowse.TabIndex = 3;
            this.rbtnBrowse.Text = "Chọn đường dẫn";
            this.rbtnBrowse.Click += new System.EventHandler(this.rbtnBrowse_Click);
            // 
            // rtxtFileName
            // 
            this.rtxtFileName.Location = new System.Drawing.Point(121, 26);
            this.rtxtFileName.Name = "rtxtFileName";
            this.rtxtFileName.Size = new System.Drawing.Size(516, 20);
            this.rtxtFileName.TabIndex = 0;
            // 
            // rGVData
            // 
            this.rGVData.AutoSizeRows = true;
            this.rGVData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rGVData.Location = new System.Drawing.Point(0, 115);
            // 
            // 
            // 
            this.rGVData.MasterTemplate.AllowAddNewRow = false;
            this.rGVData.MasterTemplate.AllowDeleteRow = false;
            this.rGVData.MasterTemplate.AllowDragToGroup = false;
            this.rGVData.MasterTemplate.AllowEditRow = false;
            this.rGVData.MasterTemplate.ShowRowHeaderColumn = false;
            this.rGVData.MasterTemplate.ViewDefinition = tableViewDefinition5;
            this.rGVData.Name = "rGVData";
            this.rGVData.ReadOnly = true;
            this.rGVData.ShowGroupPanel = false;
            this.rGVData.Size = new System.Drawing.Size(792, 422);
            this.rGVData.TabIndex = 14;
            // 
            // progressBarImport
            // 
            this.progressBarImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarImport.Location = new System.Drawing.Point(0, 537);
            this.progressBarImport.Name = "progressBarImport";
            this.progressBarImport.Size = new System.Drawing.Size(792, 33);
            this.progressBarImport.TabIndex = 15;
            this.progressBarImport.Text = ".........";
            // 
            // backgroundWorkerImport
            // 
            this.backgroundWorkerImport.WorkerReportsProgress = true;
            this.backgroundWorkerImport.WorkerSupportsCancellation = true;
            this.backgroundWorkerImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerImport_DoWork);
            this.backgroundWorkerImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerImport_ProgressChanged);
            this.backgroundWorkerImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerImport_RunWorkerCompleted);
            // 
            // frm_ImportBonus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.rGVData);
            this.Controls.Add(this.progressBarImport);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "frm_ImportBonus";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Bảng thưởng";
            this.Load += new System.EventHandler(this.frm_ImportBonus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rddlBonusTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDTPPayDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDTPBonusYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtFileName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rGVData.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rGVData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadDropDownList rddlBonusTitle;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadDateTimePicker rDTPPayDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadDateTimePicker rDTPBonusYear;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton rbtnImport;
        private Telerik.WinControls.UI.RadButton rbtnBrowse;
        private Telerik.WinControls.UI.RadTextBox rtxtFileName;
        private Telerik.WinControls.UI.RadGridView rGVData;
        private Telerik.WinControls.UI.RadProgressBar progressBarImport;
        private System.ComponentModel.BackgroundWorker backgroundWorkerImport;
    }
}
