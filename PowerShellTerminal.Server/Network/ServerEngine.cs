using System;
using System.ServiceModel;
using PowerShellTerminal.Middleware.Data;
using PowerShellTerminal.Middleware.Network;
using PowerShellTerminal.Server.Services;

namespace PowerShellTerminal.Server.Network
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServerEngine : IServerEngine
    {
        private readonly string _sessionId;

        public ServerEngine()
        {
            _sessionId = OperationContext.Current.SessionId;
            RunspaceService.Instance.AddRunspace(_sessionId);
        }

        private OperationResult MakeOperation(Action operation)
        {
            try
            {
                operation();
                return new OperationResult { Success = true };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        private OperationResult<T> MakeOperation<T>(Func<T> operation)
        {
            try
            {
                var result = operation();
                return new OperationResult<T> { Success = true, Response = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<T> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public OperationResult Connect()
        {
            return MakeOperation(() => { });
        }

        public OperationResult<string> RunScript(string scriptText)
        {
            return MakeOperation(() => RunspaceService.Instance.RunScript(_sessionId, scriptText));
        }

        public OperationResult<string> GetCurrentLocation()
        {
            return MakeOperation(() => RunspaceService.Instance.GetCurrentLocation(_sessionId));
        }
    }
}
