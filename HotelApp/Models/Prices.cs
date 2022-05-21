using HotelApp.Helps;
using System;

namespace HotelApp.Models
{
    public class Prices:NotifyPropertyChangedHelp
    {
        public int Id { get; set; }

        public Room Room { get; set; }

        public int Price { get; set; }  

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }   
    }
}
