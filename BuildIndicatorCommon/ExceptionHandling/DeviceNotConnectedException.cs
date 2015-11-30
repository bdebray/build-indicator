using System;

namespace BuildIndicatorCommon.ExceptionHandling
{
    public class DeviceNotConnectedException : Exception
    {
        public DeviceNotConnectedException()
            : this("Device not connected.")
        {
        }

        public DeviceNotConnectedException(string message)
            : base(message)
        {
        }

        public DeviceNotConnectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
