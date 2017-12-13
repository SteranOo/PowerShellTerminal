using System;
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Text;

namespace PowerShellTerminal.Server.Services
{
    public class RunspaceService
    {
        private static RunspaceService _instance;

        public static RunspaceService Instance => _instance ?? (_instance = new RunspaceService());

        private readonly Dictionary<string, Runspace> _runspaces;

        protected RunspaceService()
        {
            _runspaces = new Dictionary<string, Runspace>();

        }

        public void AddRunspace(string runspaceId)
        {
            var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            _runspaces.Add(runspaceId, runspace);

        }

        public string GetCurrentLocation(string runspaceId)
        {
            return _runspaces[runspaceId].SessionStateProxy.Path.CurrentLocation.Path;
        }

        public string RunScript(string runspaceId, string scriptText)
        {
            var pipeline = _runspaces[runspaceId].CreatePipeline();
            pipeline.Commands.AddScript(scriptText);
            pipeline.Commands.Add("Out-String");
            try
            {
                var results = pipeline.Invoke();
                var stringBuilder = new StringBuilder();
                foreach (var obj in results)
                {
                    stringBuilder.AppendLine(obj.ToString());
                }

                return stringBuilder.ToString();
            }
            catch (Exception e)
            {
                return e.Message + "\n\n";
            }
        }
    }
}
