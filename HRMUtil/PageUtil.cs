namespace HRMUtil
{
    public class PageUtil
    {
        /// <summary>
        /// Set the InitialFocus to the given control. Only works when JavaScript is supported.
        /// </summary>
        /// <param name="control">Control to set the InitialFocus on.</param>
        //public static void SetInitialFocus(Control control)
        //{
        //    if (control.Page == null)
        //    {
        //        throw new ArgumentException(
        //    "The Control must be added to a Page before you can set the IntialFocus to it.");
        //    }
        //    if (control.Page.Request.Browser.JavaScript == true)
        //    {
        //        // Create JavaScript
        //        StringBuilder s = new StringBuilder();
        //        s.Append("\n<SCRIPT LANGUAGE='JavaScript'>\n");
        //        s.Append("<!--\n");
        //        s.Append("function SetInitialFocus()\n");
        //        s.Append("{\n");
        //        s.Append("   document.");

        //        // Find the Form
        //        Control p = control.Parent;
        //        while (!(p is System.Web.UI.HtmlControls.HtmlForm))
        //            p = p.Parent;
        //        s.Append(p.ClientID);

        //        s.Append("['");
        //        s.Append(control.UniqueID);

        //        // Set Focus on the selected item of a RadioButtonList
        //        RadioButtonList rbl = control as RadioButtonList;
        //        if (rbl != null)
        //        {
        //            string suffix = "_0";
        //            int t = 0;
        //            foreach (ListItem li in rbl.Items)
        //            {
        //                if (li.Selected)
        //                {
        //                    suffix = "_" + t.ToString();
        //                    break;
        //                }
        //                t++;
        //            }
        //            s.Append(suffix);
        //        }

        //        // Set Focus on the first item of a CheckBoxList
        //        if (control is CheckBoxList)
        //        {
        //            s.Append("_0");
        //        }

        //        s.Append("'].focus();\n");
        //        s.Append("}\n");

        //        if (control.Page.SmartNavigation)
        //            s.Append("window.setTimeout(SetInitialFocus, 500);\n");
        //        else
        //            s.Append("window.onload = SetInitialFocus;\n");

        //        s.Append("// -->\n");
        //        s.Append("</SCRIPT>");

        //        // Register Client Script
        //        control.Page.RegisterClientScriptBlock("InitialFocus", s.ToString());
        //    }
        //}
    }
}