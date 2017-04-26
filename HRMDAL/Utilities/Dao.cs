using System;

namespace HRMDAL.Utilities
{
    public class Dao : IDisposable
    {
        public StoreProcedure sproc;

        public void Dispose()
        {
            if (sproc != null)
            {
                sproc.Dispose();
                sproc = null;
            }
        }
    }
}