namespace HotelApp.Models
{
    public class Room
    {
        public int Id { get; set; } 

        public int RoomNumber { get; set; }

        public int NumberOfPersons { get; set; }

        public string Category { get; set; }

        public string Features { get; set; }
    }
}
