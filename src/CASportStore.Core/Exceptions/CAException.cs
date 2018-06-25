using System;
using System.Collections.Generic;
using System.Text;

namespace CASportStore.Core.Exceptions
{
    public enum StatusCode
    {
        EMPTY,
        PRODUCT_EXIST,
        PRODUCT_NOT_FOUND,
    }
    public class CAException : Exception
    {
        public StatusCode ErrorCode { get; }

        public CAException()
        {
        }

        public CAException(StatusCode errorCode) 
            : this(errorCode, null, null, null)
        {

        }

        public CAException(StatusCode message, params object[] args) 
            : this(null, null, message, args)
        {

        }

        public CAException(StatusCode errorCode, string message, params object[] args) 
            : this(errorCode, null, message, args)
        {

        }

        public CAException(Exception innerException, string message, params object[] args) 
            : this(StatusCode.EMPTY, innerException, message, args)
        {

        }

        public CAException(StatusCode errorCode, Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
