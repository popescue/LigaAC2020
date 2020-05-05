using System;

namespace Domain
{
    public class EventPublishDate
    {
        public DateTime PublishDateValue { get; }
        public EventPublishDate(DateTime PublishDateValue)
        {
            if (PublishDateValue.Year < 2020 || PublishDateValue.Year > 2022) throw new ArgumentException("Year must be between 2020-2022", "PublishDateValue");

            this.PublishDateValue = PublishDateValue;
        }
    }
}