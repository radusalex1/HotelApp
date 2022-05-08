using HotelApp.DBContext;
using HotelApp.Helps;
using HotelApp.Views;
using System.Windows.Input;

namespace HotelApp.ViewModels
{
    public class StartViewModel
    {
        public StartViewModel()
        {
            try
            {
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;
                HotelContext hotelContext = new HotelContext(appSettings["ConnectionStrings"]);
                hotelContext.Database.CreateIfNotExists();
                
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
            LoginViewModel loginViewModel= new LoginViewModel();
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
            RegisterViewModel registerViewModel= new RegisterViewModel();
            registerPage.DataContext = registerViewModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow=registerPage;
            registerPage.Show();
        }
    }
}
