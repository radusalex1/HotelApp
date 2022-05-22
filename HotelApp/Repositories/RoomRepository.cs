using HotelApp.DBContext;
using HotelApp.Models;
using System.Collections.Generic;
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

        /// <summary>
        /// Checks if a room numbers exists.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckRoomNumber(int number)
        {
            return hotelContext.Rooms.Any(r => r.RoomNumber == number && r.Deleted==false);
        }

        /// <summary>
        /// Get a list of all rooms
        /// </summary>
        /// <returns></returns>
        public List<Room> GetAllRooms()
        {
            return hotelContext.Rooms.Where(r=>r.Deleted==false).ToList();
        }

        /// <summary>
        /// Deletes a room logically;
        /// </summary>
        public void DeleteRoom(Room room)
        {
            var res = hotelContext.Rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber && r.Deleted==false);
            res.Deleted = true;
            hotelContext.SaveChanges();
        }

        /// <summary>
        /// Updates a room;
        /// </summary>
        /// <param name="room"></param>
        public void UpdateRoom(Room room,int OldRoomNumber)
        {
            var res = hotelContext.Rooms.FirstOrDefault(r => r.RoomNumber == OldRoomNumber && r.Deleted==false);

            res.RoomNumber= room.RoomNumber;
            res.NumberOfPersons=room.NumberOfPersons;
            res.Features=room.Features;
            res.Category = room.Category;

            hotelContext.SaveChanges();
        }

        public Room GetRoomById(int id)
        {
            return hotelContext.Rooms.FirstOrDefault(r => r.Id == id);
        }
    }
}
