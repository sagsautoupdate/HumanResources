namespace HumanResources.Forms.Export.Employee
{
    partial class frm_Export_SecurityControl
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition5 = new Telerik.WinControls.UI.TableViewDefinition();
            this.KSAN = new Telerik.WinControls.UI.RadGridView();
            this.radContextMenu1 = new Telerik.WinControls.UI.RadContextMenu(this.components);
            this.radContextMenuManager1 = new Telerik.WinControls.UI.RadContextMenuManager();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.KSAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KSAN.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // KSAN
            // 
            this.KSAN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KSAN.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.KSAN.MasterTemplate.ViewDefinition = tableViewDefinition5;
            this.KSAN.Name = "KSAN";
            this.radContextMenuManager1.SetRadContextMenu(this.KSAN, this.radContextMenu1);
            this.KSAN.Size = new System.Drawing.Size(1016, 738);
            this.KSAN.TabIndex = 0;
            this.KSAN.Text = "radGridView1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frm_Export_SecurityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 738);
            this.Controls.Add(this.KSAN);
            this.Name = "frm_Export_SecurityControl";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách thẻ KSAN";
            this.Load += new System.EventHandler(this.frm_Export_SecurityControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KSAN.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KSAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView KSAN;
        private Telerik.WinControls.UI.RadContextMenuManager radContextMenuManager1;
        private Telerik.WinControls.UI.RadContextMenu radContextMenu1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
