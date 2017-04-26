namespace HumanResources.Forms.Employees
{
    partial class frm_Employee_Working
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
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn1 = new Telerik.WinControls.UI.GridViewImageColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn2 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn3 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Employee_Working));
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.radDock2 = new Telerik.WinControls.UI.Docking.RadDock();
            this.toolWindow2 = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.radTreeView2 = new Telerik.WinControls.UI.RadTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTabStrip2 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.documentContainer3 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip2 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            this.documentWindow2 = new Telerik.WinControls.UI.Docking.DocumentWindow();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radLabelElement1 = new Telerik.WinControls.UI.RadLabelElement();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDock2)).BeginInit();
            this.radDock2.SuspendLayout();
            this.toolWindow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).BeginInit();
            this.toolTabStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer3)).BeginInit();
            this.documentContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip2)).BeginInit();
            this.documentTabStrip2.SuspendLayout();
            this.documentWindow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(0, 0);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowColumnReorder = false;
            this.radGridView1.MasterTemplate.AllowDeleteRow = false;
            this.radGridView1.MasterTemplate.AllowEditRow = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "UserId";
            gridViewTextBoxColumn1.FormatString = "{0:00000#}";
            gridViewTextBoxColumn1.HeaderText = "Mã nhân viên";
            gridViewTextBoxColumn1.IsPinned = true;
            gridViewTextBoxColumn1.MinWidth = 50;
            gridViewTextBoxColumn1.Name = "UserId";
            gridViewTextBoxColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn1.WrapText = true;
            gridViewImageColumn1.EnableExpressionEditor = false;
            gridViewImageColumn1.FieldName = "Picture";
            gridViewImageColumn1.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            gridViewImageColumn1.IsPinned = true;
            gridViewImageColumn1.MaxWidth = 50;
            gridViewImageColumn1.MinWidth = 50;
            gridViewImageColumn1.Name = "Picture";
            gridViewImageColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewImageColumn1.WrapText = true;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "FullName";
            gridViewTextBoxColumn2.HeaderText = "Họ và tên";
            gridViewTextBoxColumn2.IsPinned = true;
            gridViewTextBoxColumn2.MinWidth = 50;
            gridViewTextBoxColumn2.Name = "FullName";
            gridViewTextBoxColumn2.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn2.Width = 80;
            gridViewTextBoxColumn2.WrapText = true;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "Sex";
            gridViewTextBoxColumn3.HeaderText = "Giới tính";
            gridViewTextBoxColumn3.MinWidth = 50;
            gridViewTextBoxColumn3.Name = "Sex";
            gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn3.WrapText = true;
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.FieldName = "Birthday";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn1.HeaderText = "Ngày sinh";
            gridViewDateTimeColumn1.MinWidth = 50;
            gridViewDateTimeColumn1.Name = "Birthday";
            gridViewDateTimeColumn1.WrapText = true;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "PositionName";
            gridViewTextBoxColumn4.HeaderText = "Chức danh";
            gridViewTextBoxColumn4.MinWidth = 50;
            gridViewTextBoxColumn4.Name = "PositionName";
            gridViewTextBoxColumn4.Width = 60;
            gridViewTextBoxColumn4.WrapText = true;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "RootName";
            gridViewTextBoxColumn5.HeaderText = "Phòng";
            gridViewTextBoxColumn5.MinWidth = 50;
            gridViewTextBoxColumn5.Name = "RootName";
            gridViewTextBoxColumn5.Width = 60;
            gridViewTextBoxColumn5.WrapText = true;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "DepartmentName";
            gridViewTextBoxColumn6.HeaderText = "Phòng/ đội";
            gridViewTextBoxColumn6.MinWidth = 50;
            gridViewTextBoxColumn6.Name = "DepartmentName";
            gridViewTextBoxColumn6.Width = 80;
            gridViewTextBoxColumn6.WrapText = true;
            gridViewDateTimeColumn2.EnableExpressionEditor = false;
            gridViewDateTimeColumn2.FieldName = "JoinDate";
            gridViewDateTimeColumn2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn2.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn2.HeaderText = "Ngày vào ngành hàng không";
            gridViewDateTimeColumn2.MaxWidth = 85;
            gridViewDateTimeColumn2.MinWidth = 50;
            gridViewDateTimeColumn2.Name = "JoinDate";
            gridViewDateTimeColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewDateTimeColumn2.WrapText = true;
            gridViewDateTimeColumn3.EnableExpressionEditor = false;
            gridViewDateTimeColumn3.FieldName = "JoinCompanyDate";
            gridViewDateTimeColumn3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn3.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn3.HeaderText = "Ngày vào công ty";
            gridViewDateTimeColumn3.MaxWidth = 85;
            gridViewDateTimeColumn3.MinWidth = 50;
            gridViewDateTimeColumn3.Name = "JoinCompanyDate";
            gridViewDateTimeColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewDateTimeColumn3.WrapText = true;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "HighestLevelName";
            gridViewTextBoxColumn7.HeaderText = "Trình độ cao nhất";
            gridViewTextBoxColumn7.MinWidth = 50;
            gridViewTextBoxColumn7.Name = "HighestLevelName";
            gridViewTextBoxColumn7.WrapText = true;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "HighestLevelNameValue";
            gridViewTextBoxColumn8.HeaderText = "Giá trị";
            gridViewTextBoxColumn8.MinWidth = 50;
            gridViewTextBoxColumn8.Name = "HighestLevelNameValue";
            gridViewTextBoxColumn8.WrapText = true;
            gridViewTextBoxColumn9.EnableExpressionEditor = false;
            gridViewTextBoxColumn9.FieldName = "FullName2";
            gridViewTextBoxColumn9.HeaderText = "Ho va ten";
            gridViewTextBoxColumn9.MaxWidth = 1;
            gridViewTextBoxColumn9.MinWidth = 1;
            gridViewTextBoxColumn9.Name = "FullName2";
            gridViewTextBoxColumn9.Width = 1;
            gridViewTextBoxColumn9.WrapText = true;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewImageColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewDateTimeColumn2,
            gridViewDateTimeColumn3,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9});
            this.radGridView1.MasterTemplate.EnableFiltering = true;
            this.radGridView1.MasterTemplate.ShowFilterCellOperatorText = false;
            this.radGridView1.MasterTemplate.ShowHeaderCellButtons = true;
            this.radGridView1.MasterTemplate.ShowTotals = true;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell;
            this.radGridView1.ReadOnly = true;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.ShowHeaderCellButtons = true;
            this.radGridView1.Size = new System.Drawing.Size(1260, 973);
            this.radGridView1.TabIndex = 2;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.RadGridView1_RowFormatting);
            this.radGridView1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.RadGridView1_CellFormatting);
            this.radGridView1.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridView1_CellDoubleClick);
            // 
            // radDock2
            // 
            this.radDock2.ActiveWindow = this.documentWindow2;
            this.radDock2.Controls.Add(this.toolTabStrip2);
            this.radDock2.Controls.Add(this.documentContainer3);
            this.radDock2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDock2.IsCleanUpTarget = true;
            this.radDock2.Location = new System.Drawing.Point(0, 0);
            this.radDock2.MainDocumentContainer = this.documentContainer3;
            this.radDock2.Name = "radDock2";
            this.radDock2.Padding = new System.Windows.Forms.Padding(8);
            // 
            // 
            // 
            this.radDock2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radDock2.Size = new System.Drawing.Size(1530, 1050);
            this.radDock2.TabIndex = 3;
            this.radDock2.TabStop = false;
            this.radDock2.Text = "radDock2";
            // 
            // toolWindow2
            // 
            this.toolWindow2.Caption = null;
            this.toolWindow2.Controls.Add(this.radTreeView2);
            this.toolWindow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolWindow2.Location = new System.Drawing.Point(1, 24);
            this.toolWindow2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolWindow2.Name = "toolWindow2";
            this.toolWindow2.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.toolWindow2.Size = new System.Drawing.Size(236, 1008);
            this.toolWindow2.Text = "Danh sách Phòng ban";
            // 
            // radTreeView2
            // 
            this.radTreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radTreeView2.ImageList = this.imageList1;
            this.radTreeView2.Location = new System.Drawing.Point(0, 0);
            this.radTreeView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radTreeView2.Name = "radTreeView2";
            this.radTreeView2.Size = new System.Drawing.Size(236, 1008);
            this.radTreeView2.SpacingBetweenNodes = -1;
            this.radTreeView2.TabIndex = 2;
            this.radTreeView2.Text = "radTreeView2";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "home.png");
            this.imageList1.Images.SetKeyName(1, "group.png");
            // 
            // toolTabStrip2
            // 
            this.toolTabStrip2.CanUpdateChildIndex = true;
            this.toolTabStrip2.Controls.Add(this.toolWindow2);
            this.toolTabStrip2.Location = new System.Drawing.Point(8, 8);
            this.toolTabStrip2.Name = "toolTabStrip2";
            // 
            // 
            // 
            this.toolTabStrip2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.toolTabStrip2.SelectedIndex = 0;
            this.toolTabStrip2.Size = new System.Drawing.Size(238, 1034);
            this.toolTabStrip2.SizeInfo.AbsoluteSize = new System.Drawing.Size(238, 200);
            this.toolTabStrip2.SizeInfo.SplitterCorrection = new System.Drawing.Size(38, 0);
            this.toolTabStrip2.TabIndex = 1;
            this.toolTabStrip2.TabStop = false;
            ((Telerik.WinControls.UI.Docking.ToolWindowCaptionElement)(this.toolTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // documentContainer3
            // 
            this.documentContainer3.Controls.Add(this.documentTabStrip2);
            this.documentContainer3.Name = "documentContainer3";
            this.documentContainer3.Padding = new System.Windows.Forms.Padding(8);
            // 
            // 
            // 
            this.documentContainer3.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentContainer3.SizeInfo.AbsoluteSize = new System.Drawing.Size(772, 200);
            this.documentContainer3.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainer3.SizeInfo.SplitterCorrection = new System.Drawing.Size(-38, 0);
            this.documentContainer3.TabIndex = 2;
            // 
            // documentTabStrip2
            // 
            this.documentTabStrip2.CanUpdateChildIndex = true;
            this.documentTabStrip2.Controls.Add(this.documentWindow2);
            this.documentTabStrip2.Location = new System.Drawing.Point(0, 0);
            this.documentTabStrip2.Name = "documentTabStrip2";
            // 
            // 
            // 
            this.documentTabStrip2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentTabStrip2.SelectedIndex = 0;
            this.documentTabStrip2.Size = new System.Drawing.Size(1272, 1034);
            this.documentTabStrip2.TabIndex = 0;
            this.documentTabStrip2.TabStop = false;
            ((Telerik.WinControls.UI.SplitPanelElement)(this.documentTabStrip2.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPageViewTabStripElement)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2))).ItemContentOrientation = Telerik.WinControls.UI.PageViewContentOrientation.Horizontal;
            ((Telerik.WinControls.UI.RadPageViewTabStripElement)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2))).Text = "";
            ((Telerik.WinControls.UI.StripViewItemContainer)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.StripViewItemLayout)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.TabStripItem)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).IsPinned = false;
            ((Telerik.WinControls.UI.TabStripItem)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "DANH SÁCH NHÂN VIÊN ĐANG LÀM VIỆC";
            ((Telerik.WinControls.UI.RadPageViewLabelElement)(this.documentTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(3))).Text = "DANH SÁCH NHÂN VIÊN ĐANG LÀM VIỆC";
            // 
            // documentWindow2
            // 
            this.documentWindow2.Controls.Add(this.radGridView1);
            this.documentWindow2.Controls.Add(this.radStatusStrip1);
            this.documentWindow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentWindow2.Location = new System.Drawing.Point(6, 29);
            this.documentWindow2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.documentWindow2.Name = "documentWindow2";
            this.documentWindow2.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.TabbedDocument;
            this.documentWindow2.Size = new System.Drawing.Size(1260, 999);
            this.documentWindow2.Text = "documentWindow2";
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radLabelElement1});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 973);
            this.radStatusStrip1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(1260, 26);
            this.radStatusStrip1.TabIndex = 3;
            this.radStatusStrip1.Text = "radStatusStrip1";
            // 
            // radLabelElement1
            // 
            this.radLabelElement1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.radLabelElement1.Name = "radLabelElement1";
            this.radStatusStrip1.SetSpring(this.radLabelElement1, true);
            this.radLabelElement1.Text = "";
            this.radLabelElement1.TextWrap = true;
            // 
            // frm_Employee_Working
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1530, 1050);
            this.Controls.Add(this.radDock2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_Employee_Working";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "DANH SÁCH NHÂN VIÊN ĐANG LÀM VIỆC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Employee_Working_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDock2)).EndInit();
            this.radDock2.ResumeLayout(false);
            this.toolWindow2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).EndInit();
            this.toolTabStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer3)).EndInit();
            this.documentContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip2)).EndInit();
            this.documentTabStrip2.ResumeLayout(false);
            this.documentWindow2.ResumeLayout(false);
            this.documentWindow2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Telerik.WinControls.UI.Docking.RadDock radDock2;
        private Telerik.WinControls.UI.Docking.DocumentWindow documentWindow2;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.Docking.ToolTabStrip toolTabStrip2;
        private Telerik.WinControls.UI.Docking.ToolWindow toolWindow2;
        private Telerik.WinControls.UI.RadTreeView radTreeView2;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer3;
        private Telerik.WinControls.UI.Docking.DocumentTabStrip documentTabStrip2;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadLabelElement radLabelElement1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
