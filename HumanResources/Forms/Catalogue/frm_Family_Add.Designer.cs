namespace HumanResources.Forms.Catalogue
{
    partial class frm_Family_Add
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtRelationName = new Telerik.WinControls.UI.RadTextBox();
            this.txtDescription = new Telerik.WinControls.UI.RadTextBox();
            this.ddlRelationType = new Telerik.WinControls.UI.RadDropDownList();
            this.BS_Type = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSavetvtg = new Telerik.WinControls.UI.RadButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRelationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSavetvtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.radLabel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.radLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radLabel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRelationName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDescription, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ddlRelationType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSavetvtg, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(559, 137);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // radLabel4
            // 
            this.radLabel4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radLabel4.Location = new System.Drawing.Point(3, 78);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(36, 18);
            this.radLabel4.TabIndex = 1;
            this.radLabel4.Text = "Mô tả";
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radLabel1.Location = new System.Drawing.Point(3, 8);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(90, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Tên mối quan hệ";
            // 
            // radLabel2
            // 
            this.radLabel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radLabel2.Location = new System.Drawing.Point(3, 43);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(59, 18);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "Thuộc bên";
            // 
            // txtRelationName
            // 
            this.txtRelationName.AutoSize = false;
            this.tableLayoutPanel1.SetColumnSpan(this.txtRelationName, 3);
            this.txtRelationName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRelationName.Location = new System.Drawing.Point(114, 3);
            this.txtRelationName.Name = "txtRelationName";
            this.txtRelationName.NullText = "Tên mối quan hệ...";
            this.txtRelationName.Size = new System.Drawing.Size(442, 29);
            this.txtRelationName.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.AutoSize = false;
            this.tableLayoutPanel1.SetColumnSpan(this.txtDescription, 3);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(114, 73);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.NullText = "Mô tả mối quan hệ gia đình...";
            this.txtDescription.Size = new System.Drawing.Size(442, 29);
            this.txtDescription.TabIndex = 3;
            // 
            // ddlRelationType
            // 
            this.ddlRelationType.AutoSize = false;
            this.tableLayoutPanel1.SetColumnSpan(this.ddlRelationType, 3);
            this.ddlRelationType.DataSource = this.BS_Type;
            this.ddlRelationType.DisplayMember = "RTypeName";
            this.ddlRelationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlRelationType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlRelationType.Location = new System.Drawing.Point(114, 38);
            this.ddlRelationType.Name = "ddlRelationType";
            this.ddlRelationType.Size = new System.Drawing.Size(442, 29);
            this.ddlRelationType.TabIndex = 9;
            this.ddlRelationType.ValueMember = "RTypeId";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Location = new System.Drawing.Point(476, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "<html><strong>Thoát</strong></html>";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSavetvtg
            // 
            this.btnSavetvtg.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSavetvtg.Location = new System.Drawing.Point(393, 110);
            this.btnSavetvtg.Name = "btnSavetvtg";
            this.btnSavetvtg.Size = new System.Drawing.Size(77, 24);
            this.btnSavetvtg.TabIndex = 7;
            this.btnSavetvtg.Text = "<html><strong>Lưu</strong></html>";
            this.btnSavetvtg.Click += new System.EventHandler(this.btnSavetvtg_Click);
            // 
            // frm_Family_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 137);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(600, 170);
            this.Name = "frm_Family_Add";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MaxSize = new System.Drawing.Size(600, 170);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DANH MỤC MỐI QUAN HỆ TRONG GIA ĐÌNH";
            this.Load += new System.EventHandler(this.frm_Family_Add_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRelationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BS_Type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSavetvtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtRelationName;
        private Telerik.WinControls.UI.RadTextBox txtDescription;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadDropDownList ddlRelationType;
        private System.Windows.Forms.BindingSource BS_Type;
        private Telerik.WinControls.UI.RadButton btnSavetvtg;
    }
}
