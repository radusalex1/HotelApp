using HotelApp.DBContext;
using HotelApp.Models;
using System.Linq;

namespace HotelApp.Repositories
{
    public class UserRepository
    {
        HotelContext hotelContext;
        public UserRepository()
        {
            var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

          this.hotelContext =
                new HotelContext(appSettings["ConnectionStrings"]);
        }

        public void InsertUser(User user)
        {
            hotelContext.Users.Add(user);
            hotelContext.SaveChanges(); 
        }

        /// <summary>
        /// True if login credentials are valid. False otherwise.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <returns></returns>
        public bool LogInCheckCredentials(string Email,string Password)
        {
            return hotelContext.Users.Any(u=>u.Email==Email && u.Password == Password);
        }

        /// <summary>
        /// Returns the user based on email.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public  User GetUser(string Email)
        {
            return hotelContext.Users.FirstOrDefault(u=>u.Email==Email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ExistingUser(User user)
        {
            return hotelContext.Users.Any(u=>u.Email==user.Email || u.PhoneNumber==user.PhoneNumber);
        }

    }
}
