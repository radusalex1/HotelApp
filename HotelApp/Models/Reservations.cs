using System;

namespace HotelApp.Models
{
    public class Reservations
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User User { get; set; } = null;

        public Room Room { get; set; } = null;

        public bool IsOffers { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } 

    }
}
