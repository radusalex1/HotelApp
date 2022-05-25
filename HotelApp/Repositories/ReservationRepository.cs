using HotelApp.DBContext;
using HotelApp.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace HotelApp.Repositories
{
    public class ReservationRepository
    {
        HotelContext hotelContext;

        public ReservationRepository()
        {
            var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

            this.hotelContext =
                  new HotelContext(appSettings["ConnectionStrings"]);
        }

        public void AddReservation(Reservations reservation)
        {
            Reservations reservations = new Reservations()
            {
                Name = reservation.Name,
                User = hotelContext.Users.FirstOrDefault(u => u.Id == reservation.User.Id),
                Room = hotelContext.Rooms.FirstOrDefault(r => r.Id == reservation.Room.Id),
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
            };

            hotelContext.Reservations.Add(reservations);
            hotelContext.SaveChanges();

            ///Adds a price
            var reservation1 = hotelContext.Reservations
                       .FirstOrDefault(r => r.Id == reservations.Id);

            var pricesList = hotelContext.Prices
                .Where(p => p.RoomId == reservations.Room.Id)
                .OrderBy(p => p.StartDate).ToList();

            int totalPrice = 0;

            foreach (var price2 in pricesList)
            {
                if (reservation1.StartDate >= price2.StartDate && reservation1.EndDate <= price2.EndDate)
                {
                    totalPrice += (reservation1.EndDate - reservation1.StartDate).Days * price2.PricePerNight;
                    break;
                }
                else
                {
                    if (reservation1.StartDate <= price2.EndDate && reservation1.StartDate >= price2.StartDate)
                    {
                        totalPrice += (price2.EndDate - reservation1.StartDate).Days * price2.PricePerNight;
                    }
                    else
                    {
                        if (reservation1.EndDate >= price2.StartDate && reservation.EndDate <= price2.EndDate)
                        {
                            totalPrice += (reservation1.EndDate - price2.StartDate).Days * price2.PricePerNight;
                        }
                        else
                        {
                            totalPrice += (price2.EndDate - price2.StartDate).Days * price2.PricePerNight;
                        }
                    }
                }
            }

            reservation1.Price = totalPrice;

            hotelContext.SaveChanges();


        }
        public List<Reservations> GetREservationsByUser(User user)
        {
            List<Reservations> reservations = new List<Reservations>();

            reservations = hotelContext.Reservations
                   .Include(r => r.Room)
                   .Include(r => r.User)
                   .Where(r => r.User.Id == user.Id).ToList();

            

            return reservations;
        }
    }
}
