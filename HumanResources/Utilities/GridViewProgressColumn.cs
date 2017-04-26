using System;
using Telerik.WinControls.UI;

namespace HumanResources.Utilities
{
    public class GridViewProgressColumn : GridViewDataColumn
    {
        public GridViewProgressColumn(string name, string fieldName)
            : base(name, fieldName)
        {
            ReadOnly = true;
            SetDefaultValueOverride(DataTypeProperty, typeof(decimal));
        }

        public GridViewProgressColumn(string name)
            : base(name)
        {
            ReadOnly = true;
            SetDefaultValueOverride(DataTypeProperty, typeof(decimal));
        }

        public GridViewProgressColumn()
        {
            ReadOnly = true;
            SetDefaultValueOverride(DataTypeProperty, typeof(decimal));
        }

        public override Type GetCellType(GridViewRowInfo row)
        {
            if (row is GridViewDataRowInfo)
                return typeof(GridProgressCellElement);
            return base.GetCellType(row);
        }
    }
}