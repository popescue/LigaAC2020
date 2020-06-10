using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class EventPublishDate
    {
        public EventPublishDate(DateTime value)
        {
            if (value.Year < 2020 || value.Year > 2022)
                throw new ArgumentException("Year must be between 2020-2022", nameof(value));

            Value = value;
        }

        public DateTime Value { get; }

        public bool IsPublished(DateTime now)
        {
            return Value >= now;
        }
    }
}