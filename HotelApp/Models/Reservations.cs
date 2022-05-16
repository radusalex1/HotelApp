using System;

namespace HotelApp.Models
{
    public class Reservations
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public User User { get; set; }

        public Room Room { get; set; }  

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);

    }
}
