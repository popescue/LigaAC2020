using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Event
    {
        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public Location Location { get; }
        public DateTime StartsAt { get; }
        public TimeSpan Duration { get; }
        public EventType Type { get; }
        public Audience Audience { get; }
        public DateTime PublishDate { get; }
        public bool IsActive { get; }


        public Event(string Id, string Title, string Description, Location Location, DateTime StartsAt,
                        TimeSpan Duration, EventType EventType, Audience Audience, DateTime PublishDate, bool isActive)
        {
            //if (string.IsNullOrWhiteSpace(Id))
                //throw new ArgumentException("Id cannot be null or blank", "Id");

            if (string.IsNullOrWhiteSpace(Title))
                throw new ArgumentException("Title cannot be null or blank", "Title");

            if (Title.Length > 100)
                throw new ArgumentException("Title must not be longer than 100 characters", "Title");

            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description cannot be null or blank", "Description");

            if (Description.Length > 5000)
                throw new ArgumentException("Description must not be longer than 5000 characters", "Description");

            if (StartsAt.Year < 2020 || StartsAt.Year > 2022)
                throw new ArgumentException("Year must be between 2020-2022", "StartsAt");

            if (Duration.TotalHours > 50)
                throw new ArgumentException("Duration must not exceed 50 hours", "Duration");

            if (!Enum.IsDefined(typeof(EventType), EventType))
                throw new ArgumentException("Event Type is not a valid one", "EventType");

            if (!Enum.IsDefined(typeof(Audience), Audience))
                throw new ArgumentException("Audience is not a valid one", "EventType");

            if (PublishDate.Year < 2020 || PublishDate.Year > 2022)
                throw new ArgumentException("Year must be between 2020-2022", "StartsAt");

            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Location = Location;
            this.StartsAt = StartsAt;
            this.Duration = Duration;
            this.Type = EventType;
            this.Audience = Audience;
            this.PublishDate = PublishDate;
            this.IsActive = isActive;

        }

    }

    public enum Audience
    {
        Children,
        GeneralAudience
    }

    public enum EventType
    {
        Concert,
        Theatre
    }
}

/*- Titlu → string, maxLength(100), sa nu ramana campul gol?

- Descriere → string, maxLength(5000), not empty

- Poze → dim< 1MB?

- Locatie → string, maxLength(100), map

- Tip Locatie(Indoor, Outdoor) → dropdown?

- Start Date/Time : Ora(Timp) - (06.04.2021 19:00)

Clientul va alege dintr-un calendar si ceas?

No → 1 <= Day <=31, x <= Month <= y, Year = 2020, 2021, 2022

0 <= H< 24, 0 <= min <=60

- Durata (2h) → max(50h)

- Tip[(Concert, Teatru)] → dropdown, multiple selection

- Audienta(Copii, Audienta Generala) → dropdown, multiple selection

- Publish Date(data de la care evenimentul este vizibil in Hub) → same as Start Date

- Active(True/False)*/
