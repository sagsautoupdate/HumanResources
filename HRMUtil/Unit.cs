namespace HRMUtil
{
    public class Unit
    {
        public Unit(int unitId, string unitName)
        {
            UnitId = unitId;
            UnitName = unitName;
        }

        public Unit(double unitIdD, string unitName)
        {
            UnitIdD = unitIdD;
            UnitName = unitName;
        }

        public int UnitId { get; set; }
        public double UnitIdD { get; set; }

        public string UnitName { get; set; }
    }
}