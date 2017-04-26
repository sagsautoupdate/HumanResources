namespace HumanResources
{
    partial class ListSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListSheet));
            this.rlstListSheet = new Telerik.WinControls.UI.RadListView();
            this.rbtnOK = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.rlstListSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rlstListSheet
            // 
            this.rlstListSheet.Location = new System.Drawing.Point(3, 6);
            this.rlstListSheet.Name = "rlstListSheet";
            this.rlstListSheet.Size = new System.Drawing.Size(254, 207);
            this.rlstListSheet.TabIndex = 0;
            this.rlstListSheet.DoubleClick += new System.EventHandler(this.rlstListSheet_DoubleClick);
            // 
            // rbtnOK
            // 
            this.rbtnOK.Location = new System.Drawing.Point(75, 222);
            this.rbtnOK.Name = "rbtnOK";
            this.rbtnOK.Size = new System.Drawing.Size(110, 24);
            this.rbtnOK.TabIndex = 1;
            this.rbtnOK.Text = "OK";
            this.rbtnOK.Click += new System.EventHandler(this.rbtnOK_Click);
            // 
            // ListSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 251);
            this.Controls.Add(this.rbtnOK);
            this.Controls.Add(this.rlstListSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListSheet";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Các Sheet Từ File Nguồn Excel";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.ListSheet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rlstListSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadListView rlstListSheet;
        private Telerik.WinControls.UI.RadButton rbtnOK;
    }
}
