namespace lab.Storage
{
    [System.Serializable]
    public class IncorrectlabDataException : System.Exception
    {
        public IncorrectlabDataException() { }
        public IncorrectlabDataException(string message) : base(message) { }
        public IncorrectlabDataException(string message, System.Exception inner) : base(message, inner) { }
        protected IncorrectlabDataException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}