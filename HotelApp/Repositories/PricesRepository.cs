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
        public PricesRepository()
        {
            var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

            this.hotelContext =
                  new HotelContext(appSettings["ConnectionStrings"]);

        }

        public List<Prices> GetAllPrices()
        {
            return hotelContext.Prices
                .Include(p=>p.Room)
                .Where(p => p.Deleted == false && p.Room.Deleted==false).ToList();  
        }

        public void AddPrice(Prices price)
        { 
            hotelContext.Prices.Add(price);
            hotelContext.SaveChanges();
        }

        public void DeletePrices(Prices prices)
        {
            var res = hotelContext.Prices.FirstOrDefault(p => p.Id == prices.Id && p.Deleted == false);
            res.Deleted = true;
            hotelContext.SaveChanges();
        }
    }
}
