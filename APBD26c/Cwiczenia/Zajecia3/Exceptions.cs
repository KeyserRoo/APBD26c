namespace Zajecia3 {
    public class OverfillException : System.Exception {
        public OverfillException() { }
        public OverfillException(string message) : base(message) { }
        public OverfillException(string message, SystemException inner) : base(message, inner) { }
    }
    public class LessThanZeroException : System.Exception {
        public LessThanZeroException() { }
        public LessThanZeroException(string message) : base(message) { }
        public LessThanZeroException(string message, SystemException inner) : base(message, inner) { }
    }
    public class BoatEmptyException : System.Exception{
        public BoatEmptyException() { }
        public BoatEmptyException(string message) : base(message) { }
        public BoatEmptyException(string message, SystemException inner) : base(message, inner) { }
    }
    public class CapacityException : System.Exception{
        public CapacityException() { }
        public CapacityException(string message) : base(message) { }
        public CapacityException(string message, SystemException inner) : base(message, inner) { }
    }
}