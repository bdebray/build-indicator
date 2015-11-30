using System;

namespace BuildIndicatorCommon.ExceptionHandling
{
    public class BuildNotFoundException : Exception
    {
        public BuildNotFoundException()
            : this("Build not found.")
        {
        }

        public BuildNotFoundException(string message)
            : base(message)
        {
        }

        public BuildNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
