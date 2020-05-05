using System;

namespace Domain
{
    public class EventDescription
    {
        public string DescriptionValue { get; }
        public EventDescription(string DescriptionValue)
        {
            if (string.IsNullOrWhiteSpace(DescriptionValue)) throw new ArgumentException("Description cannot be null or blank", "DescriptionValue");
            if (DescriptionValue.Length > 5000) throw new ArgumentException("Description must not be longer than 5000 characters", "DescriptionValue");

            this.DescriptionValue = DescriptionValue;
        }
    }
}