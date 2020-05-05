using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cultural_HubTest
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void YearNotInRange()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(1, 3, 0, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123", "titlu", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void DurationNotInRange1()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(2, 3, 0, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123", "titlu", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void DurationNotInRange2()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(2, 2, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123", "titlu", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void DurationNotInRange3()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(55, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123", "titlu", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void IdEmpty()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(55, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("", "titlu", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void IdNull()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(55, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event(null, "titlu", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void TitleNull()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(55, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123", null, "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void TitleEmpty()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(55, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123", "", "costel costeleste",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }

        [TestMethod]
        public void TitleNotInRange()
        {
            DateTime d1 = new DateTime(2023, 1, 14, 3, 57, 0);
            TimeSpan t1 = new TimeSpan(55, 5, 0);
            Event e;
            Assert.ThrowsException<ArgumentException>(() => e = new Event("123",
                                                    new string('a', 101), "descriere",
                                                    new Location("strada Costle Doreleanu, 75", LocationType.Indoor), d1, t1,
                                                     EventType.Concert, Audience.GeneralAudience, d1, false));
        }
    }
}
