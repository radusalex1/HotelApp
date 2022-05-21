using HotelApp.DBContext;
using HotelApp.Models;
using System.Linq;

namespace HotelApp.Repositories
{
    public class RoomRepository
    {
        HotelContext hotelContext;

        public RoomRepository()
        {
            var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

            this.hotelContext =
                  new HotelContext(appSettings["ConnectionStrings"]);
        }

        /// <summary>
        /// Get Number of rooms based on category.
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public int GetNumberOfRoomsBasedOnCategory(string Category)
        {
           return hotelContext.Rooms.Where(r=>r.Category == Category).Count();
        }

        /// <summary>
        /// Adds a room.
        /// </summary>
        /// <param name="room"></param>
        public void AddRoom(Room room)
        {
            hotelContext.Rooms.Add(room);
            hotelContext.SaveChanges();
        }
    }
}
