using System;

namespace Domain
{
    public class EventId
    {
        public string IdValue { get; set; }
        public EventId(string IdValue)
        {
            if (string.IsNullOrWhiteSpace(IdValue)) throw new ArgumentException("Id cannot be null or blank", "IdValue");
            this.IdValue = IdValue;
        }
    }
}