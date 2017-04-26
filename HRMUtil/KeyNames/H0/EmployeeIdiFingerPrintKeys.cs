namespace HRMUtil.KeyNames.H0
{
    public class EmployeeIdiFingerPrintKeys
    {
        /// <summary>
        ///     Some field names of H0_EmployeeIdiFingerPrint table
        /// </summary>
        public const string Field_EmployeeIdiFingerPrint_PK_UserFingerPrintId = "PK_UserFingerPrintId";

        public const string Field_EmployeeIdiFingerPrint_UserId = "UserId";
        public const string Field_EmployeeIdiFingerPrint_FingerIndex = "FingerIndex";
        public const string Field_EmployeeIdiFingerPrint_IndexValue = "IndexValue";
        public const string Field_EmployeeIdiFingerPrint_Features = "Features";


        /// <summary>
        ///     StoreProcedure name of H0_EmployeeIdiFingerPrint object.
        /// </summary>
        public const string Sp_Ins_H0_EmployeeIdiFingerPrint = "Ins_H0_EmployeeIdiFingerPrint";

        public const string Sp_Upd_H0_EmployeeIdiFingerPrint = "Upd_H0_EmployeeIdiFingerPrint";
        public const string Sp_Del_H0_EmployeeIdiFingerPrint = "Del_H0_EmployeeIdiFingerPrint";
        public const string Sp_Del_H0_EmployeeIdiFingerPrintByUserId = "Del_H0_EmployeeIdiFingerPrintByUserId";

        public const string Sp_Del_H0_EmployeeIdiFingerPrintByUserIdFingerIndex =
            "Del_H0_EmployeeIdiFingerPrintByUserIdFingerIndex";

        public const string Sp_Sel_H0_EmployeeIdiFingerPrintByFilter = "Sel_H0_EmployeeIdiFingerPrintByFilter";
    }
}