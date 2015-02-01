
using System;

namespace Weather.Common.Exceptions
{
    public class NotImplementedMethodException : Exception
    {
        public NotImplementedMethodException()
        {
        }

        public NotImplementedMethodException(string message = null, params string[] args)
            : base(message)
        {
            this.MethodArgs = string.Join(", ", args);
        }

        public string MethodArgs { get; private set; }
    }
}
