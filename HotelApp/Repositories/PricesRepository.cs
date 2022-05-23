using HotelApp.DBContext;
using HotelApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HotelApp.Repositories
{
    public class PricesRepository
    {
        HotelContext hotelContext;
        RoomRepository roomRepository;

        public PricesRepository()
        {
            var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

            this.hotelContext =
                  new HotelContext(appSettings["ConnectionStrings"]);

            this.roomRepository = new RoomRepository();
        }

        public List<Prices> GetAllPrices()
        {
            return hotelContext.Prices
                .Include(p=>p.Room)
                .Where(p => p.Deleted == false && p.Room.Deleted==false).ToList();  
        }

        public void AddPrice(Prices price,Room room)
        { 
            hotelContext.Prices.Add(price);
            hotelContext.SaveChanges();

            var res = hotelContext.Prices.FirstOrDefault(p => p.Id == price.Id);
            res.Room = hotelContext.Rooms.FirstOrDefault(r => r.Id == room.Id);

            hotelContext.SaveChanges();
        }

        public void DeletePrices(Prices prices)
        {
            var res = hotelContext.Prices.FirstOrDefault(p => p.Id == prices.Id && p.Deleted == false);
            res.Deleted = true;
            hotelContext.SaveChanges();
        }

        /// <summary>
        /// order asc by startdate.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public List<Prices> GetPricesForRoom(int roomId)
        {
            return hotelContext.Prices
                       .Where(p => p.RoomId == roomId)
                       .OrderBy(p => p.StartDate).ToList();
        }
    }
}
