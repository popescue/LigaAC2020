﻿using System;
using System.Collections.Generic;

namespace Services
{
    public class EventDetails
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LocationAddress { get; set; }

        public string LocationType { get; set; }

        public DateTime StartsAt { get; set; }

        public TimeSpan Duration { get; set; }

        public string Type { get; set; }

        public string Audience { get; set; }

        public List<Uri> Pictures { get; set; }
    }
}