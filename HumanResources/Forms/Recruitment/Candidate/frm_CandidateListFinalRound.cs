using System;
using System.Windows.Forms;
using HRMBLL.H2;
using HumanResources.Forms.Recruitment.Candidate.Contract;
using HumanResources.Properties;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Recruitment.Candidate
{
    public partial class frm_CandidateListFinalRound : RadForm
    {
        private static frm_CandidateListFinalRound s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();

        public frm_CandidateListFinalRound()
        {
            InitializeComponent();

            InitData();
        }

        public static frm_CandidateListFinalRound Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_CandidateListFinalRound();
                return s_Instance;
            }
        }

        private void CandidateListFinalRound_Load(object sender, EventArgs e)
        {
            AddMenu();
            _rcm.Items["rmiAdd"].Click += RmiCandidateContract_Click;
            _rcm.Items["rmiEdit"].Click += Frm_CandidateListFinalRound_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_CandidateListFinalRound_Click1;


            radDropDownList1.SelectedValueChanged += DdlRecruitmentSeason_SelectedValueChanged;
            FormClosed += CandidateListFinalRound_FormClosed;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.CellFormatting += RadGridView1_CellFormatting;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.CellDoubleClick += RadGridView1_CellDoubleClick;
            KeyDown += Frm_CandidateListFinalRound_KeyDown;
        }

        private void Frm_CandidateListFinalRound_Click1(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void Frm_CandidateListFinalRound_Click(object sender, EventArgs e)
        {
            var cc = new frm_CandidateContract(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                radGridView1.CurrentRow.Cells["FullName"].Value.ToString(),
                Convert.ToInt32(radGridView1.CurrentRow.Cells["SessionId"].Value),
                Convert.ToInt32(radGridView1.CurrentRow.Cells["PositionId"].Value));
            Utilities.Utilities.SetScreenColor(cc);
            cc.ShowDialog();
        }


        private void AddMenu()
        {
            var _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            var _rmiContract = new RadMenuItem
            {
                Text = "Hợp đồng đào tạo",
                Image = Resources.File,
                Name = "rmiContract"
            };
            var _rmiPreview = new RadMenuItem
            {
                Text = "Xem hợp đồng",
                Image = Resources.Preview,
                Name = "rmiPreviewContract"
            };
            _rmiPreview.Click += RmiCandidateReport_Click;
            _rmiContract.Items.Add(_rmiPreview);
            var _rmiPrint = new RadMenuItem
            {
                Text = "In hợp đồng",
                Image = Resources.File,
                Name = "rmiPrintContract"
            };
            _rmiPrint.Click += _rmiPrint_Click;
            ;
            _rmiContract.Items.Add(_rmiPrint);
            _rcm.Items.Add(_rmiContract);
        }

        private void InitData()
        {
            BS_RecruitmentSeason.DataSource = SessionsBLL.DT_GetAll();
            if (radDropDownList1.SelectedValue != null)
            {
                BS_CandidateList.DataSource = CandidatesBLL.GetDTForTrainingByFilter(string.Empty,
                    Convert.ToInt32(radDropDownList1.SelectedValue));
                Utilities.Utilities.GridFormatting(radGridView1);
            }
        }

        public void RefreshGrid()
        {
            BS_CandidateList.DataSource = CandidatesBLL.GetDTForTrainingByFilter(string.Empty,
                Convert.ToInt32(radDropDownList1.SelectedValue));
            Utilities.Utilities.GridFormatting(radGridView1);
        }


        private void _rmiPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var rp = new ReportPreview(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString().Trim(), "Prt");
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void Frm_CandidateListFinalRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                rbtPrint.PerformClick();
            if (e.KeyCode == Keys.F4)
                rbtPreview.PerformClick();
        }

        private void rbtPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var rp = new ReportPreview(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString().Trim(), "Prt");
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void rbtPreview_Click(object sender, EventArgs e)
        {
            try
            {
                var rp = new ReportPreview(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString().Trim(), "Pre");
                rp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void RadGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
        }

        private void RmiCandidateReport_Click(object sender, EventArgs e)
        {
            try
            {
                var rp = new ReportPreview(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString().Trim(), "Pre");
                rp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void RmiCandidateContract_Click(object sender, EventArgs e)
        {
            try
            {
                var cc = new frm_CandidateContract(this,
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["CandidateId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString(),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["SessionId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["PositionId"].Value));
                Utilities.Utilities.SetScreenColor(cc);
                cc.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn ứng viên trước khi tạo hợp đồng", "Alert", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
            }
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "Sex")
                if (Convert.ToBoolean(e.CellElement.RowInfo.Cells["Sex"].Value))
                    e.CellElement.Text = "Nam";
                else
                    e.CellElement.Text = "Nữ";
            if (e.Column.Name == "Birthday")
                if ((e.Row.Cells["DayOfBirth"].Value != DBNull.Value) &&
                    (e.Row.Cells["MonthOfBirth"].Value != DBNull.Value) &&
                    (e.Row.Cells["YearOfBirth"].Value != DBNull.Value))
                {
                    var _YearOfBirth = 0;
                    var _MonthOfBirth = 0;
                    var _DayOfBirth = 0;
                    try
                    {
                        _DayOfBirth = Convert.ToInt32(e.Row.Cells["DayOfBirth"].Value);
                    }
                    catch
                    {
                    }
                    try
                    {
                        _MonthOfBirth = Convert.ToInt32(e.Row.Cells["MonthOfBirth"].Value);
                    }
                    catch
                    {
                    }
                    try
                    {
                        _YearOfBirth = Convert.ToInt32(e.Row.Cells["YearOfBirth"].Value);
                    }
                    catch
                    {
                    }

                    if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                        e.Row.Cells["Birthday"].Value = new DateTime(_YearOfBirth, _MonthOfBirth, _DayOfBirth);
                }
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void CandidateListFinalRound_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void DdlRecruitmentSeason_SelectedValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            if (
                CandidatesBLL.GetDTForTrainingByFilter(string.Empty, Convert.ToInt32(radDropDownList1.SelectedValue))
                    .Rows.Count >
                0)
            {
                BS_CandidateList.DataSource = CandidatesBLL.GetDTForTrainingByFilter(string.Empty,
                    Convert.ToInt32(radDropDownList1.SelectedValue));
                radGridView1.DataSource = BS_CandidateList;
                Utilities.Utilities.GridFormatting(radGridView1);
            }
            else
            {
                radGridView1.DataSource = null;
            }
            Cursor.Current = Cursors.Default;
        }
    }
}