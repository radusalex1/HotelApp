using System;

namespace HotelApp.Models
{
    public class Reservations
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public User User { get; set; }

        public Room Room { get; set; }

        public Offer Offers { get; set; } = null;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } 

    }
}
