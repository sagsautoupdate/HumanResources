using System;
using System.Drawing;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday.Helper
{
    public class CheckBoxHeaderCell : GridHeaderCellElement
    {
        private RadCheckBoxElement checkbox;

        private bool suspendProcessingToggleStateChanged;

        public CheckBoxHeaderCell(GridViewColumn column, GridRowElement row)
            : base(column, row)
        {
        }

        protected override Type ThemeEffectiveType
        {
            get { return typeof(GridHeaderCellElement); }
        }

        public override void Initialize(GridViewColumn column, GridRowElement row)
        {
            base.Initialize(column, row);
            column.AllowSort = false;
        }

        public override void SetContent()
        {
        }

        protected override void DisposeManagedResources()
        {
            checkbox.ToggleStateChanged -= checkbox_ToggleStateChanged;
            base.DisposeManagedResources();
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            checkbox = new RadCheckBoxElement();
            checkbox.ToggleStateChanged += checkbox_ToggleStateChanged;
            Children.Add(checkbox);
        }

        protected override SizeF ArrangeOverride(SizeF finalSize)
        {
            var size = base.ArrangeOverride(finalSize);

            var rect = GetClientRectangle(finalSize);
            checkbox.Arrange(new RectangleF((finalSize.Width - checkbox.DesiredSize.Width)/2, (rect.Height - 20)/2, 20,
                20));

            return size;
        }

        public override bool IsCompatible(GridViewColumn data, object context)
        {
            return (data.Name == "Check") && context is GridTableHeaderRowElement
                   && base.IsCompatible(data, context);
        }

        private void checkbox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (!suspendProcessingToggleStateChanged)
            {
                var valueState = false;

                if (args.ToggleState == ToggleState.On)
                    valueState = true;
                GridViewElement.EditorManager.EndEdit();
                TableElement.BeginUpdate();
                for (var i = 0; i < ViewInfo.Rows.Count; i++)
                    ViewInfo.Rows[i].Cells[ColumnIndex].Value = valueState;
                TableElement.EndUpdate(false);

                TableElement.Update(GridUINotifyAction.DataChanged);
            }
        }

        public void SetCheckBoxState(ToggleState state)
        {
            suspendProcessingToggleStateChanged = true;
            checkbox.ToggleState = state;
            suspendProcessingToggleStateChanged = false;
        }

        public override void Attach(GridViewColumn data, object context)
        {
            base.Attach(data, context);
            GridControl.ValueChanged += GridControl_ValueChanged;
        }

        public override void Detach()
        {
            if (GridControl != null)
                GridControl.ValueChanged -= GridControl_ValueChanged;
            base.Detach();
        }

        private void GridControl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var editor = sender as RadCheckBoxEditor;
                if (editor != null)
                {
                    GridViewElement.EditorManager.EndEdit();
                    if ((ToggleState) editor.Value == ToggleState.Off)
                    {
                        SetCheckBoxState(ToggleState.Off);
                    }
                    else
                    {
                        if ((ToggleState) editor.Value == ToggleState.On)
                        {
                            var found = false;
                            foreach (var row in ViewInfo.Rows)
                                if (((row != RowInfo) && (row.Cells[ColumnIndex].Value == null)) ||
                                    !(bool) row.Cells[ColumnIndex].Value)
                                {
                                    found = true;
                                    break;
                                }
                            if (!found)
                                SetCheckBoxState(ToggleState.On);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}