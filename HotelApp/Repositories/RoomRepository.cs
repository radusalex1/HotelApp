using HotelApp.DBContext;
using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
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
            //hotelContext.Rooms.Add(room);

            var RoomNrPar = new SqlParameter("@RoomNr", room.RoomNumber); 
            var NrPersPar = new SqlParameter("@NumberOfPersons",room.NumberOfPersons);
            var CategoryPar= new SqlParameter("@Category",room.Category);
            var FeaturesPar= new SqlParameter("@Features", room.Features);

            hotelContext.Database.ExecuteSqlCommand("InsertRoom @RoomNr, @NumberOfPersons, @Category, @Features", RoomNrPar, NrPersPar, CategoryPar, FeaturesPar);

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
            var result = hotelContext.Database
                .SqlQuery<Room>("GetAllRooms").ToList();

            return result;
        }

        /// <summary>
        /// Deletes a room logically;
        /// </summary>
        public void DeleteRoom(Room room)
        { 
            var roomIdParameter = new SqlParameter("@roomId", room.Id);

            hotelContext.Database.ExecuteSqlCommand("DeleteRoomById @roomId", roomIdParameter);

            hotelContext.SaveChanges();

        }

        /// <summary>
        /// Updates a room;
        /// </summary>
        /// <param name="room"></param>
        public void UpdateRoom(Room room, int OldRoomNumber)
        {
            var res = hotelContext.Rooms.FirstOrDefault(r => r.RoomNumber == OldRoomNumber && r.Deleted == false);

            //res.RoomNumber = room.RoomNumber;
            //res.NumberOfPersons = room.NumberOfPersons;
            //res.Features = room.Features;
            //res.Category = room.Category;

            var RoomIdPar = new SqlParameter("@RoomId", res.Id);
            var RoomNrPar = new SqlParameter("@RoomNr", room.RoomNumber);
            var NrPersPar = new SqlParameter("@NumberOfPersons", room.NumberOfPersons);
            var CategoryPar = new SqlParameter("@Category", room.Category);
            var FeaturesPar = new SqlParameter("@Features", room.Features);

            hotelContext.Database.ExecuteSqlCommand("UpdateRoom @RoomId, @RoomNr, @NumberOfPersons, @Category, @Features", RoomIdPar,RoomNrPar, NrPersPar, CategoryPar, FeaturesPar);

            hotelContext.SaveChanges();
        }

        public Room GetRoomById(int id)
        {

            var roomIdParameter = new SqlParameter("@roomId", id);

            var result = hotelContext.Database
                .SqlQuery<Room>("GetRoomById @roomId", roomIdParameter).FirstOrDefault();

            return result;
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
