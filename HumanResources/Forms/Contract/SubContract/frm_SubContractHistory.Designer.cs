namespace HumanResources.Forms.Contract.SubContract
{
    partial class frm_SubContractHistory
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
            Telerik.WinControls.Data.FilterDescriptor filterDescriptor1 = new Telerik.WinControls.Data.FilterDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.rmbEmployees = new Telerik.WinControls.UI.RadMultiColumnComboBox();
            this.BS_Emp = new System.Windows.Forms.BindingSource(this.components);
            this.rbtAdd = new Telerik.WinControls.UI.RadButton();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.BS_EmpSubContract = new System.Windows.Forms.BindingSource(this.components);
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.rbtSavetvtg = new Telerik.WinControls.UI.RadButton();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.BS_Position = new System.Windows.Forms.BindingSource(this.components);
            this.BS_Scale = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rmbEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmbEmployees.EditorControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmbEmployees.EditorControl.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Emp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_EmpSubContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtSavetvtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Position)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rmbEmployees
            // 
            this.rmbEmployees.AutoFilter = true;
            this.rmbEmployees.DataSource = this.BS_Emp;
            this.rmbEmployees.DisplayMember = "FullName";
            this.rmbEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rmbEmployees.DropDownSizingMode = ((Telerik.WinControls.UI.SizingMode)((Telerik.WinControls.UI.SizingMode.RightBottom | Telerik.WinControls.UI.SizingMode.UpDown)));
            // 
            // rmbEmployees.NestedRadGridView
            // 
            this.rmbEmployees.EditorControl.BackColor = System.Drawing.SystemColors.Window;
            this.rmbEmployees.EditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rmbEmployees.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rmbEmployees.EditorControl.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.rmbEmployees.EditorControl.MasterTemplate.AllowAddNewRow = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowCellContextMenu = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowColumnChooser = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowColumnReorder = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowDeleteRow = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowDragToGroup = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AllowEditRow = false;
            this.rmbEmployees.EditorControl.MasterTemplate.AutoGenerateColumns = false;
            gridViewTextBoxColumn1.FieldName = "UserId";
            gridViewTextBoxColumn1.FormatString = "{0:00000#}";
            gridViewTextBoxColumn1.HeaderText = "Mã nhân viên";
            gridViewTextBoxColumn1.MinWidth = 85;
            gridViewTextBoxColumn1.Name = "UserId";
            gridViewTextBoxColumn1.Width = 85;
            gridViewTextBoxColumn1.WrapText = true;
            gridViewTextBoxColumn2.FieldName = "FullName";
            gridViewTextBoxColumn2.HeaderText = "Họ và tên";
            gridViewTextBoxColumn2.MinWidth = 150;
            gridViewTextBoxColumn2.Name = "FullName";
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldName = "FullName2";
            gridViewTextBoxColumn3.HeaderText = "Ho va ten";
            gridViewTextBoxColumn3.MinWidth = 150;
            gridViewTextBoxColumn3.Name = "FullName2";
            gridViewTextBoxColumn3.Width = 150;
            this.rmbEmployees.EditorControl.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3});
            this.rmbEmployees.EditorControl.MasterTemplate.DataSource = this.BS_Emp;
            this.rmbEmployees.EditorControl.MasterTemplate.EnableFiltering = true;
            this.rmbEmployees.EditorControl.MasterTemplate.EnableGrouping = false;
            filterDescriptor1.PropertyName = "FullName2";
            this.rmbEmployees.EditorControl.MasterTemplate.FilterDescriptors.AddRange(new Telerik.WinControls.Data.FilterDescriptor[] {
            filterDescriptor1});
            this.rmbEmployees.EditorControl.MasterTemplate.ShowFilteringRow = false;
            this.rmbEmployees.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rmbEmployees.EditorControl.Name = "NestedRadGridView";
            this.rmbEmployees.EditorControl.ReadOnly = true;
            this.rmbEmployees.EditorControl.ShowGroupPanel = false;
            this.rmbEmployees.EditorControl.Size = new System.Drawing.Size(2, 3);
            this.rmbEmployees.EditorControl.TabIndex = 0;
            this.rmbEmployees.Location = new System.Drawing.Point(84, 3);
            this.rmbEmployees.Name = "rmbEmployees";
            this.rmbEmployees.Size = new System.Drawing.Size(248, 29);
            this.rmbEmployees.TabIndex = 13;
            this.rmbEmployees.TabStop = false;
            this.rmbEmployees.ValueMember = "UserId";
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.rmbEmployees.GetChildAt(0).GetChildAt(2).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.rmbEmployees.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Padding = new System.Windows.Forms.Padding(4, 2, 2, 1);
            // 
            // rbtAdd
            // 
            this.rbtAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbtAdd.Location = new System.Drawing.Point(241, 38);
            this.rbtAdd.Name = "rbtAdd";
            this.rbtAdd.Size = new System.Drawing.Size(91, 30);
            this.rbtAdd.TabIndex = 12;
            this.rbtAdd.Text = "<html><strong>Xem</strong></html>";
            this.rbtAdd.Click += new System.EventHandler(this.rbtAdd_Click);
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radGridView1);
            this.radPanel2.Controls.Add(this.radPanel3);
            this.radPanel2.Controls.Add(this.radPanel1);
            this.radPanel2.Controls.Add(this.tableLayoutPanel1);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1016, 738);
            this.radPanel2.TabIndex = 2;
            // 
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(0, 76);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowColumnReorder = false;
            this.radGridView1.MasterTemplate.AllowDeleteRow = false;
            this.radGridView1.MasterTemplate.AllowDragToGroup = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "Active";
            gridViewCheckBoxColumn1.IsPinned = true;
            gridViewCheckBoxColumn1.MinWidth = 15;
            gridViewCheckBoxColumn1.Name = "Active";
            gridViewCheckBoxColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewCheckBoxColumn1.Width = 33;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "UserId";
            gridViewTextBoxColumn4.FormatString = "{0:00000#}";
            gridViewTextBoxColumn4.HeaderText = "Mã nhân viên";
            gridViewTextBoxColumn4.IsPinned = true;
            gridViewTextBoxColumn4.MinWidth = 85;
            gridViewTextBoxColumn4.Name = "UserId";
            gridViewTextBoxColumn4.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn4.Width = 99;
            gridViewTextBoxColumn4.WrapText = true;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "FullName";
            gridViewTextBoxColumn5.HeaderText = "Họ và tên";
            gridViewTextBoxColumn5.IsPinned = true;
            gridViewTextBoxColumn5.MinWidth = 200;
            gridViewTextBoxColumn5.Name = "FullName";
            gridViewTextBoxColumn5.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 232;
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.FieldName = "FromDate";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn1.HeaderText = "Ngày bắt đầu";
            gridViewDateTimeColumn1.MinWidth = 85;
            gridViewDateTimeColumn1.Name = "FromDate";
            gridViewDateTimeColumn1.ReadOnly = true;
            gridViewDateTimeColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewDateTimeColumn1.Width = 99;
            gridViewDateTimeColumn1.WrapText = true;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "PositionName";
            gridViewTextBoxColumn6.HeaderText = "Vị trí làm việc";
            gridViewTextBoxColumn6.MinWidth = 120;
            gridViewTextBoxColumn6.Name = "PositionName";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.Width = 139;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "ScaleOfSalaryName";
            gridViewTextBoxColumn7.HeaderText = "Chức danh hợp đồng";
            gridViewTextBoxColumn7.MinWidth = 120;
            gridViewTextBoxColumn7.Name = "ScaleOfSalaryName";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.Width = 139;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "Value";
            gridViewTextBoxColumn8.HeaderText = "Mức";
            gridViewTextBoxColumn8.MinWidth = 50;
            gridViewTextBoxColumn8.Name = "SalaryLevel";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewTextBoxColumn8.Width = 58;
            gridViewTextBoxColumn9.EnableExpressionEditor = false;
            gridViewTextBoxColumn9.FieldName = "SalaryValue";
            gridViewTextBoxColumn9.FormatString = "{0:#,###0}";
            gridViewTextBoxColumn9.HeaderText = "Giá trị";
            gridViewTextBoxColumn9.MinWidth = 75;
            gridViewTextBoxColumn9.Name = "SalaryValue";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewTextBoxColumn9.Width = 87;
            gridViewTextBoxColumn10.EnableExpressionEditor = false;
            gridViewTextBoxColumn10.FieldName = "EmployeeSubContractId";
            gridViewTextBoxColumn10.HeaderText = "column1";
            gridViewTextBoxColumn10.IsVisible = false;
            gridViewTextBoxColumn10.Name = "EmployeeSubContractId";
            gridViewTextBoxColumn10.Width = 31;
            gridViewTextBoxColumn11.EnableExpressionEditor = false;
            gridViewTextBoxColumn11.FieldName = "FullName2";
            gridViewTextBoxColumn11.HeaderText = "Ho va ten";
            gridViewTextBoxColumn11.MinWidth = 100;
            gridViewTextBoxColumn11.Name = "FullName2";
            gridViewTextBoxColumn11.ReadOnly = true;
            gridViewTextBoxColumn11.Width = 117;
            gridViewTextBoxColumn12.EnableExpressionEditor = false;
            gridViewTextBoxColumn12.FieldName = "EmployeeContractId";
            gridViewTextBoxColumn12.HeaderText = "column1";
            gridViewTextBoxColumn12.IsVisible = false;
            gridViewTextBoxColumn12.Name = "EmployeeContractId";
            gridViewTextBoxColumn12.Width = 41;
            gridViewTextBoxColumn13.EnableExpressionEditor = false;
            gridViewTextBoxColumn13.FieldName = "FullName";
            gridViewTextBoxColumn13.HeaderText = "column1";
            gridViewTextBoxColumn13.IsVisible = false;
            gridViewTextBoxColumn13.Name = "FullNam";
            gridViewTextBoxColumn13.Width = 44;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12,
            gridViewTextBoxColumn13});
            this.radGridView1.MasterTemplate.DataSource = this.BS_EmpSubContract;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.ShowGroupPanelScrollbars = false;
            this.radGridView1.Size = new System.Drawing.Size(1016, 632);
            this.radGridView1.TabIndex = 5;
            this.radGridView1.Text = "radGridView1";
            // 
            // radPanel3
            // 
            this.radPanel3.Controls.Add(this.rbtSavetvtg);
            this.radPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel3.Location = new System.Drawing.Point(0, 708);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(1016, 30);
            this.radPanel3.TabIndex = 8;
            // 
            // rbtSavetvtg
            // 
            this.rbtSavetvtg.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbtSavetvtg.Location = new System.Drawing.Point(937, 0);
            this.rbtSavetvtg.Name = "rbtSavetvtg";
            this.rbtSavetvtg.Size = new System.Drawing.Size(79, 30);
            this.rbtSavetvtg.TabIndex = 0;
            this.rbtSavetvtg.Text = "<html><strong>Lưu (F2)</strong></html>";
            this.rbtSavetvtg.Click += new System.EventHandler(this.rbtSavetvtg_Click);
            // 
            // radPanel1
            // 
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 71);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1016, 5);
            this.radPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanel1.Controls.Add(this.rmbEmployees, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.radLabel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rbtAdd, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1016, 71);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // radLabel5
            // 
            this.radLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radLabel5.Location = new System.Drawing.Point(3, 8);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(54, 18);
            this.radLabel5.TabIndex = 1;
            this.radLabel5.Text = "Họ và tên";
            // 
            // frm_SubContractHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 738);
            this.Controls.Add(this.radPanel2);
            this.KeyPreview = true;
            this.Name = "frm_SubContractHistory";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubContractHistory";
            this.Load += new System.EventHandler(this.SubContractHistory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_SubContractHistory_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.rmbEmployees.EditorControl.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmbEmployees.EditorControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmbEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Emp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_EmpSubContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtSavetvtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Position)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Telerik.WinControls.UI.RadMultiColumnComboBox rmbEmployees;
        private Telerik.WinControls.UI.RadButton rbtAdd;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private System.Windows.Forms.BindingSource BS_Emp;
        private System.Windows.Forms.BindingSource BS_EmpSubContract;
        private System.Windows.Forms.BindingSource BS_Position;
        private System.Windows.Forms.BindingSource BS_Scale;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadButton rbtSavetvtg;
    }
}
