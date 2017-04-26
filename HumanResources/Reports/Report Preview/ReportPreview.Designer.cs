namespace HumanResources.Reports.Report_Preview
{
    partial class ReportPreview
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.education_Contract1 = new HumanResources.Reports.SGN.Recruitment.Education_Contract();
            this.subcontract_Contract1 = new HumanResources.Reports.SGN.Contract.Subcontract_Contract();
            this.contract_Trial1 = new HumanResources.Reports.SGN.Contract.Contract_Trial();
            this.contract_Under3M1 = new HumanResources.Reports.SGN.Contract.Contract_Under3M();
            this.contract1 = new HumanResources.Reports.SGN.Contract.Contract();
            this.daD_Contract1 = new HumanResources.Reports.DAD.Contract.DAD_Contract();
            this.daD_Contract_Trial1 = new HumanResources.Reports.DAD.Contract.DAD_Contract_Trial();
            this.daD_Contract_Under3M1 = new HumanResources.Reports.DAD.Contract.DAD_Contract_Under3M();
            this.daD_Subcontract_Contract1 = new HumanResources.Reports.DAD.Contract.DAD_Subcontract_Contract();
            this.daD_Education_Contract1 = new HumanResources.Reports.DAD.Recruitment.DAD_Education_Contract();
            this.cxR_Contract1 = new HumanResources.Reports.CXR.Contract.CXR_Contract();
            this.cxR_Contract_Trial1 = new HumanResources.Reports.CXR.Contract.CXR_Contract_Trial();
            this.cxR_Contract_Under3M1 = new HumanResources.Reports.CXR.Contract.CXR_Contract_Under3M();
            this.cxR_Subcontract_Contract1 = new HumanResources.Reports.CXR.Contract.CXR_Subcontract_Contract();
            this.cxR_Education_Contract1 = new HumanResources.Reports.CXR.Recruitment.CXR_Education_Contract();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1016, 738);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // ReportPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 738);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "ReportPreview";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Report Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private SGN.Recruitment.Education_Contract education_Contract1;
        private SGN.Contract.Subcontract_Contract subcontract_Contract1;
        private SGN.Contract.Contract contract1;
        private SGN.Contract.Contract_Trial contract_Trial1;
        private SGN.Contract.Contract_Under3M contract_Under3M1;
        private DAD.Contract.DAD_Contract daD_Contract1;
        private DAD.Contract.DAD_Contract_Trial daD_Contract_Trial1;
        private DAD.Contract.DAD_Contract_Under3M daD_Contract_Under3M1;
        private DAD.Contract.DAD_Subcontract_Contract daD_Subcontract_Contract1;
        private DAD.Recruitment.DAD_Education_Contract daD_Education_Contract1;
        private CXR.Contract.CXR_Contract cxR_Contract1;
        private CXR.Contract.CXR_Contract_Trial cxR_Contract_Trial1;
        private CXR.Contract.CXR_Contract_Under3M cxR_Contract_Under3M1;
        private CXR.Contract.CXR_Subcontract_Contract cxR_Subcontract_Contract1;
        private CXR.Recruitment.CXR_Education_Contract cxR_Education_Contract1;
    }
}
