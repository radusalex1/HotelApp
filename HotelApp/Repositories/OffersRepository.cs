using HotelApp.DBContext;
using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelApp.Repositories
{
    public class OffersRepository
    {
        HotelContext hotelContext;

        public OffersRepository()
        {
            var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

            this.hotelContext =
                  new HotelContext(appSettings["ConnectionStrings"]);
        }

        /// <summary>
        /// Get Upcoming Offers.
        /// </summary>
        /// <returns></returns>
        public List<Offer> GetUpcomingOffers()
        {
            var currentDate = DateTime.Now.Date;
            return hotelContext.Offers.Where(o => o.StartDate >= currentDate).ToList();
        }

        /// <summary>
        /// Add a new offer.
        /// </summary>
        /// <param name="offer"></param>
        public void AddOffer(Offer offer)
        {
            hotelContext.Offers.Add(offer);
            hotelContext.SaveChanges();
        }
    }
}
