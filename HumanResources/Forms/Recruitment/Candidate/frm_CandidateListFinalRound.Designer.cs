namespace HumanResources.Forms.Recruitment.Candidate
{
    partial class frm_CandidateListFinalRound
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.ConditionalFormattingObject conditionalFormattingObject1 = new Telerik.WinControls.UI.ConditionalFormattingObject();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.BS_RecruitmentSeason = new System.Windows.Forms.BindingSource(this.components);
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.BS_CandidateList = new System.Windows.Forms.BindingSource(this.components);
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.rbtPreview = new Telerik.WinControls.UI.RadButton();
            this.rbtPrint = new Telerik.WinControls.UI.RadButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_RecruitmentSeason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_CandidateList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtPrint)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radDropDownList1.AutoSize = false;
            this.radDropDownList1.DataSource = this.BS_RecruitmentSeason;
            this.radDropDownList1.DisplayMember = "Name";
            this.radDropDownList1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.radDropDownList1.Location = new System.Drawing.Point(104, 6);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(298, 25);
            this.radDropDownList1.TabIndex = 2;
            this.radDropDownList1.ValueMember = "SessionId";
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.Location = new System.Drawing.Point(3, 9);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(95, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Đợt tuyển dụng";
            // 
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(0, 42);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowColumnReorder = false;
            this.radGridView1.MasterTemplate.AllowDeleteRow = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            gridViewTextBoxColumn1.FieldName = "RowNumber";
            gridViewTextBoxColumn1.HeaderText = "STT";
            gridViewTextBoxColumn1.MinWidth = 50;
            gridViewTextBoxColumn1.Name = "RowNumber";
            gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn1.Width = 65;
            gridViewTextBoxColumn2.FieldName = "FullName";
            gridViewTextBoxColumn2.HeaderText = "Họ và tên";
            gridViewTextBoxColumn2.MinWidth = 50;
            gridViewTextBoxColumn2.Name = "FullName";
            gridViewTextBoxColumn2.Width = 250;
            gridViewTextBoxColumn3.FieldName = "PositionName";
            gridViewTextBoxColumn3.HeaderText = "Chức danh";
            gridViewTextBoxColumn3.MinWidth = 50;
            gridViewTextBoxColumn3.Name = "PositionName";
            gridViewTextBoxColumn3.Width = 150;
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn1.HeaderText = "Ngày sinh";
            gridViewDateTimeColumn1.MinWidth = 50;
            gridViewDateTimeColumn1.Name = "Birthday";
            gridViewTextBoxColumn4.Expression = "";
            gridViewTextBoxColumn4.FieldName = "Sex";
            gridViewTextBoxColumn4.HeaderText = "Giới tính";
            gridViewTextBoxColumn4.MinWidth = 50;
            gridViewTextBoxColumn4.Name = "Sex";
            gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn4.Width = 85;
            gridViewTextBoxColumn5.FieldName = "RemarkLR";
            gridViewTextBoxColumn5.HeaderText = "Ghi chú";
            gridViewTextBoxColumn5.MinWidth = 50;
            gridViewTextBoxColumn5.Name = "RemarkLR";
            gridViewTextBoxColumn5.Width = 250;
            gridViewTextBoxColumn6.FieldName = "CandidateId";
            gridViewTextBoxColumn6.HeaderText = "column1";
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.MinWidth = 50;
            gridViewTextBoxColumn6.Name = "CandidateId";
            gridViewTextBoxColumn6.Width = 51;
            gridViewTextBoxColumn7.FieldName = "FullName1";
            gridViewTextBoxColumn7.HeaderText = "Ho va ten";
            gridViewTextBoxColumn7.MaxWidth = 1;
            gridViewTextBoxColumn7.MinWidth = 1;
            gridViewTextBoxColumn7.Name = "FullName1";
            gridViewTextBoxColumn7.Width = 1;
            gridViewTextBoxColumn8.FieldName = "SessionId";
            gridViewTextBoxColumn8.HeaderText = "column1";
            gridViewTextBoxColumn8.IsVisible = false;
            gridViewTextBoxColumn8.MinWidth = 50;
            gridViewTextBoxColumn8.Name = "SessionId";
            gridViewTextBoxColumn9.FieldName = "PositionId";
            gridViewTextBoxColumn9.HeaderText = "column1";
            gridViewTextBoxColumn9.IsVisible = false;
            gridViewTextBoxColumn9.MinWidth = 50;
            gridViewTextBoxColumn9.Name = "PositionId";
            conditionalFormattingObject1.ApplyToRow = true;
            conditionalFormattingObject1.CellBackColor = System.Drawing.Color.Empty;
            conditionalFormattingObject1.CellForeColor = System.Drawing.Color.Empty;
            conditionalFormattingObject1.ConditionType = Telerik.WinControls.UI.ConditionTypes.Greater;
            conditionalFormattingObject1.Name = "NewCondition";
            conditionalFormattingObject1.RowBackColor = System.Drawing.Color.Empty;
            conditionalFormattingObject1.RowForeColor = System.Drawing.Color.MidnightBlue;
            conditionalFormattingObject1.TValue1 = "0";
            conditionalFormattingObject1.TValue2 = "0";
            gridViewTextBoxColumn10.ConditionalFormattingObjectList.Add(conditionalFormattingObject1);
            gridViewTextBoxColumn10.FieldName = "CandidateContractId";
            gridViewTextBoxColumn10.HeaderText = "column1";
            gridViewTextBoxColumn10.IsVisible = false;
            gridViewTextBoxColumn10.MinWidth = 50;
            gridViewTextBoxColumn10.Name = "CandidateContractId";
            gridViewTextBoxColumn11.FieldName = "DayOfBirth";
            gridViewTextBoxColumn11.HeaderText = "column1";
            gridViewTextBoxColumn11.IsVisible = false;
            gridViewTextBoxColumn11.Name = "DayOfBirth";
            gridViewTextBoxColumn12.FieldName = "MonthOfBirth";
            gridViewTextBoxColumn12.HeaderText = "column2";
            gridViewTextBoxColumn12.IsVisible = false;
            gridViewTextBoxColumn12.Name = "MonthOfBirth";
            gridViewTextBoxColumn13.FieldName = "YearOfBirth";
            gridViewTextBoxColumn13.HeaderText = "column3";
            gridViewTextBoxColumn13.IsVisible = false;
            gridViewTextBoxColumn13.Name = "YearOfBirth";
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12,
            gridViewTextBoxColumn13});
            this.radGridView1.MasterTemplate.DataSource = this.BS_CandidateList;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.Size = new System.Drawing.Size(1016, 656);
            this.radGridView1.TabIndex = 2;
            this.radGridView1.TabStop = false;
            this.radGridView1.Text = "radGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.rbtPreview);
            this.radPanel2.Controls.Add(this.rbtPrint);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 698);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1016, 40);
            this.radPanel2.TabIndex = 6;
            // 
            // rbtPreview
            // 
            this.rbtPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbtPreview.Location = new System.Drawing.Point(831, 0);
            this.rbtPreview.Name = "rbtPreview";
            this.rbtPreview.Size = new System.Drawing.Size(106, 40);
            this.rbtPreview.TabIndex = 4;
            this.rbtPreview.Text = "<html><strong>Xem HĐ (F4)</strong></html>";
            this.rbtPreview.Click += new System.EventHandler(this.rbtPreview_Click);
            // 
            // rbtPrint
            // 
            this.rbtPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbtPrint.Location = new System.Drawing.Point(937, 0);
            this.rbtPrint.Name = "rbtPrint";
            this.rbtPrint.Size = new System.Drawing.Size(79, 40);
            this.rbtPrint.TabIndex = 3;
            this.rbtPrint.Text = "<html><strong>In (F3)</strong></html>";
            this.rbtPrint.Click += new System.EventHandler(this.rbtPrint_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.radDropDownList1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.radLabel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1016, 37);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // radPanel1
            // 
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 37);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1016, 5);
            this.radPanel1.TabIndex = 8;
            // 
            // frm_CandidateListFinalRound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 738);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.radPanel2);
            this.KeyPreview = true;
            this.Name = "frm_CandidateListFinalRound";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "DANH SÁCH ỨNG VIÊN TRÚNG TUYỂN VÀO VÒNG HỘI ĐỒNG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CandidateListFinalRound_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_RecruitmentSeason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_CandidateList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtPrint)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource BS_RecruitmentSeason;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private System.Windows.Forms.BindingSource BS_CandidateList;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton rbtPreview;
        private Telerik.WinControls.UI.RadButton rbtPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
    }
}
