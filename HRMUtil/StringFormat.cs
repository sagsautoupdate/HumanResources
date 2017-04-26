using System;
using System.Security.Cryptography;
using System.Text;

namespace HRMUtil
{
    public class StringFormat
    {
        public static string FormatMonthYear
        {
            get { return "{0:MM/yyyy}"; }
        }

        public static string FormatGridUserId
        {
            get { return "{0:00000#}"; }
        }

        public static string FormatGridContractNumber
        {
            get { return "{0:000#}"; }
        }

        public static string FormatGridUserId2
        {
            get { return "{0:000#}"; }
        }

        public static string FormatUserId
        {
            get { return "00000#"; }
        }

        public static string FormatEmpLeaveId
        {
            get { return "00000#"; }
        }

        public static string FormatMark
        {
            get { return "#0.0"; }
        }

        public static string FormatCurrency
        {
            get { return "{0:#,###0.00}"; }
        }

        public static string FormatCurrencyFinal
        {
            get { return "{0:#,###0}"; }
        }

        public static string FormatCoefficient
        {
            get { return "#,##0.00"; }
        }

        public static string FormatCoefficient3DigitGrid
        {
            get { return "{0:#,###0.000}"; }
        }

        public static string FormatCoefficient3Digit
        {
            get { return "#,###0.000"; }
        }

        public static string ReplaceWordWrap(string text)
        {
            text = text.Replace("'", "\"");
            text = text.Replace("|", ":");
            text = text.Replace(";", "<br/>");
            return text;
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string FormatMonthYearVN(DateTime date)
        {
            return date.ToString("MM/yyyy");
        }

        public static string GetUserCode(int userId)
        {
            return userId.ToString("000#");
        }

        public static string GetUserCodeX6(int userId)
        {
            return userId.ToString("00000#");
        }

        public static double SetRoundCoefficient3Digit(double value)
        {
            var svalue = value.ToString("#,###0.000");
            return Convert.ToDouble(svalue);
        }

        public static string SetFormatCoefficient(double value)
        {
            return value.ToString("#0.00");
        }

        public static string SetContractNo(int value)
        {
            return value.ToString("000#");
        }

        public static string SetDay(int value)
        {
            return value.ToString("0#");
        }

        public static string SetMonth(int value)
        {
            return value.ToString("0#");
        }

        public static string SetYear(int value)
        {
            return value.ToString("000#");
        }

        public static string SetFormatMoney(decimal value)
        {
            return value.ToString("#,###0.00");
        }

        public static string SetFormatMoneyFinal(decimal value)
        {
            return value.ToString("#,###0");
        }

        public static string getSHA1(string inputString)
        {
            return BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.Default.GetBytes(inputString)))
                .Replace("-", "");
        }

        public static string TrimFullName(string fullname)
        {
            var arrFN = fullname.Split(' ');
            var fullnameReturn = string.Empty;
            for (var i = 0; i < arrFN.Length; i++)
            {
                var s = arrFN[i];
                if (s.Trim().Length > 0)
                    if (fullnameReturn.Length > 0)
                        fullnameReturn = fullnameReturn + " " + s;
                    else
                        fullnameReturn = s;
            }
            return fullnameReturn;
        }
    }
}