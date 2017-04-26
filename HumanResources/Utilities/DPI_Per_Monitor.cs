using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

/// <summary>
///     DPI_Per_Monitor
///     Use to make a simple Windows for app truely DPI-aware.
///     That is AVOID woolly apps!
///     1) Make sure your forms are designed with font size set in PIXELS, not POINTS (yes!!)
///     All controls possible should use inherited fonts
///     Each font explicitly given in PIXEL needs to be handled  in the callback function
///     provided to Check_WM_DPICHANGED
///     The standard "8.25pt" matches "11.0px" (8.25 *96/72 = 11)
///     2) Leave the form in the default Autoscale=Font mode
///     3) Insert call to DPI_Per_Monitor.TryEnableDPIAware() , right after InitializeComponent();
///     4) Add suitable call to DPI_Per_Monitor.Check_WM_DPICHANGED_WM_NCCREATE in DefWndProc
///     If a DefWndProc override is not already present add e.g. these lines
///     protected override void DefWndProc(ref Message m) {
///     DPI_Per_Monitor.Check_WM_DPICHANGED_WM_NCCREATE(SetUserFonts,m, this.Handle);
///     base.DefWndProc(ref m);
///     }
///     that has a call-back function you must provide, that set fonts as needed (in pixels or points),
///     if all are inherited, then just one set:
///     void SetUserFonts(float scaleFactorX, float scaleFactorY) {
///     Font = new Font(Font.FontFamily, 11f * scaleFactorX, GraphicsUnit.Pixel);
///     }
///     5) And a really odd one, due to a Visual studio BUG.
///     This ONLY works, if your PRIMARY monitor is scalled at 100% at COMPILE time!!!
///     It is NOT just a matter of using a different reference than the 96 dpi below, and
///     it does not help to run it from a secondary monitor set to 100% !!!
///     And to make things worse, Visual Studio is one of the programs that doesn't handle
///     change of scale on primary monitor, without at the least a sign out....
///     NOTE that if you got (Checked)ListBoxes, repeated autosizing (e.g. move between monitors)
///     might fail as it rounds the height down to a multipla of the itemheight. So despite a
///     bottom-anchor it will 'creep' upwards...
///     So I recommend to place an empty and/or hidden bottom-anchored label just below the boxes,
///     to scale the spacing and set e.g. :  yourList.Height=yourAnchor.Top-yourList.Top
///     Also note that not everything gets scaled automatically. Only new updates of Win10 handles
///     the titlebar correctly. Also the squares of checkboxes are forgotten.
/// </summary>
internal static class DPI_Per_Monitor
{
    public static float MeanDPIprimary = 96f;

    private static readonly SemaphoreSlim semaphoreScale = new SemaphoreSlim(1, 1);
    private static int Oldscales = -1;
    private static bool isCurrentlyScaling;

    [DllImport("SHCore.dll")]
    private static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetProcessDPIAware();

    internal static void TryEnableDPIAware(Form form)
    {
        try
        {
            SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.Process_Per_Monitor_DPI_Aware);
        }
        catch
        {
            try
            {
                SetProcessDPIAware();
            }
            catch
            {
            }
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool EnableNonClientDpiScaling(IntPtr hWnd);

    internal static void Check_WM_DPICHANGED_WM_NCCREATE(VoidOfFloatFloatDelegate CallBackWithScale, Message m,
        IntPtr hwnd)
    {
        switch (m.Msg)
        {
            case 0x02E0:
                try
                {
                    semaphoreScale.Wait(2000);
                    var Local_isCurrentlyScaling = isCurrentlyScaling;
                    isCurrentlyScaling = true;
                    semaphoreScale.Release();
                    if (Local_isCurrentlyScaling)
                        break;
                    var CurrentScales = m.WParam.ToInt32();


                    if (Oldscales != CurrentScales)
                    {
                        var scaleFactorX = (CurrentScales & 0xFFFF)/96f;
                        var scaleFactorY = (CurrentScales >> 16)/96f;
                        CallBackWithScale(scaleFactorX, scaleFactorY);
                    }
                    semaphoreScale.Wait(2000);
                    Oldscales = CurrentScales;
                    isCurrentlyScaling = false;
                    semaphoreScale.Release();
                }
                catch
                {
                }

                break;
            case 0x81:
                try
                {
                    EnableNonClientDpiScaling(hwnd);
                }
                catch
                {
                }
                break;
            default:
                break;
        }
    }

    private enum PROCESS_DPI_AWARENESS
    {
        Process_DPI_Unaware = 0,
        Process_Per_Monitor_DPI_Aware = 2,
        Process_System_DPI_Aware = 1
    }

    internal delegate void VoidOfFloatFloatDelegate(float x, float y);
}