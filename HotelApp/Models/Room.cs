using System.Collections.Generic;

namespace HotelApp.Models
{
    public class Room
    {
        public int Id { get; set; } 

        public int RoomNumber { get; set; }

        public int NumberOfPersons { get; set; }

        public bool Available { get; set; }

        public RoomType TypeOfRoom { get; set; }

        public List<string> Features { get; set; }

        public double PricePerNight { get; set; }

    }
}
