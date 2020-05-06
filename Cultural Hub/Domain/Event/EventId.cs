using System;

namespace Domain
{
    public class EventId
    {
        public string Value { get; set; }

        public EventId(string IdValue)
        {
            if (string.IsNullOrWhiteSpace(IdValue)) throw new ArgumentException("Id cannot be null or blank", "IdValue");
            this.Value = IdValue;
        }

        public static implicit operator string(EventId eventId)
        {
            return eventId.Value;
        }

        public static implicit operator EventId(string eventId)
        {
            return new EventId(eventId);
        }
    }
}