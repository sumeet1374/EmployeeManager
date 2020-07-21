using System;

namespace Common.Services
{
    /// <summary>
    ///  Exception class to specifically raise business exception if certian
    ///  business condition is not met.
    /// </summary>
    public class BusinessException:Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(string message):base(message)
        {

        }
    }
}
