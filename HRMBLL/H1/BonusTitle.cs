using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    public class BonusTitle
    {
        public static DataTable GetByType(int type)
        {
            return new BonusTitleDAL().GetByType(type);
        }
    }
}