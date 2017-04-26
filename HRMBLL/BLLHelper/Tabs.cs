namespace HRMBLL.BLLHelper
{
    //*********************************************************************
    //
    // IncomeTabs Interface
    //
    // This interface is implemented by each tab in the Monthly.aspx page.
    //
    //*********************************************************************


    public interface Tabs
    {
        int UserId { get; set; }

        void Init();
    }
}