using HotelApp.DBContext;
using HotelApp.Helps;
using HotelApp.Models;
using HotelApp.Views;
using System;
using System.Linq;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class StartViewModel
    {
        public HotelContext hotelContext;
        public StartViewModel()
        {
            try
            {
                var appSettings =
                    System.Configuration.ConfigurationManager.AppSettings;

               this.hotelContext=
                    new HotelContext(appSettings["ConnectionStrings"]);

                hotelContext.Database.CreateIfNotExists();

                if (!hotelContext.Users.Any())
                {
                    User user = new()
                    {
                        Name = "Radu_User",
                        Surname = "Alexandru_User",
                        PhoneNumber = "0735125928",
                        IsAdmin = false,
                        Email = "user@yahoo.com",
                        Password = "1234"
                    };
                    hotelContext.Users.Add(user);

                    User useradmin = new()
                    {
                        Name = "Radu_admin",
                        Surname = "admin",
                        PhoneNumber = "0735125929",
                        IsAdmin = true,
                        Email = "admin@yahoo.com",
                        Password = "admin"
                    };
                    hotelContext.Users.Add(useradmin);


                    Offer offer = new()
                    {
                        Name = "Oferta pasti",
                        NumberOfPersons = 4,
                        StartDate = System.DateTime.Now.Date,
                        EndDate = System.DateTime.Now.Date.AddDays(3),
                        Description = "Descriere",
                        Price = 750
                    };
                    hotelContext.Offers.Add(offer);


                    Offer offer1 = new()
                    {
                        Name = "Oferta craciun",
                        NumberOfPersons = 4,
                        StartDate = System.DateTime.Now.Date,
                        EndDate = System.DateTime.Now.Date.AddDays(3),
                        Description = "Descriere2",
                        Price = 800
                    };

                   
                    hotelContext.Offers.Add(offer1);
                    Room room = new()
                    {
                        RoomNumber = 1,
                        NumberOfPersons = 2,
                        Category = "Double",
                        Features = "Asdzxc"
                    };
                    hotelContext.Rooms.Add(room);

                    Prices price = new()
                    {
                        Room=room,
                        PricePerNight = 150,
                        StartDate = DateTime.Now.Date,
                        EndDate = DateTime.Now.Date.AddDays(30),
                    };
                    hotelContext.Prices.Add(price); 


                    hotelContext.SaveChanges();
                    hotelContext.Dispose();
                }
            }
            catch
            {
                var x = 3;
            }
        }

        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                _LoginCommand = new RelayCommand(LogIn);
                return _LoginCommand;
            }
        }
        public void LogIn(object param)
        {
            LoginPage loginPage = new LoginPage();
            LoginViewModel loginViewModel = new LoginViewModel();
            loginPage.DataContext = loginViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = loginPage;
            loginPage.Show();
        }

        private ICommand _RegisterCommand;
        public ICommand RegisterCommand
        {
            get
            {
                _RegisterCommand = new RelayCommand(SignUp);
                return _RegisterCommand;
            }
        }

        public void SignUp(object param)
        {
            RegisterPage registerPage = new RegisterPage();
            RegisterViewModel registerViewModel = new RegisterViewModel(hotelContext);
            registerPage.DataContext = registerViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = registerPage;
            registerPage.Show();
        }
    }
}
