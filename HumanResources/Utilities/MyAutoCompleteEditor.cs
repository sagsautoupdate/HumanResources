using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace HumanResources.Utilities
{
    internal class MyAutoCompleteEditor : RadTextBoxControlEditor
    {
        protected override RadElement CreateEditorElement()
        {
            return new RadAutoCompleteBoxElement();
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            var element = EditorElement as RadAutoCompleteBoxElement;

            if (element.IsAutoCompleteDropDownOpen)
                return;
            base.OnKeyDown(e);
        }
    }
}