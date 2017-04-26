using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using HRMUtil;

namespace HumanResources
{
    public class clsGlobal
    {
        public static bool IsLoggedIn = false;

        public static int UserId = 0;
        public static string UserName = string.Empty;
        public static string FullName = string.Empty;
        public static string Password = string.Empty;
        public static DateTime BirthDay = FormatDate.GetSQLDateMinValue;
        public static string PositionName = string.Empty;
        public static string DepartmentName = string.Empty;
        public static string DepartmentFullName = string.Empty;
        public static int DepartmentLevel = 0;
        public static int StatusIN = 1;
        public static int StatusOUT = 2;
        public static string Server = string.Empty;
        public static DateTime SalaryDataDate = DateTime.MinValue;
        public static string PathCurrentDirectory = Directory.GetCurrentDirectory();
        public static int SalaryIsVCQLNN = 0;
        public static List<EmployeeImageList> EmployeeImageList;
        public static string StoredProcedures;
        public static string ChangedContent;


        public static string CurrentImageURL = string.Empty;
        public static string CurrentConnectionString = string.Empty;

        public static string ConnectionString = string.Empty;

        public static string CompanyName = string.Empty;
        public static string Representation = string.Empty;

        public static bool IsAdmin = false;
        public static bool IsHRAdmin = false;
    }

    public class EmployeeImageList
    {
        public EmployeeImageList()
        {
        }

        public EmployeeImageList(string imageName, Image imageValue)
        {
            ImageName = imageName;
            Image = imageValue;
        }

        public Image Image { get; set; }

        public string ImageName { get; set; }

        public Image GetImageByName(string name)
        {
            return ImageName == name ? Image : null;
        }
    }
}