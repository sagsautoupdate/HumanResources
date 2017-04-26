using System;

namespace HRMUtil
{
    public class HRMException : ApplicationException
    {
        public const int INSERT_ERROR_CODE = 1300100;
        public const int UPDATE_ERROR_CODE = 1300200;
        public const int DELETE_ERROR_CODE = 1300300;

        /// <summary>
        /// </summary>
        private readonly long mErrorCode = 500;

        public HRMException(long errorcode) : base("", null)
        {
            mErrorCode = errorcode;
        }

        public HRMException(string message) : base(message, null)
        {
        }

        public HRMException(string message, long errorcode) : base(message, null)
        {
            mErrorCode = errorcode;
        }

        public HRMException(string message, Exception inner) : base(message, inner)
        {
        }

        public HRMException(string message, Exception inner, long errorcode)
            : base(message, inner)
        {
            mErrorCode = errorcode;
        }

        public long ErrorCode
        {
            get { return mErrorCode; }
        }
    }
}