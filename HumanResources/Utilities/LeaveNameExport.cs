namespace HumanResources.Utilities
{
    public class LeaveNameExport
    {
        public LeaveNameExport()
        {
        }

        public LeaveNameExport(int Id, string Name)
        {
            LeaveId = Id;
            LeaveName = Name;
        }

        public int LeaveId { get; set; }

        public string LeaveName { get; set; }
    }
}