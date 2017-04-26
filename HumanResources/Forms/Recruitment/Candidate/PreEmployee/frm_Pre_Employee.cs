using System;
using System.Windows.Forms;
using HRMBLL.H2;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Recruitment.Candidate.PreEmployee
{
    public partial class frm_Pre_Employee : RadForm
    {
        private static frm_Pre_Employee s_Instance;

        public frm_Pre_Employee()
        {
            InitializeComponent();
        }

        public static frm_Pre_Employee Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Pre_Employee();
                return s_Instance;
            }
        }

        private void frm_Pre_Employee_Load(object sender, EventArgs e)
        {
            InitData();

            radGridView1.CellEditorInitialized += RadGridView1_CellEditorInitialized;
            radGridView1.CellFormatting += RadGridView1_CellFormatting;
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
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
            if (e.Column.Name == "Sex")
                if (e.CellElement != null)
                    if (Convert.ToBoolean(e.CellElement.Value))
                        e.CellElement.Text = "Nam";
                    else
                        e.CellElement.Text = "Nữ";
        }

        private void RadGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (radGridView1.CurrentColumn is GridViewComboBoxColumn)
                if (radGridView1.CurrentColumn.Name == "CandidateTrainingJobHistoryId")
                {
                    var editor = (RadDropDownListEditor) radGridView1.ActiveEditor;
                    var editorElement = (RadDropDownListEditorElement) editor.EditorElement;
                    try
                    {
                        editorElement.DataSource = CandidateTrainingJobHistoryBLL.GetDTByCandidateId_Type(
                            Convert.ToInt32(e.Row.Cells["CandidateId"].Value), "EDU");
                    }
                    catch (Exception ex)
                    {
                    }


                    editorElement.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                    editorElement.ValueMember = "Training_Job1";
                    editorElement.DisplayMember = "CandidateTrainingJobHistoryId";

                    e.Row.Cells["EducationId"].Value = editorElement.Text;
                }
        }


        private void radDropDownList1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (radDropDownList1.SelectedValue != null)
            {
                Cursor.Current = Cursors.AppStarting;

                radGridView1.DataSource = CandidatesBLL.GetForTrainingByFilterPreEmployee(string.Empty,
                    Convert.ToInt32(radDropDownList1.SelectedValue));


                Utilities.Utilities.GridFormatting(radGridView1);
                Cursor.Current = Cursors.Default;
            }
        }


        private void InitData()
        {
            radDropDownList1.DataSource = SessionsBLL.DT_GetAll();
            if (radDropDownList1.SelectedValue != null)
            {
                Cursor.Current = Cursors.AppStarting;

                radGridView1.DataSource = CandidatesBLL.GetForTrainingByFilterPreEmployee(string.Empty,
                    Convert.ToInt32(radDropDownList1.SelectedValue));
                Utilities.Utilities.GridFormatting(radGridView1);


                Cursor.Current = Cursors.Default;
            }
        }


        private void radGridView1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
        }
    }
}