namespace HumanResources.Forms.Regulation
{
    partial class frm_Party_Management
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Party_Management));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn2 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.rtrvDepartment = new Telerik.WinControls.UI.RadTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.BS_Employees = new System.Windows.Forms.BindingSource(this.components);
            this.radDock2 = new Telerik.WinControls.UI.Docking.RadDock();
            this.documentWindow2 = new Telerik.WinControls.UI.Docking.DocumentWindow();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radLabelElement1 = new Telerik.WinControls.UI.RadLabelElement();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.toolTabStrip2 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.toolWindow2 = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.documentContainer3 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip2 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            ((System.ComponentModel.ISupportInitialize)(this.rtrvDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Employees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDock2)).BeginInit();
            this.radDock2.SuspendLayout();
            this.documentWindow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).BeginInit();
            this.toolTabStrip2.SuspendLayout();
            this.toolWindow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer3)).BeginInit();
            this.documentContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip2)).BeginInit();
            this.documentTabStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rtrvDepartment
            // 
            this.rtrvDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtrvDepartment.ImageList = this.imageList1;
            this.rtrvDepartment.Location = new System.Drawing.Point(0, 0);
            this.rtrvDepartment.Name = "rtrvDepartment";
            this.rtrvDepartment.ShowLines = true;
            this.rtrvDepartment.ShowNodeToolTips = true;
            this.rtrvDepartment.Size = new System.Drawing.Size(236, 696);
            this.rtrvDepartment.SpacingBetweenNodes = -1;
            this.rtrvDepartment.TabIndex = 0;
            this.rtrvDepartment.Text = "radTreeView1";
            this.rtrvDepartment.SelectedNodeChanged += new Telerik.WinControls.UI.RadTreeView.RadTreeViewEventHandler(this.RtrvDepartment_SelectedNodeChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "home.png");
            this.imageList1.Images.SetKeyName(1, "group.png");
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Green;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 738);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // radDock2
            // 
            this.radDock2.ActiveWindow = this.toolWindow2;
            this.radDock2.Controls.Add(this.toolTabStrip2);
            this.radDock2.Controls.Add(this.documentContainer3);
            this.radDock2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDock2.IsCleanUpTarget = true;
            this.radDock2.Location = new System.Drawing.Point(3, 0);
            this.radDock2.MainDocumentContainer = this.documentContainer3;
            this.radDock2.Name = "radDock2";
            this.radDock2.Padding = new System.Windows.Forms.Padding(8);
            // 
            // 
            // 
            this.radDock2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radDock2.Size = new System.Drawing.Size(1013, 738);
            this.radDock2.TabIndex = 4;
            this.radDock2.TabStop = false;
            this.radDock2.Text = "radDock2";
            // 
            // documentWindow2
            // 
            this.documentWindow2.Controls.Add(this.radStatusStrip1);
            this.documentWindow2.Controls.Add(this.radGridView1);
            this.documentWindow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentWindow2.Location = new System.Drawing.Point(6, 29);
            this.documentWindow2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.documentWindow2.Name = "documentWindow2";
            this.documentWindow2.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.TabbedDocument;
            this.documentWindow2.Size = new System.Drawing.Size(743, 687);
            this.documentWindow2.Text = "documentWindow2";
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radLabelElement1});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 661);
            this.radStatusStrip1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(743, 26);
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
            // radGridView1
            // 
            this.radGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView1.ForeColor = System.Drawing.Color.Black;
            this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView1.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowColumnReorder = false;
            this.radGridView1.MasterTemplate.AllowDeleteRow = false;
            this.radGridView1.MasterTemplate.AllowSearchRow = true;
            this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "UserId";
            gridViewTextBoxColumn1.FormatString = "{0:00000#}";
            gridViewTextBoxColumn1.HeaderText = "Mã nhân viên";
            gridViewTextBoxColumn1.IsPinned = true;
            gridViewTextBoxColumn1.MinWidth = 85;
            gridViewTextBoxColumn1.Name = "UserId";
            gridViewTextBoxColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn1.Width = 85;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "FullName";
            gridViewTextBoxColumn2.HeaderText = "Họ và tên";
            gridViewTextBoxColumn2.IsPinned = true;
            gridViewTextBoxColumn2.MinWidth = 200;
            gridViewTextBoxColumn2.Name = "FullName";
            gridViewTextBoxColumn2.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn2.Width = 200;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "PositionName";
            gridViewTextBoxColumn3.HeaderText = "Chức danh";
            gridViewTextBoxColumn3.IsPinned = true;
            gridViewTextBoxColumn3.MinWidth = 150;
            gridViewTextBoxColumn3.Name = "PositionName";
            gridViewTextBoxColumn3.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn3.Width = 150;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "PartyNumber";
            gridViewTextBoxColumn4.HeaderText = "Số sổ Đảng";
            gridViewTextBoxColumn4.MinWidth = 85;
            gridViewTextBoxColumn4.Name = "PartyNumber";
            gridViewTextBoxColumn4.Width = 85;
            gridViewTextBoxColumn4.WrapText = true;
            gridViewDateTimeColumn1.FieldName = "DateJoinParty";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn1.HeaderText = "Ngày vào Đảng";
            gridViewDateTimeColumn1.MinWidth = 85;
            gridViewDateTimeColumn1.Name = "DateJoinParty";
            gridViewDateTimeColumn1.Width = 85;
            gridViewDateTimeColumn1.WrapText = true;
            gridViewDateTimeColumn2.FieldName = "DateJoinPartyOfficial";
            gridViewDateTimeColumn2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn2.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn2.HeaderText = "Ngày vào Đảng chính thức";
            gridViewDateTimeColumn2.MinWidth = 85;
            gridViewDateTimeColumn2.Name = "DateJoinPartyOfficial";
            gridViewDateTimeColumn2.Width = 141;
            gridViewDateTimeColumn2.WrapText = true;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "PlaceJoinParty";
            gridViewTextBoxColumn5.HeaderText = "Nơi vào Đảng";
            gridViewTextBoxColumn5.MinWidth = 200;
            gridViewTextBoxColumn5.Name = "PlaceJoinParty";
            gridViewTextBoxColumn5.Width = 200;
            gridViewTextBoxColumn5.WrapText = true;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "FullName2";
            gridViewTextBoxColumn6.HeaderText = "Ho va ten";
            gridViewTextBoxColumn6.MinWidth = 200;
            gridViewTextBoxColumn6.Name = "FullName2";
            gridViewTextBoxColumn6.Width = 200;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewDateTimeColumn1,
            gridViewDateTimeColumn2,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.radGridView1.MasterTemplate.DataSource = this.BS_Employees;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell;
            this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.Size = new System.Drawing.Size(743, 687);
            this.radGridView1.TabIndex = 0;
            this.radGridView1.Text = "radGridView1";
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
            this.toolTabStrip2.Size = new System.Drawing.Size(238, 722);
            this.toolTabStrip2.SizeInfo.AbsoluteSize = new System.Drawing.Size(238, 200);
            this.toolTabStrip2.SizeInfo.SplitterCorrection = new System.Drawing.Size(38, 0);
            this.toolTabStrip2.TabIndex = 1;
            this.toolTabStrip2.TabStop = false;
            ((Telerik.WinControls.UI.Docking.ToolWindowCaptionElement)(this.toolTabStrip2.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // toolWindow2
            // 
            this.toolWindow2.Caption = null;
            this.toolWindow2.Controls.Add(this.rtrvDepartment);
            this.toolWindow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolWindow2.Location = new System.Drawing.Point(1, 24);
            this.toolWindow2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolWindow2.Name = "toolWindow2";
            this.toolWindow2.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.toolWindow2.Size = new System.Drawing.Size(236, 696);
            this.toolWindow2.Text = "Danh sách Phòng ban";
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
            this.documentTabStrip2.Size = new System.Drawing.Size(755, 722);
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
            // frm_Party_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 738);
            this.Controls.Add(this.radDock2);
            this.Controls.Add(this.splitter1);
            this.Name = "frm_Party_Management";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DANH SÁCH ĐẢNG VIÊN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Party_Management_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rtrvDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Employees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDock2)).EndInit();
            this.radDock2.ResumeLayout(false);
            this.documentWindow2.ResumeLayout(false);
            this.documentWindow2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip2)).EndInit();
            this.toolTabStrip2.ResumeLayout(false);
            this.toolWindow2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer3)).EndInit();
            this.documentContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip2)).EndInit();
            this.documentTabStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Splitter splitter1;
        private Telerik.WinControls.UI.RadTreeView rtrvDepartment;
        private System.Windows.Forms.BindingSource BS_Employees;
        private System.Windows.Forms.ImageList imageList1;
        private Telerik.WinControls.UI.Docking.RadDock radDock2;
        private Telerik.WinControls.UI.Docking.DocumentWindow documentWindow2;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadLabelElement radLabelElement1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.Docking.ToolTabStrip toolTabStrip2;
        private Telerik.WinControls.UI.Docking.ToolWindow toolWindow2;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer3;
        private Telerik.WinControls.UI.Docking.DocumentTabStrip documentTabStrip2;
    }
}
