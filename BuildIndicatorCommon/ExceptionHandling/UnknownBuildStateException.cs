using System;

namespace BuildIndicatorCommon.ExceptionHandling
{
    public class UnknownBuildStateException : Exception
    {
        public UnknownBuildStateException(string state)
            : base(String.Format("Unrecognized Build State: {0}", state))
        {
        }
    }
}
