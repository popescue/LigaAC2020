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