using System.Runtime.Serialization;

namespace NLox
{
    [Serializable]
    internal class SyntaxError : Exception
    {
        public int Line { get; set; }

        public SyntaxError()
        {
        }

        public SyntaxError(string? message) : base(message)
        {
        }

        public SyntaxError(int line, string? message) : base(message)
        {
            Line = line;
        }

        public SyntaxError(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SyntaxError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}