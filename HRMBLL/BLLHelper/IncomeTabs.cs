namespace HRMBLL.BLLHelper
{
    //*********************************************************************
    //
    // IncomeTabs Interface
    //
    // This interface is implemented by each tab in the Monthly.aspx page.
    //
    //*********************************************************************


    public interface IncomeTabs
    {
        int UserId { get; set; }

        int Month { get; set; }

        int Year { get; set; }

        void Init();
    }
}