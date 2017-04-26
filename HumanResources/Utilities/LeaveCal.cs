namespace HumanResources.Utilities
{
    public class LeaveCal
    {
        public LeaveCal(int Month, int Value, int Sum)
        {
            this.Month = Month;
            this.Value = Value;
            this.Sum = Sum;
        }

        public LeaveCal()
        {
        }

        public int Month { get; set; }

        public int Sum { get; set; }

        public int Value { get; set; }
    }
}