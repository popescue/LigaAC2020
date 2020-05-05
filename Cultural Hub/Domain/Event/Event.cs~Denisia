using System;


namespace Domain
{
    public class Event
    {
        public EventId Id { get; }
        public ClientId ClientId { get; }
        public EventTitle Title { get; }
        public EventDescription Description { get; }
        public Location Location { get; }
        public EventStartDate StartsAt { get; }
        public EventDuration Duration { get; }
        public EventType Type { get; }
        public Audience Audience { get; }
        public EventPublishDate PublishDate { get; }
        public bool IsActive { get; }
        public bool IsPublished { get; }

        public Event( EventId Id, EventTitle Title, EventDescription Description, Location Location, EventStartDate StartsAt, EventDuration Duration, EventType Type, Audience Audience, EventPublishDate PublishDate, bool IsActive)
            {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Location = Location;
            this.StartsAt = StartsAt;
            this.Duration = Duration;
            this.Type = Type;
            this.Audience = Audience;
            this.PublishDate = PublishDate;
            this.IsActive = IsActive;
            }

        public Event(EventId id, ClientId clientId)
        {
            this.Id = id;
            this.ClientId = clientId;
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
