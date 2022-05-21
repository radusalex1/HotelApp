using System;
using System.Collections.Generic;

namespace HotelApp.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfPersons { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }
        

    }
}
