using HotelApp.DBContext;
using HotelApp.Models;

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
            hotelContext.Reservations.Add(reservation);
            hotelContext.SaveChanges();
        }
    }
}
