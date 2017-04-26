using System;
using System.Drawing;
using Telerik.WinControls.UI;

namespace HumanResources.Utilities
{
    public class GridProgressCellElement : GridDataCellElement
    {
        private RadProgressBarElement progressBar;

        public GridProgressCellElement(GridViewColumn column, GridRowElement row)
            : base(column, row)
        {
        }

        protected override Type ThemeEffectiveType
        {
            get { return typeof(GridDataCellElement); }
        }

        public override bool IsEditable
        {
            get { return false; }
        }

        protected override void UpdateInfoCore()
        {
            base.UpdateInfoCore();

            var indicator = progressBar.IndicatorElement1;
            indicator.BackColor = Color.Red;
            indicator.BackColor2 = Color.Red;
            indicator.BackColor3 = Color.Red;
            indicator.BackColor4 = Color.Red;
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            progressBar = new RadProgressBarElement();
            progressBar.StretchVertically = true;
            progressBar.StretchHorizontally = true;
            Children.Add(progressBar);
        }

        public override bool IsCompatible(GridViewColumn data, object context)
        {
            return data is GridViewProgressColumn && context is GridDataRowElement;
        }

        protected override void SetContentCore(object value)
        {
            progressBar.Value1 = Convert.ToInt32(value);
        }
    }
}