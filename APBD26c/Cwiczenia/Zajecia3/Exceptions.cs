namespace Zajecia3
{
    public class OverfillException : System.Exception
    {
        public OverfillException() { }
        public OverfillException(string message) : base(message) { }
        public OverfillException(string message, SystemException inner) : base(message, inner) { }
    }
}