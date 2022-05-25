using HotelApp.DBContext;
using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            List<Offer> offers = new List<Offer>(hotelContext.Offers.Where(o => o.StartDate >= currentDate && o.Deleted==false)
                .OrderBy(r=>r.StartDate)
                .ToList());

            List<Offer> offersAvailable = new List<Offer>();

            foreach (var offer in offers)
            {
                if(AvailableRooms(offer.NumberOfPersons,offer.StartDate,offer.EndDate)!=0)
                {
                    offersAvailable.Add(offer);
                }
            }
            return offersAvailable;
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

        public int AvailableRooms(int NumberOfPersons, DateTime startDate, DateTime endDate)
        {
            ///still not working properly;its working i guess:). not working now working:)
            ///lista toate rezervarile
            List<Reservations> res1 = hotelContext.Reservations
                .Include(r => r.Room)
                .Where(r => r.Room.NumberOfPersons >= NumberOfPersons && r.Room.Deleted==false)
                .ToList();

            //!res1.Any(r => r.EndDate >= startDate && r.StartDate <= endDate)

            ///lista toate camere de nr pers; presupunem ca toate camerele sunt libere
            List <Room> RoomsNrPers = new List<Room>(hotelContext.Rooms.Where(r => r.NumberOfPersons == NumberOfPersons && r.Deleted==false).ToList());
            List<Room> availableRooms = new List<Room>();

            foreach (var r in res1)
            { 

                if(r.EndDate<=startDate && r.StartDate >= endDate)
                {
                    availableRooms.Add(r.Room);
                }

            }           

            return availableRooms.Count();
        }

        public List<Offer> GetAllOffers()
        {
            return hotelContext.Offers.OrderByDescending(o=>o.StartDate).ToList();
        }
    }
}
