using System;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday.Helper
{
    public class CustomCheckBoxColumn : GridViewCheckBoxColumn
    {
        public override Type GetCellType(GridViewRowInfo row)
        {
            if (row is GridViewTableHeaderRowInfo)
                return typeof(CheckBoxHeaderCell);
            return base.GetCellType(row);
        }
    }
}