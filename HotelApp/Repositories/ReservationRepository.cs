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

        public void AddReservation(Reservations reservation, int NumberOfPersons)
        {
            if (reservation.IsOffer == true)
            {
                Reservations reservations = new()
                {
                    Name = reservation.Name,
                    User = hotelContext.Users.FirstOrDefault(u => u.Id == reservation.UserId),
                    Room = hotelContext.Rooms.FirstOrDefault(r => r.NumberOfPersons == NumberOfPersons),
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    IsOffer = true,
                    Price = reservation.Price,
                    Status=reservation.Status,
                };
                reservations.UserId=reservation.UserId;
                reservations.RoomId = reservations.Room.Id;

                hotelContext.Reservations.Add(reservations);
            }
            else
            {
                Reservations reservations = new Reservations()
                {
                    Name = reservation.Name,
                    User = hotelContext.Users.FirstOrDefault(u => u.Id == reservation.User.Id),
                    Room = hotelContext.Rooms.FirstOrDefault(r => r.Id == reservation.Room.Id),
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Status=reservation.Status
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
            }
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

        public List<Reservations> GetAllReservations()
        {
            return hotelContext.Reservations.ToList();
        }

        public void ChageStatusOfReservation(Reservations reservation)
        {
            var res = hotelContext.Reservations
                .Include(r=>r.Room)
                .Include(r=>r.User)
                .FirstOrDefault(r => r.Id == reservation.Id);

            if (res.Status == "paid")
            {
                res.Status = "unpaid";
            }
            else
            {
                res.Status = "paid";
            }
            hotelContext.SaveChanges();
        }
    }
}
