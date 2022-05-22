using System;

namespace HotelApp.Models
{
    public class Prices
    {
        public int Id { get; set; }

        public Room Room { get; set; } = null;

        public int RoomId { get; set; }

        public int PricePerNight { get; set; }  

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
