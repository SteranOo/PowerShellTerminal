using System.ServiceModel;
using PowerShellTerminal.Middleware.Data;

namespace PowerShellTerminal.Middleware.Network
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IServerEngine
    {
        [OperationContract]
        OperationResult Connect();

        [OperationContract]
        OperationResult<string> RunScript(string scriptText);

        [OperationContract]
        OperationResult<string> GetCurrentLocation();
    }
}
