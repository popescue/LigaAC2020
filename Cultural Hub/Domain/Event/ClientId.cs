using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ClientId
    {
        public ClientId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Id cannot be empty", nameof(value));

            Value = value;
        }

        public Guid Value { get; }
    }
}