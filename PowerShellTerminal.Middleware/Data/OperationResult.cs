using System.Runtime.Serialization;

namespace PowerShellTerminal.Middleware.Data
{
    [DataContract]
    public class OperationResult
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }

    [DataContract]
    public class OperationResult<T>
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public T Response { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
