using System;
using System.ServiceModel;

namespace PowerShellTerminal.Client.Network
{
    public class ServiceClientFactory<TChannel> : ClientBase<TChannel> where TChannel : class
    {
        public TChannel Create(string url)
        {
            Endpoint.Address = new EndpointAddress(new Uri(url));
            return Channel;
        }
    }
}
