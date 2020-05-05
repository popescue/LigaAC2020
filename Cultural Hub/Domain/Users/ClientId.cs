using System;

namespace Domain
{
    public class ClientId
    {
        public string ClientIdValue { get; }
        public ClientId(string ClientIdValue)
        {
            if (string.IsNullOrWhiteSpace(ClientIdValue)) throw new ArgumentException("Id cannot be null or blank", "ClientIdValue");

            this.ClientIdValue = ClientIdValue;
        }
    }
}