using System.Collections.Generic;
using System.ComponentModel;

namespace HumanResources.Forms.Workingday.Helper
{
    //public enum WORKDAYERROR
    //{
    //    [Description("ERROR: LECH PHEP TREN BANG PHEP vs BANG CONG")] ERROR_LEAVE_LEAVEWD = 2,
    //    [Description("ERROR: LECH NCQD vs NCDC")] ERROR_NCQD_NCDC = 1,
    //    [Description("ERROR: NGHI TUAN")] ERROR_NGHITUAN = 0,
    //    [Description("ERROR: CHUA CHAM CONG hoac CHUA DI LAM")] ERROR_WORKING_DAYS = 3
    //}
    public class WE
    {
        private int _WEId;
        private string _WEName;

        public int WEId { get => _WEId; set => _WEId = value; }
        public string WEName { get => _WEName; set => _WEName = value; }

        public WE()
        {

        }

        public WE(int WEId, string WEName)
        {
            this._WEId = WEId;
            this._WEName = WEName;
        }

        public List<WE> GetAllError()
        {
            var list = new List<WE>();
            
            list.Add(new WE(0, "NGHI TUAN"));
            list.Add(new WE(1, "NCQD vs NCDC"));
            list.Add(new WE(2, "PHEP"));
            list.Add(new WE(3, "CHUA CHAM CONG hoac CHUA DI LAM"));

            return list;
        }

        public string GetErrorNameById(int id)
        {
            var _list = GetAllError();

            if (id != -1)
                return _list.Find(x => x._WEId == id)._WEName;
            else
                return "";
        }

        public int GetErrorIdByName(string name)
        {
            var _list = GetAllError();

            return _list.Find(x => x._WEName.Equals(name))._WEId;
        }
    }
}