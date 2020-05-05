using System;

namespace Domain
{
    public class EventDuration
    {
        public TimeSpan DurationValue { get; }
        public EventDuration(TimeSpan DurationValue)
        {
            if (DurationValue.TotalHours > 50) throw new ArgumentException("Duration must not exceed 50 hours", "DurationValue");

            this.DurationValue = DurationValue;
        }
    }
}