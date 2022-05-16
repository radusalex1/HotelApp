using HotelApp.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
