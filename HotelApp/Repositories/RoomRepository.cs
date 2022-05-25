using HotelApp.DBContext;
using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
            return hotelContext.Rooms.Where(r => r.Category == Category).Count();
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
            return hotelContext.Rooms.Any(r => r.RoomNumber == number && r.Deleted == false);
        }

        /// <summary>
        /// Get a list of all rooms
        /// </summary>
        /// <returns></returns>
        public List<Room> GetAllRooms()
        {
            return hotelContext.Rooms.Where(r => r.Deleted == false).ToList();
        }

        /// <summary>
        /// Deletes a room logically;
        /// </summary>
        public void DeleteRoom(Room room)
        {
            var res = hotelContext.Rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber && r.Deleted == false);
            res.Deleted = true;
            hotelContext.SaveChanges();
        }

        /// <summary>
        /// Updates a room;
        /// </summary>
        /// <param name="room"></param>
        public void UpdateRoom(Room room, int OldRoomNumber)
        {
            var res = hotelContext.Rooms.FirstOrDefault(r => r.RoomNumber == OldRoomNumber && r.Deleted == false);

            res.RoomNumber = room.RoomNumber;
            res.NumberOfPersons = room.NumberOfPersons;
            res.Features = room.Features;
            res.Category = room.Category;

            hotelContext.SaveChanges();
        }

        public Room GetRoomById(int id)
        {
            return hotelContext.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public ObservableCollection<Room> GetAvailableRooms(DateTime startDate, DateTime endDate, int numberOfPersons)
        {
            ///still not working properly;its working i guess:)
            ///lista toate rezervarile
            List<Reservations> res1 = hotelContext.Reservations
                .Include(r => r.Room)
                .Where(r => r.Room.NumberOfPersons == numberOfPersons)
                .ToList();

            ///lista toate camere de nr pers; presupunem ca toate camerele sunt libere
            List<Room> RoomsNrPers = new List<Room>(hotelContext.Rooms.Where(r => r.NumberOfPersons == numberOfPersons).ToList());

            foreach (var r in res1)
            {
                ///datele selectate sunt in afara oricarei rezervari
                if (endDate <= r.StartDate || startDate >= r.EndDate)
                {
                    if (!RoomsNrPers.Contains(r.Room))
                    {
                        RoomsNrPers.Add(r.Room);
                    }
                }
                else
                {
                    RoomsNrPers.Remove(r.Room);
                    break;
                }
            }

            ObservableCollection<Room> availableRooms = new ObservableCollection<Room>(RoomsNrPers);

            return availableRooms;

        }
    }
}
