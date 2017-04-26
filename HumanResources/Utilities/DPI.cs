using System.Runtime.InteropServices;

namespace HumanResources.Utilities
{
    public class DPI
    {
        public enum _Process_DPI_Awareness
        {
            Process_DPI_Unaware = 0,
            Process_Per_Monitor_DPI_Aware = 2,
            Process_System_DPI_Aware = 1
        }

        [DllImport("shcore.dll")]
        public static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);
    }
}