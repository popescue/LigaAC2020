﻿using System;

namespace Domain
{
    public class EventStartDate
    {
        public DateTime StartDateValue { get; }
        public EventStartDate(int year, int month, int day, int hour, int minute)
        {
            if (year < 2020 || year > 2022) throw new ArgumentException("Year must be between 2020-2022", "year");

            this.StartDateValue = new DateTime(year, month, day, hour, minute, 0);
        }
    }
}