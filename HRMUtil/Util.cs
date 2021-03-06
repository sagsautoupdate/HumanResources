namespace HRMUtil
{
    public class Util
    {
        #region Column Explain for H1_WorkdayCoefficientEmployeesFinal

        public static string GetExplainForH1_WorkdayCoefficientEmployeesFinal(string colName)
        {
            var nameReturn = string.Empty;
            switch (colName)
            {
                case "HSQDNCHL":
                    nameReturn = "HSQDNCHL: Hệ số quy đổi ngày công hưởng lương";
                    break;
                case "HSTLNS":
                    nameReturn = "HSTLNS: Hệ số tính lương năng suất";
                    break;
                case "HSLNS":
                    nameReturn = "HSLNS: Hệ số lương năng suất được hưởng";
                    break;
                case "HSLNSPCTN":
                    nameReturn = "HSLNSPCTN: Hệ số lương năng suất phụ cấp trách nhiệm được hưởng";
                    break;
                case "HSLCB":
                    nameReturn = "HSLCB: Hệ số lương cơ bản được hưởng";
                    break;
                case "HSPCDH":
                    nameReturn = "HSPCDH: Hệ số phụ cấp độc hại";
                    break;
                case "HSPCTN":
                    nameReturn = "HSPCTN: Hệ số phụ cấp trách nhiệm";
                    break;
                case "HSPCKV":
                    nameReturn = "HSPCKV: Hệ số phụ cấp khu vực";
                    break;
                case "HSPCCV":
                    nameReturn = "HSPCCV: Hệ số phụ cấp chức vụ";
                    break;
                case "HSK":
                    nameReturn = "HSK: Hệ số đánh giá hoàn thành công việc";
                    break;
            }
            return nameReturn;
        }

        #endregion

        #region Contains Methods

        public static bool IsContainsByArr(string str, int id)
        {
            var isReturn = false;
            var arr = str.Split(',');

            foreach (var item in arr)
                if (item.Trim().Length > 0)
                    if (int.Parse(item) == id)
                    {
                        isReturn = true;
                        break;
                    }

            return isReturn;
        }

        public static bool IsContains(string str, int id)
        {
            //bool isReturn = false;
            //string[] arr = str.Split(',');

            //foreach (string item in arr)
            //{

            //    if (item.Trim().Length > 0)
            //    {
            //        if (int.Parse(item) == id)
            //        {
            //            isReturn = true;
            //            break;
            //        }
            //    }
            //}

            return str.Contains("," + id + ",");
        }

        public static string GetFormatVNDate(int day, int month, int year)
        {
            if ((day > 0) && (month > 0) && (year > 0))
                return day + "/" + month + "/" + year;
            if ((day <= 0) && (month > 0) && (year > 0))
                return month + "/" + year;
            if ((day <= 0) && (month <= 0) && (year > 0))
                return year.ToString();
            return string.Empty;
        }


        public static bool CheckIsHTCVTitle(string sName)
        {
            try
            {
                var index = sName.IndexOf('.');
                var value = sName.Substring(0, index + 1).Trim();

                if (value.Equals("1.") || value.Equals("2.") || value.Equals("3.") || value.Equals("4.") ||
                    value.Equals("5.") || value.Equals("6.") || value.Equals("7.") ||
                    value.Equals("I.") || value.Equals("II.") || value.Equals("III.") || value.Equals("IV."))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Reject Methods

        public static string RejectFirstLastComma(string str)
        {
            var strReturn = string.Empty;
            if (str != null)
                if (str.Length > 1)
                    strReturn = str.Substring(1, str.Length - 2);
            return strReturn;
        }

        public static string RejectFirstComma(string str)
        {
            var strReturn = string.Empty;
            if (str.Length > 1)
                strReturn = str.Substring(1, str.Length - 1);
            return strReturn;
        }

        public static string RejectLastComma(string str)
        {
            var strReturn = string.Empty;
            if (str.Length > 1)
                strReturn = str.Substring(0, str.Length - 1);
            return strReturn;
        }

        #endregion
    }
}