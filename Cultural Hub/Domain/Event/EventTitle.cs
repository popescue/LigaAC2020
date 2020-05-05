using System;

namespace Domain
{
    public class EventTitle
    {
        public string TitleValue { get; }
        public EventTitle(string TitleValue)
        {
            if (string.IsNullOrWhiteSpace(TitleValue)) throw new ArgumentException("Title cannot be null or blank", "TitleValue");
            if (TitleValue.Length > 100) throw new ArgumentException("Title must not be longer than 100 characters", "TitleValue");

            this.TitleValue = TitleValue;
        }
    }
}